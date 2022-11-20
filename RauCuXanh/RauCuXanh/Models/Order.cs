using System;
using System.Collections.Generic;
using System.Text;

namespace RauCuXanh.Models
{
    public class Order
    {
        public string OrderID { get; set; }
        public string OrderDate { get; set; }
        public int OrderQuantity { get; set; }
        public int OrderPrice { get; set; }
        public string OrderStatus { get; set; }
    }
}
