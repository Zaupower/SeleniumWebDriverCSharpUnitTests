using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sleeniumTest.Models
{
    public class OrderDetails
    {
        public IEnumerable<Product> Products { get; set; }
        public decimal SubTotal { get; set; }
        public decimal ShippingAndHandling { get; set; }
        public decimal GrandTotal { get; set; }

    }
}
