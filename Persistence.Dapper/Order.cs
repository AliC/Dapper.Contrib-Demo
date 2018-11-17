using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Dapper
{
    public class Order
    {
        public int Id { get; set; }
        public int DistributorId { get; set; }
        public string ContactName { get; set; }

        [Computed]
        public IEnumerable<OrderLine> OrderLines { get; set; }
    }

    public class OrderLine
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
