using System;
using System.Collections.Generic;
using System.Text;

namespace RauCuXanh.Models
{
    public class Raucu
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Raucu_type { get; set; }
        public int Price { get; set; }
        public float Discount { get; set; }
        public string Product_pic { get; set; }
        public int Review_id { get; set; }
        public int Shop_id { get; set; }
        public string Timestamp { get; set; }

        public Raucu() { }
        public Raucu(int id, string name, string description, string raucu_type, int price, float discount, string product_pic, int review_id, int shop_id, string timestamp)
        {
            Id = id;
            Name = name;
            Description = description;
            Raucu_type = raucu_type;
            Price = price;
            Discount = discount;
            Product_pic = product_pic;
            Review_id = review_id;
            Shop_id = shop_id;
            Timestamp = timestamp;
        }
    }
}
