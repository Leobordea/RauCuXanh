using System;
using System.Collections.Generic;
using System.Text;

namespace RauCuXanh.Models
{
    public class Raucu
    {
        public string Description { get; set; }
        public float Discount { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Product_pic { get; set; }
        public string Raucu_type { get; set; }
        public int Review_id { get; set; }
        public int Shop_id { get; set; }
        public string Timestamp { get; set; }
    }
}
