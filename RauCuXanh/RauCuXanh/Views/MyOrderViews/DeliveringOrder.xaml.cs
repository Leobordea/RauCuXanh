using RauCuXanh.ViewModels.HomePageViewModels;
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
    public partial class DeliveringOrder : ContentPage
    {
        DeliveringOrderViewModel _viewmodel;
        public DeliveringOrder()
        {
            InitializeComponent();
            BindingContext = _viewmodel = new DeliveringOrderViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewmodel.OnAppearing();
        }
    }
}