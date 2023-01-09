using RauCuXanh.Views.MyOrderViews;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace RauCuXanh.ViewModels.HomePageViewModels
{
    public class OrderSuccessViewModel
    {
        public Command NavToHomePage { get; set; }
        public Command NavToOrderDetailPage { get; set; }
        public OrderSuccessViewModel()
        {
            NavToHomePage = new Command(ExeNavToHomePage);
            NavToOrderDetailPage = new Command(ExeNavToOrderDetailPage);
        }

        public async void ExeNavToHomePage()
        {
            await App.Current.MainPage.Navigation.PopToRootAsync();
        }

        public async void ExeNavToOrderDetailPage()
        {
            await App.Current.MainPage.Navigation.PushAsync(new DeliveringOrder());
        }
    }
}
