using RauCuXanh.ViewModels;
using RauCuXanh.Views.HomePageViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RauCuXanh.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            BindingContext = new LoginViewModel(Navigation);
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if (Preferences.Get("RememberMe", false) && Preferences.Get("UID", -1) != -1)
            {
                App.Current.MainPage = new AppShell();
                await Navigation.PopToRootAsync();
            }
        }
    }
}