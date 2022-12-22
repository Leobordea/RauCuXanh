using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using RauCuXanh.Models;
using Xamarin.Forms;

namespace RauCuXanh.ViewModels.HomePageViewModels
{
    public class SearchViewModel : HomeViewModel
    {
        private string searchText;
        public string SearchText
        {
            get { return searchText; }
            set
            {
                SetProperty(ref searchText, value);
            }
        }

        public ObservableCollection<Raucu> SearchResults { get; set; }

        private bool _visible1 = false;
        public bool Visible1 { get { return _visible1; } set { SetProperty(ref _visible1, value); } }

        private bool _visible2 = false;
        public bool Visible2 { get { return _visible2; } set { SetProperty(ref _visible2, value); } }

        public Command SearchCommand { get; }

        public SearchViewModel() { }
        public SearchViewModel(string s)
        {
            Title = "Tìm kiếm";
            SearchResults = new ObservableCollection<Raucu>();
            SearchCommand = new Command(Search);
            Task.Run(async () => { await ExecuteLoadRaucusCommand(); }).Wait();
            SearchText = s;
            Task.Run(Search);
        }

        void Search()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                SearchResults.Clear();
                Visible1 = true;
                Visible2 = false;
            }
            else
            {
                SearchResults.Clear();
                var result = Raucus.Where(i => i.Name.ToLower().Contains(SearchText.ToLower())).ToList();
                foreach (Raucu r in result)
                {
                    SearchResults.Add(r);
                }
                Visible1 = false;
                Visible2 = true;
            }
        }
    }
}
