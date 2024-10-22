﻿using System;
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

        private bool _visible1 = true;
        public bool Visible1 { get { return _visible1; } set { SetProperty(ref _visible1, value); } }

        private bool _visible2 = false;
        public bool Visible2 { get { return _visible2; } set { SetProperty(ref _visible2, value); } }

        public Command SearchCommand { get; }
        public List<string> Recommendations { get; set; }
        public Command RecommendCommand { get; set; }

        public SearchViewModel()
        {
            Title = "Tìm kiếm";
            SearchResults = new ObservableCollection<Raucu>();
            SearchCommand = new Command(Search);
            Recommendations = new List<string>() { "Rau cải thìa", "Rau húng quế", "Súp lơ", "Rau bắp cải" };
            RecommendCommand = new Command<string>(ExeRecommendCommand);
            Task.Run(async () => { await ExecuteLoadRaucusCommand(); }).Wait();
        }

        public void ExeRecommendCommand(string str)
        {
            SearchText = str;
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
