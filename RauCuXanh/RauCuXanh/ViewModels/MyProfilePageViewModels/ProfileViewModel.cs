using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using RauCuXanh.Views;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;

namespace RauCuXanh.ViewModels
{
    public class ProfileViewModel : BaseViewModel
    {
        public Command MyOrderButton { get; set; }
        public Command PersonalInformationButton { get; set; }
        public Command InsightsButton { get; set; }
        public Command ChangePasswordButton { get; set; }
        public INavigation Navigation { get; set; }
        public ProfileViewModel(INavigation navigation)
        {
            Title = "Thông tin của tôi";
            Navigation = navigation;
            MyOrderButton = new Command(async () => await NavigateToOrderPage());
            PersonalInformationButton = new Command(async () => await NavigateToPerInfoPage());
            InsightsButton = new Command(async () => await NavigateToInsights());
            ChangePasswordButton = new Command(async () => await NavigateToChangePassPage());
        }

        public async Task NavigateToOrderPage()
        {
            await Navigation.PushAsync(new Views.MyOrderViews.MyOrderPage());
        }

        public async Task NavigateToPerInfoPage()
        {
            await Navigation.PushAsync(new PersonalInformationPage());
        }

        public async Task NavigateToInsights()
        {
            await Navigation.PushAsync(new InsightsPage());
        }

        public async Task NavigateToChangePassPage()
        {
            await Navigation.PushAsync(new ChangePasswordPage());
        }
    }
}
