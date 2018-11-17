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
            IEnumerable<OrderLine> actualOrderLines;

            Order expectedOrder = new Order
            {
                DistributorId = 1,
                ContactName = "Alastair"
            };

            var expectedOrderLines = new List<OrderLine>
            {
                new OrderLine
                {
                    ProductId = 2,
                    Quantity = 3
                }
            };

            using (IDbConnection connection = Database.GetConnection())
            {
                _newId = connection.Insert(expectedOrder);

                expectedOrderLines.ForEach(ol => ol.OrderId = (int)_newId);
                connection.Insert(expectedOrderLines);
                
                actualOrders = connection.Query<Order>("SELECT * FROM Orders WHERE Id = @Id", new { Id = _newId });
                actualOrderLines = connection.Query<OrderLine>("SELECT * FROM OrderLines WHERE OrderId = @Id", new { Id = _newId });
            }

            Assert.That(actualOrders, Is.Not.Null);
            CollectionAssert.IsNotEmpty(actualOrders);

            var actualOrder = actualOrders.First();
            Assert.That(actualOrder.DistributorId, Is.EqualTo(expectedOrder.DistributorId));
            Assert.That(actualOrder.ContactName, Is.EqualTo(expectedOrder.ContactName));

            Assert.That(actualOrderLines, Is.Not.Null);
            CollectionAssert.IsNotEmpty(actualOrderLines);
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
