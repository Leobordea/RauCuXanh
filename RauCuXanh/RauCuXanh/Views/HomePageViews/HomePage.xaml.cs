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
    public partial class HomePage : ContentPage
    {
        HomeViewModel _viewModel;

        public HomePage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new HomeViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            var _container = BindingContext as HomeViewModel;
            SearchSuggestion.BeginRefresh();

            if (string.IsNullOrWhiteSpace(e.NewTextValue))
            {
                SearchSuggestion.ItemsSource = null;
                SearchSuggestion.IsVisible = false;
            }
            else
            {
                var itemsource = _container.Raucus.Where(i => i.Name.ToLower().Contains(e.NewTextValue.ToLower()));
                SearchSuggestion.ItemsSource = itemsource;
                SearchSuggestion.HeightRequest = Math.Min(60 * itemsource.Count(), 240);
                SearchSuggestion.IsVisible = true;
            }
            SearchSuggestion.EndRefresh();
        }
    }
}