using System;
using System.Collections.Generic;
using System.Text;

namespace RauCuXanh.Models
{
    public class Cart
    {
        public int Quantity { get; set; }
        public int Raucu_id { get; set; }
        public string Timestamp { get; set; }
        public int User_id { get; set; }
    }
}
