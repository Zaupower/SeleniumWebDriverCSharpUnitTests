using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sleeniumTest.Models
{
    public class OrderDetails
    {
        IEnumerable<Product> products { get; set; }
        public string SubTotal { get; set; }
        public string ShippingAndHandling { get; set; }
        public string GrandTotal { get; set; }

    }
}
