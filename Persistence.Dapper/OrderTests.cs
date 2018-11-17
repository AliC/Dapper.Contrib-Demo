using Dapper;
using Dapper.Contrib.Extensions;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Transactions;

namespace Persistence.Dapper
{
    [TestFixture]
    public class OrderTests
    {
        private long _newId;

        [Test]
        public void InsertOrder()
        {
            IEnumerable<Order> actualOrders;

            Order expectedOrder = new Order
            {
                DistributorId = 1,
                ContactName = "Alastair",
                OrderLines = new List<OrderLine>
                {
                    new OrderLine { ProductId = 2, Quantity = 3 },
                    new OrderLine { ProductId = 3, Quantity = 1 }
                }
            };

            var sql = "SELECT * FROM Orders o LEFT OUTER JOIN OrderLines ol ON o.Id = ol.OrderId WHERE o.Id = @Id";
            using (IDbConnection connection = Database.GetConnection())
            {
                expectedOrder.Id = (int)connection.Insert(expectedOrder);
                _newId = expectedOrder.Id;

                foreach (var orderLine in expectedOrder.OrderLines)
                {
                    orderLine.OrderId = expectedOrder.Id;
                    orderLine.Id = (int)connection.Insert(orderLine);
                }

                actualOrders = connection.Query(sql, GetMap(), new { expectedOrder.Id }).Distinct();
            }

            Assert.That(actualOrders, Is.Not.Null);
            CollectionAssert.IsNotEmpty(actualOrders);
            Assert.That(actualOrders.Count(), Is.EqualTo(1));

            var actualOrder = actualOrders.First();
            Assert.That(actualOrder.DistributorId, Is.EqualTo(expectedOrder.DistributorId));
            Assert.That(actualOrder.ContactName, Is.EqualTo(expectedOrder.ContactName));

            Assert.That(actualOrder.OrderLines, Is.Not.Null);
            CollectionAssert.IsNotEmpty(actualOrder.OrderLines);
            Assert.That(actualOrder.OrderLines.Count(), Is.EqualTo(expectedOrder.OrderLines.Count()));
            Assert.True(actualOrder.OrderLines.All(ol => ol.OrderId == expectedOrder.Id));

            ((List<OrderLine>)expectedOrder.OrderLines).ForEach(eol =>
                Assert.True(eol.ProductId == actualOrder.OrderLines.Single(aol => aol.Id == eol.Id).ProductId));
            ((List<OrderLine>)expectedOrder.OrderLines).ForEach(eol =>
                Assert.True(eol.Quantity == actualOrder.OrderLines.Single(aol => aol.Id == eol.Id).Quantity));
        }

        private Func<Order, OrderLine, Order> GetMap()
        {
            var existingOrders = new Dictionary<int, Order>();

            return (o, ol) =>
            {
                if (!existingOrders.TryGetValue(o.Id, out var order))
                {
                    order = o;
                    order.OrderLines = new List<OrderLine>();
                    existingOrders.Add(order.Id, order);
                }

                ((IList<OrderLine>)order.OrderLines).Add(ol);

                return order;
            };
        }

        [SetUp]
        public void SetUp()
        {
        }

        [TearDown()]
        public void TearDown()
        {
            Database.DeleteOrder(_newId);
        }
    }
}
