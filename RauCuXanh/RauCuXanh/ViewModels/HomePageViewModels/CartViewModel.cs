using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public ObservableCollection<Raucu> CartProducts { get; set; }
        public Command LoadCart { get; set; }
        public Command IncreaseQuantity { get; set; }
        public Command DecreaseQuantity { get; set; }
        public Command OpenPopup { get; set; } 
        public CartViewModel()
        {
            Title = "Giỏ hàng";
            CartProducts = new ObservableCollection<Raucu>();
            LoadCart = new Command(async () => await ExeLoadCart());
            IncreaseQuantity = new Command<int>(ExeIncrease);
            DecreaseQuantity = new Command<int>(ExeDecrease);
            OpenPopup = new Command<View>(ExeOpenPopup);
        }

        async Task ExeLoadCart()
        {
            IsBusy = true;
            try
            {
                CartProducts.Clear();
                var apiClient = RestService.For<ICartApi>(RestClient.BaseUrl);
                var obj = apiClient.GetCarts();

                foreach (Raucu rc in Raucus)
                {
                    CartProducts.Add(rc);
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

        INavigation Navigation => Application.Current.MainPage.Navigation;

        public void ExeOpenPopup(View anchor)
        {
            var popup = new CartMoneyDetailPopup
            {
                Anchor = anchor
            };
            Navigation.ShowPopup(popup);
        }

        public void ExeIncrease(int quantity)
        {

        }

        public void ExeDecrease(int quantity)
        {

        }

        public new void OnAppearing()
        {
            IsBusy = true;
        }
    }
}
