using System;
using System.Collections.Generic;
using System.Text;

namespace RauCuXanh.Models
{
    public class Cart
    {
        public string id { get; set; }
        public int quantity { get; set; }
        public string raucu_id { get; set; }
        public string timestamp { get; set; }
        public string user_id { get; set; }
    }
}
