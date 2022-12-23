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
        public Command OpenPopup { get; set; }
        public Command LoadCart { get; set; }
        public Command DeleteCommand { get; set; }
        public Command BuyCommand { get; set; }
        public CartViewModel()
        {
            Title = "Giỏ hàng";
            CartProducts = new ObservableCollection<CartItem>();
            LoadCart = new Command(async () => await ExeLoadCart());
            OpenPopup = new Command<View>(ExeOpenPopup);
            DeleteCommand = new Command<string>(ExeDelete);
            BuyCommand = new Command(ExeBuy);
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
                var cartService = new CartService();
                var carts = await cartService.getCarts();
                var UserID = "1";
                foreach (var c in carts)
                {
                    if (c.user_id == UserID)
                    {
                        foreach (Raucu r in Raucus)
                        {
                            if (r.Id == c.raucu_id.ToString())
                            {
                                CartProducts.Add(new CartItem() { Raucu = r, Cart = c });
                                InitialCost += r.Price * c.quantity;
                                Discount += r.Price * r.Discount * c.quantity;
                                TotalCost += (r.Price - r.Price * r.Discount) * c.quantity;
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

        public async void ExeDelete(string id)
        {
            var cartService = new CartService();
            var res = await App.Current.MainPage.DisplayAlert("Thông báo", "Bạn có muốn xóa sản phẩm không?", "Có", "Không");
            if (res)
            {
                var response = await cartService.deleteCart(id);

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
            await App.Current.MainPage.Navigation.PushAsync(new OrderPage());
        }

        public new void OnAppearing()
        {
            IsBusy = true;
        }

        INavigation Navigation => Application.Current.MainPage.Navigation;

        public void ExeOpenPopup(View anchor)
        {
            var popup = new CartMoneyDetailPopup
            {
                Anchor = anchor
            };
            Navigation.ShowPopup(popup);
        }
    }
}
