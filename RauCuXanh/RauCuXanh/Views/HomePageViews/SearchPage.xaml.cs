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

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchResult.BeginRefresh();
            
            if (string.IsNullOrWhiteSpace(e.NewTextValue))
            {
                SearchResult.ItemsSource = null;
                SearchResult.IsVisible = false;
                SearchSuggestion.IsVisible = true;
            }
            else
            {
                var itemsource = _viewmodel.Raucus.Where(i => i.Name.ToLower().Contains(e.NewTextValue.ToLower()));
                SearchResult.ItemsSource = itemsource;
                SearchResult.IsVisible = true;
                SearchSuggestion.IsVisible = false;
            }
            SearchResult.EndRefresh();
        }

        private void MaterialButton_Clicked(object sender, EventArgs e)
        {
            var btn = sender as Button;
            searchBar.Text = btn.Text;
        }
    }
}