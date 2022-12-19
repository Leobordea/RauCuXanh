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
    public partial class SpecificPage : ContentPage
    {
        SpecificViewModel _viewmodel;
        public SpecificPage()
        {
            InitializeComponent();
        }

        public SpecificPage(string str)
        {
            InitializeComponent();
            BindingContext = _viewmodel = new SpecificViewModel(str);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewmodel.OnAppearing();
        }
    }
}