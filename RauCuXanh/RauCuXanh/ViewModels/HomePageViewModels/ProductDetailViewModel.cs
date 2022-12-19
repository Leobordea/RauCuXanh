using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using RauCuXanh.Models;
using RauCuXanh.Services;
using RauCuXanh.Views.HomePageViews;
using Refit;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs;

namespace RauCuXanh.ViewModels.HomePageViewModels
{
    public class ProductDetailViewModel : BaseViewModel
    {
        private Raucu _raucu;
        public Raucu Raucu
        {
            get { return _raucu; }
            set { SetProperty(ref _raucu, value); }
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
        public Command LoadShopCommand { get; set; }
        public Command IncreaseQuantity { get; set; }
        public Command DecreaseQuantity { get; set; }
        public Command NavToShopCommand { get; set; }
        public Command AddToCart { get; set; }

        public ProductDetailViewModel() { }
        public ProductDetailViewModel(Raucu p)
        {
            Title = "Chi tiết sản phẩm";
            Raucu = p;
            LoadShopCommand = new Command(async () => await ExeLoadShopCommand());
            IncreaseQuantity = new Command(ExeIncreaseQuantity);
            DecreaseQuantity = new Command(ExeDecreaseQuantity);
            NavToShopCommand = new Command(ExeNavToShop);
            AddToCart = new Command(ExeAddToCart);
        }

        async Task ExeLoadShopCommand()
        {
            IsBusy = true;
            try
            {
                var apiClient = RestService.For<IShopApi>(RestClient.BaseUrl);
                Shop = await apiClient.GetShopById(Raucu.Shop_id);
            } 
            catch (Exception ex)
            {
                await MaterialDialog.Instance.AlertAsync(message: ex.Message);
            } 
            finally
            {
                IsBusy = false;
            }
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

        public async void ExeAddToCart()
        {
        }

        public void OnAppearing()
        {
            IsBusy = true;
        }
    }
}
