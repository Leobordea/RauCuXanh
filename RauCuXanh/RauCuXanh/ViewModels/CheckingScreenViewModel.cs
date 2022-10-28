using RauCuXanh.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RauCuXanh.ViewModels
{
    public class CheckingScreenViewModel : BaseViewModel
    {
        public INavigation Navigation { get; set; }

        public CheckingScreenViewModel(INavigation navigation)
        {
            App.Current.MainPage.DisplayAlert("Alert", "You have been alerted", "OK");
            Navigation = navigation;
            Task.Run(async () => await CheckAndNavigate());
        }

        private async Task CheckAndNavigate()
        {
            await Navigation.PushAsync(new AppShell());
        }
    }
}
