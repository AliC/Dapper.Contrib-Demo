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
    }

    public class OrderLine
    {
        public int OrderId { get; internal set; }
        public int ProductId { get; internal set; }
        public int Quantity { get; internal set; }
    }
}
