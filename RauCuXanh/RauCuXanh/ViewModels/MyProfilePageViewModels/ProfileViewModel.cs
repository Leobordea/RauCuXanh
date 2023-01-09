using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using RauCuXanh.Models;
using RauCuXanh.Services;
using RauCuXanh.Views;
using Refit;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs;

namespace RauCuXanh.ViewModels
{
    public class ProfileViewModel : BaseViewModel
    {
        private int userid = 1;

        private User user;
        public User User
        {
            get { return user; }
            set { SetProperty(ref user, value); }
        }

        public Command LoadUserInfo { get; set; }
        public Command MyOrderButton { get; set; }
        public Command PersonalInformationButton { get; set; }
        public Command InsightsButton { get; set; }
        public Command ChangePasswordButton { get; set; }
        public INavigation Navigation { get; set; }

        public ProfileViewModel(INavigation navigation)
        {
            IsBusy = false;
            LoadUserInfo = new Command(async () => await LoadUser());
            Title = "Thông tin của tôi";
            Navigation = navigation;
            MyOrderButton = new Command(async () => await NavigateToOrderPage());
            PersonalInformationButton = new Command(async () => await NavigateToPerInfoPage());
            InsightsButton = new Command(async () => await NavigateToInsights());
            ChangePasswordButton = new Command(async () => await NavigateToChangePassPage());
        }

        public async Task LoadUser()
        {
            IsBusy = true;
            try
            {
                var apiClient = RestService.For<IUserApi>(RestClient.BaseUrl);
                var user = await apiClient.GetUserById(userid);
                User = user;
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

        internal void OnAppearing()
        {
            IsBusy = true;
        }
    }
}
