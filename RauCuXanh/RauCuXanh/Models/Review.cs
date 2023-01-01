using System;
using System.Collections.Generic;
using System.Text;

namespace RauCuXanh.Models
{
    public class Review
    {
        public string Id { get; set; }
        public string Timestampt { get; set; }
        public string User_id { get; set; }
        public int Star { get; set; }
        public string Comment { get; set; }
        public string Review_type { get; set; }
        public string Raucu_id { get; set; }
        public string Shop_id { get; set; }
    }
}
