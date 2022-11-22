using RauCuXanh.Models;
using RauCuXanh.ViewModels.FavoriteViewModels;
using RauCuXanh.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RauCuXanh.Views
{
    public partial class FavoritePage : ContentPage
    {
        FavoriteViewModel _viewModel;

        public FavoritePage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new FavoriteViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}