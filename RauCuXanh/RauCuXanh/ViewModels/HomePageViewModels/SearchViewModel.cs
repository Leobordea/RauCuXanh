using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace RauCuXanh.ViewModels.HomePageViewModels
{
    public class SearchViewModel : HomeViewModel
    {
        private string searchText;
        public string SearchText
        {
            get { return searchText; }
            set { SetProperty(ref searchText, value); }
        }

        public SearchViewModel() { }
        public SearchViewModel(string s)
        {
            Title = "Tìm kiếm";
            SearchText = s;
        }
    }
}
