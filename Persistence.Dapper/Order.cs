using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Dapper
{
    public class Order
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ContactName { get; set; }
    }
}
