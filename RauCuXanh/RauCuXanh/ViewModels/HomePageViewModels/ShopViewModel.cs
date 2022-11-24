using System;
using System.Collections.Generic;
using System.Text;
using RauCuXanh.Models;

namespace RauCuXanh.ViewModels.HomePageViewModels
{
    public class ShopViewModel : HomeViewModel
    {
        private Shop _shop;
        public Shop Shop { get { return _shop; } set { SetProperty(ref _shop, value); } }
        public ShopViewModel() { }
        public ShopViewModel(Shop s) 
        {
            Title = s.Name;
            Shop = s;
        }
    }
}
