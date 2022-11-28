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
        public HomePage()
        {
            InitializeComponent();
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
                var itemsource = _container.Products.Where(i => i.Name.ToLower().Contains(e.NewTextValue.ToLower()));
                SearchSuggestion.ItemsSource = itemsource;
                SearchSuggestion.HeightRequest = Math.Min(60 * itemsource.Count(), 240);
                SearchSuggestion.IsVisible = true;
            }
            SearchSuggestion.EndRefresh();
        }
    }
}