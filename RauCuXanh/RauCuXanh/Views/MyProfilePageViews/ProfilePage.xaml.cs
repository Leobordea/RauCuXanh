using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RauCuXanh.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RauCuXanh.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        ProfileViewModel _viewmodel;
        public ProfilePage()
        {
            InitializeComponent();
            BindingContext = _viewmodel = new ProfileViewModel(Navigation);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewmodel.OnAppearing();
        }

        void Logout(object sender, EventArgs args)
        {
            Preferences.Set("RememberMe", false);
            Preferences.Set("UID", -1);
            App.Current.MainPage = new NavigationPage(new LoginPage())
            {
                BackgroundColor = Color.White
            };
        }
    }
}