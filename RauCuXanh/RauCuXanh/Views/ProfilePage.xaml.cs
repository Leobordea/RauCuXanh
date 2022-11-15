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
        public ProfilePage()
        {
            InitializeComponent();
            BindingContext = new ProfileViewModel(Navigation);
        }
        void Logout(object sender, EventArgs args)
        {
            App.Current.MainPage = new NavigationPage(new LoginPage())
            {
                BackgroundColor = Color.White
            };
        }
    }
}