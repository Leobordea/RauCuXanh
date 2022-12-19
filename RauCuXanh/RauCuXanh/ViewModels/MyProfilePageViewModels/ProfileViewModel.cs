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
    public class ProfileViewModel : INotifyPropertyChanged
    {
        private int userid = 1;

        private string username = string.Empty;
        public String Username
        {
            get => username;
            set
            {
                username = value;
                OnPropertyChanged();
            }
        }

        private string profile_pic = string.Empty;
        public String Profile_pic
        {
            get => profile_pic;
            set
            {
                profile_pic = value;
                OnPropertyChanged();
            }
        }

        private string email = string.Empty;

        public String Email
        {
            get => email;
            set
            {
                email = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public Command MyOrderButton { get; set; }
        public Command PersonalInformationButton { get; set; }
        public Command InsightsButton { get; set; }
        public Command ChangePasswordButton { get; set; }
        public string Title { get; }
        public INavigation Navigation { get; set; }
        public bool IsBusy { get; private set; }

        public ProfileViewModel(INavigation navigation)
        {
            IsBusy = false;
            Task.Run(async () => await LoadUser());

            Title = "Thông tin của tôi";
            Navigation = navigation;
            MyOrderButton = new Command(async () => await NavigateToOrderPage());
            PersonalInformationButton = new Command(async () => await NavigateToPerInfoPage());
            InsightsButton = new Command(async () => await NavigateToInsights());
            ChangePasswordButton = new Command(async () => await NavigateToChangePassPage());
        }

        public async Task LoadUser()
        {
            if (!IsBusy)
            {
                IsBusy = true;
                try
                {
                    var apiClient = RestService.For<IUserApi>(RestClient.BaseUrl);
                    var user = await apiClient.GetUserById(userid);
                    username = user.Username;
                    OnPropertyChanged(nameof(Username));
                    email = user.Email;
                    OnPropertyChanged(nameof(Email));
                    profile_pic = user.Profile_pic;
                    OnPropertyChanged(nameof(Profile_pic));

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
