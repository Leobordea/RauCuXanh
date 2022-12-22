using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RauCuXanh.Models;
using RauCuXanh.ViewModels.FavoriteViewModels;
using RauCuXanh.ViewModels.HomePageViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RauCuXanh.Views.HomePageViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CartPage : ContentPage
    {
        CartViewModel _viewModel;
        public CartPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new CartViewModel();
        }

        //public CartPage(Raucu r)
        //{
        //    InitializeComponent();
        //    BindingContext = _viewModel = new CartViewModel(r);
        //}

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {

        }
    }
}