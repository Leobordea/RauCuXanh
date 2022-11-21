using RauCuXanh.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RauCuXanh.ViewModels
{
    public class RegisterCompletedViewModel : BaseViewModel
    {
        public INavigation Navigation { get; set; }
        public Command BackToLoginBtn { get; }

        public RegisterCompletedViewModel(INavigation navigation)
        {
            Navigation = navigation;
            BackToLoginBtn = new Command(async() => await BackToLoginBtnClicked());
        }

        private async Task BackToLoginBtnClicked()
        {
            await Navigation.PopToRootAsync();
        }
    }
}
