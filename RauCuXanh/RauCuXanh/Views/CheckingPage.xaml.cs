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
    public partial class CheckingPage : ContentPage
    {
        public CheckingPage()
        {
            InitializeComponent();
            BindingContext = new CheckingPageViewModel(Navigation);
        }
        protected async override void OnAppearing() { 
            base.OnAppearing(); 
            await Task.Delay(2000);
            App.Current.MainPage = new AppShell();
            await Navigation.PushAsync(new AppShell());
        }
    }
}