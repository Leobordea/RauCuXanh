using System;
using System.Collections.Generic;
using System.Text;

namespace RauCuXanh.Models
{
    public class Review
    {
        public string Comments { get; set; }
        public int Id { get; set; }
        public int? Raucu_id { get; set; }
        public string Review_type { get; set; }
        public int? Shop_id { get; set; }
        public float Stars { get; set; }
        public string Timestamp { get; set; }
        public int User_id { get; set; }
    }
}
