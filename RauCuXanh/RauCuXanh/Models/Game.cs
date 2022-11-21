using System;
using System.Collections.Generic;
using System.Text;

namespace RauCuXanh.Models
{
    public class Game
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Thumbnail { get; set; }
        public string Short_description { get; set; }
        public string Game_url { get; set; }
        public string Genre { get; set; }
        public string Platform { get; set; }
        public string Publisher { get; set; }
        public string Developer { get; set; }
        public string Release_date { get; set; }
        public string Freetogame_profile_url { get; set; }
    }
    internal class BaseFreeToPlayApi
    {
        public static string BaseUrl => "https://www.freetogame.com/api";
    }
}
