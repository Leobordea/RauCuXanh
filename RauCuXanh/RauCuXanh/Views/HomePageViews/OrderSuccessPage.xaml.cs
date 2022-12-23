using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RauCuXanh.ViewModels.HomePageViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RauCuXanh.Views.HomePageViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrderSuccessPage : ContentPage
    {
        OrderSuccessViewModel _viewmodel;
        public OrderSuccessPage()
        {
            InitializeComponent();
            BindingContext = _viewmodel = new OrderSuccessViewModel();
        }
    }
}