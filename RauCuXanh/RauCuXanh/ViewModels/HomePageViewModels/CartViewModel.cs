using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using RauCuXanh.Models;
using RauCuXanh.Services;
using RauCuXanh.Views.HomePageViews;
using Refit;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs;

namespace RauCuXanh.ViewModels.HomePageViewModels
{
    public class CartViewModel : HomeViewModel
    {
        private float _initialCost = 0;
        public float InitialCost { get { return _initialCost; } set { SetProperty(ref _initialCost, value); } }
        private float _totalCost = 0;
        public float TotalCost { get { return _totalCost; } set { SetProperty(ref _totalCost, value); } }
        private float _discount = 0;
        public float Discount { get { return _discount; } set { SetProperty(ref _discount, value); } }

        public ObservableCollection<CartItem> CartProducts { get; set; }
        public Command LoadCart { get; set; }
        public Command DeleteCommand { get; set; }
        public Command BuyCommand { get; set; }
        public Command IncreaseQuantity { get; set; }
        public Command DecreaseQuantity { get; set; }

        public CartViewModel()
        {
            Title = "Giỏ hàng";
            CartProducts = new ObservableCollection<CartItem>();
            LoadCart = new Command(async () => await ExeLoadCart());
            DeleteCommand = new Command<Cart>(ExeDelete);
            BuyCommand = new Command(ExeBuy);
            IncreaseQuantity = new Command<Cart>(ExeIncrease);
            DecreaseQuantity = new Command<Cart>(ExeDecrease);
        }

        public async void ExeIncrease(Cart cart)
        {
            var cartService = RestService.For<ICartApi>(RestClient.BaseUrl);
            var response = await cartService.UpdateCart(new Cart() { Raucu_id = cart.Raucu_id, User_id = cart.User_id, Quantity = ++cart.Quantity });

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                IsBusy = true;
            }
        }

        public async void ExeDecrease(Cart cart)
        {
            if (cart.Quantity == 1)
            {
                return;
            }
            var cartService = RestService.For<ICartApi>(RestClient.BaseUrl);
            var response = await cartService.UpdateCart(new Cart() { Raucu_id = cart.Raucu_id, User_id = cart.User_id, Quantity = --cart.Quantity });

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                IsBusy = true;
            }
        }

        public async Task ExeLoadCart()
        {
            IsBusy = true;
            try
            {
                await ExecuteLoadRaucusCommand();
                CartProducts.Clear();
                InitialCost = 0;
                Discount = 0;
                TotalCost = 0;
                var cartService = RestService.For<ICartApi>(RestClient.BaseUrl);
                var carts = await cartService.GetCarts();
                var UserID = 1;
                foreach (var c in carts)
                {
                    if (c.User_id == UserID)
                    {
                        foreach (Raucu r in Raucus)
                        {
                            if (r.Id == c.Raucu_id)
                            {
                                CartProducts.Add(new CartItem() { Raucu = r, Cart = c });
                                InitialCost += r.Price * c.Quantity;
                                Discount += r.Price * r.Discount * c.Quantity;
                                TotalCost += (r.Price - r.Price * r.Discount) * c.Quantity;
                            }
                        }
                    }
                }
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

        public async void ExeDelete(Cart cart)
        {
            var res = await App.Current.MainPage.DisplayAlert("Thông báo", "Bạn có muốn xóa sản phẩm không?", "Có", "Không");
            if (res)
            {
                var cartService = RestService.For<ICartApi>(RestClient.BaseUrl);
                var response = await cartService.DeleteCart(cart);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    IsBusy = true;
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Thất bại", "Xóa thất bại!", "OK");
                }
            }
        }

        public async void ExeBuy()
        {
            if (CartProducts.Count == 0)
            {
                return;
            }
            await App.Current.MainPage.Navigation.PushAsync(new OrderPage());
        }
    }
}
