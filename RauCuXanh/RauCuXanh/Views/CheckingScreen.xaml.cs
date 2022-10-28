using RauCuXanh.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RauCuXanh.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CheckingScreen : ContentPage
    {
        public CheckingScreen()
        {
            InitializeComponent();
            BindingContext = new CheckingScreenViewModel(Navigation);
        }
        protected async override void OnAppearing() { 
            base.OnAppearing(); 
            await Task.Delay(2000);
            App.Current.MainPage = new AppShell();
            await Navigation.PushAsync(new AppShell());
        }
    }
}