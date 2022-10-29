using RauCuXanh.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RauCuXanh.ViewModels
{
    public class ForgetPasswordViewModel : BaseViewModel
    {
        public Command LoginBtn { get; }
        public INavigation Navigation { get; set; }

        public ForgetPasswordViewModel(INavigation navigation)
        {
            Navigation = navigation;
            LoginBtn = new Command(async() => await OnLoginClicked());
        }

        public async Task OnLoginClicked()
        {
            await Navigation.PushAsync(new CheckingScreen());
        }
    }
}
