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
            IEnumerable<Order> orders;
            Order expectedOrder;

            using (IDbConnection connection = Database.GetConnection())
            {
                expectedOrder = new Order { ProductId = 1, ContactName = "Alastair" };
                _newId = connection.Insert(expectedOrder);

                orders = connection.Query<Order>("SELECT * FROM Orders WHERE Id = @Id", new { Id = _newId });
            }
            
            Assert.That(orders, Is.Not.Null);
            var actualOrder = orders.First();
            Assert.That(actualOrder.ProductId, Is.EqualTo(expectedOrder.ProductId));
            Assert.That(actualOrder.ContactName, Is.EqualTo(expectedOrder.ContactName));
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
