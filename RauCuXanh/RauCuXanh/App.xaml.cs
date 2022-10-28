using RauCuXanh.Services;
using RauCuXanh.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RauCuXanh
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            XF.Material.Forms.Material.Init(this);
            DependencyService.Register<MockDataStore>();
            MainPage = new NavigationPage(new LoginPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
