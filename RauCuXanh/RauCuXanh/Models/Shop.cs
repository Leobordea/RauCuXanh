using System;
using System.Collections.Generic;
using System.Text;

namespace RauCuXanh.Models
{
    public class Shop
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone_no { get; set; }
        public string Profile_pic { get; set; }
        public int No_selling { get; set; }
        public int No_sold { get; set; }
        public int Review_id { get; set; }
        public string Timestamp { get; set; }

        public Shop() { }
        public Shop(int id, string name, string address, string phone_no, string profile_pic, int no_selling, int no_sold, int review_id, string timestamp)
        {
            Id = id;
            Name = name;
            Address = address;
            Phone_no = phone_no;
            Profile_pic = profile_pic;
            No_selling = no_selling;
            No_sold = no_sold;
            Review_id = review_id;
            Timestamp = timestamp;
        }
    }
}
