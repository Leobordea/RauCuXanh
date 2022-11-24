using System;
using System.Collections.Generic;
using System.Text;
using RauCuXanh.Models;
using RauCuXanh.Views.HomePageViews;
using Xamarin.Forms;

namespace RauCuXanh.ViewModels.HomePageViewModels
{
    public class ProductDetailViewModel : HomeViewModel
    {
        private Raucu _product;
        public Raucu Product
        {
            get { return _product; }
            set { SetProperty(ref _product, value); }
        }

        private int _quantity = 1;
        public int Quantity
        {
            get { return _quantity; }
            set {
                if (value >= 1)
                    SetProperty(ref _quantity, value);
            }
        }

        private Shop _shop;
        public Shop Shop
        {
            get { return _shop; }
            set { SetProperty(ref _shop, value); }
        }

        public Command IncreaseQuantity { get; set; }
        public Command DecreaseQuantity { get; set; }
        public Command NavToShopCommand { get; set; }

        public ProductDetailViewModel() { }
        public ProductDetailViewModel(Raucu p)
        {
            Title = "Chi tiết sản phẩm";
            Product = p;
            foreach (Shop s in Shops)
            {
                if (s.Id == p.Shop_id)
                {
                    Shop = s;
                    break;
                }
            }
            IncreaseQuantity = new Command(ExeIncreaseQuantity);
            DecreaseQuantity = new Command(ExeDecreaseQuantity);
            NavToShopCommand = new Command(ExeNavToShop);
        }

        public void ExeIncreaseQuantity()
        {
            Quantity++;
        }

        public void ExeDecreaseQuantity()
        {
            Quantity--;
        }

        public async void ExeNavToShop()
        {
            await App.Current.MainPage.Navigation.PushAsync(new ShopPage(Shop));
        }
    }
}
