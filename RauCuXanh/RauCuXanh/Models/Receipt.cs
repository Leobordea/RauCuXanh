using System;
using System.Collections.Generic;
using System.Text;

namespace RauCuXanh.Models
{
    public class Receipt
    {
        public string Id { get; set; }
        public string User_id { get; set; }
        public int Shipping_cost { get; set; }
        public string Shipping_addr { get; set; }
        public int Total_price { get; set; }
        public string Time_stamp { get; set; }
    }
}
