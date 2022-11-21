using RauCuXanh.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace RauCuXanh.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {
        public INavigation Navigation { get; set; }
        public Command RegisterBtn { get; }
        public Command OnClickTermOfUse { get; }

        public RegisterViewModel(INavigation navigation)
        {
            Navigation = navigation;
            RegisterBtn = new Command(async() => await OnRegisterClicked());
            OnClickTermOfUse = new Command<string>(url => TermOfUse(url));
        }

        private async Task OnRegisterClicked()
        {
            await Navigation.PushAsync(new RegisterCompletedPage());
        }

        private async void TermOfUse(string url)
        {
            await Browser.OpenAsync(url);
        }
    }
}
