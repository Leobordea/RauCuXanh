using System;
using System.Collections.Generic;
using System.Text;

namespace RauCuXanh.Models
{
    public class Receipt
    {
        public string Id { get; set; }
        public string User_id { get; set; }
        public float Shipping_cost { get; set; }
        public string Shipping_addr { get; set; }
        public float Total_price { get; set; }
        public string Timestampt { get; set; }
    }
}
