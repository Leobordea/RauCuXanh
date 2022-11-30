using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using RauCuXanh.Models;
using RauCuXanh.Views.HomePageViews;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;

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
            _ = ExeLoadCart();
        }

        async Task ExeLoadCart()
        {
            IsBusy = true;
            await Task.Delay(2000);
            CartProducts.Clear();

            foreach (Raucu rc in Products)
            {
                CartProducts.Add(rc);
            }
            IsBusy = false;
        }

        INavigation Navigation => Application.Current.MainPage.Navigation;

        public void ExeOpenPopup(View anchor)
        {
            var popup = new CartMoneyDetailPopup();
            popup.Anchor = anchor;
            Navigation.ShowPopup(popup);
        }

        public void ExeIncrease(int quantity)
        {

        }

        public void ExeDecrease(int quantity)
        {

        }
    }
}
