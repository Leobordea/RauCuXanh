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
    public partial class SearchPage : ContentPage
    {
        SearchViewModel _viewmodel;
        public SearchPage()
        {
            InitializeComponent();
        }
        public SearchPage(string s)
        {
            InitializeComponent();
            BindingContext = _viewmodel = new SearchViewModel(s);
        }
    }
}