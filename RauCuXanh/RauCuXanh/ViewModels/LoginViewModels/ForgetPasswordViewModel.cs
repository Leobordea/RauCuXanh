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
        public Command ResetPassBtn { get; }
        public INavigation Navigation { get; set; }

        public ForgetPasswordViewModel(INavigation navigation)
        {
            Navigation = navigation;
            ResetPassBtn = new Command(async() => await OnResetPassword());
        }

        public async Task OnResetPassword()
        {
            await Navigation.PushAsync(new RecoveryMailSentPage());
        }
    }
}
