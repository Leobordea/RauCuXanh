using RauCuXanh.Models;
using RauCuXanh.ViewModels.MyOrderViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RauCuXanh.Views.MyOrderViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrderDetailPage : ContentPage
    {
        OrderDetailViewModel _viewmodel;
        public OrderDetailPage()
        {
            InitializeComponent();
        }

        public OrderDetailPage(Receipt r)
        {
            InitializeComponent();
            BindingContext = _viewmodel = new OrderDetailViewModel(r);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewmodel.OnAppearing();
        }
    }
}