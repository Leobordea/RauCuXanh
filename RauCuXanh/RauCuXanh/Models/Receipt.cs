using System;
using System.Collections.Generic;
using System.Text;

namespace RauCuXanh.Models
{
    public class Receipt
    {
        public int Id { get; set; }
        public int User_id { get; set; }
        public string Shipping_cost { get; set; }
        public string Shipping_addr { get; set; }
        public string Total_price { get; set; }
        public string Timestamp { get; set; }
        public string Order_status { get; set; }
    }
}
