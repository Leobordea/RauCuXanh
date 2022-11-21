using RauCuXanh.Models;
using RauCuXanh.ViewModels.NotificationViewModels;
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
    public partial class NotificationPage : ContentPage
    {
        NotificationViewModel _viewModel;

        public NotificationPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new NotificationViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}