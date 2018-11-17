using Dapper;
using Dapper.Contrib.Extensions;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Persistence.Dapper
{
    [TestFixture]
    public class OrderTests
    {

        [Test]
        public void InsertOrder()
        {
            IEnumerable<Order> orders;

            using (IDbConnection connection = Database.GetConnection())
            {
                var newId = connection.Insert(new Order { ProductId = 1, ContactName = "Alastair" });

                orders = connection.Query<Order>("SELECT * FROM Orders WHERE Id = @Id", new { Id = newId });
            }

            
            Assert.That(orders, Is.Not.Null);
        }
    }
}
