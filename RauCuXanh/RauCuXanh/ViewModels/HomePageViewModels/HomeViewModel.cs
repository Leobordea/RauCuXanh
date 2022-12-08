using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using RauCuXanh.Models;
using RauCuXanh.Services;
using RauCuXanh.Views.HomePageViews;
using Refit;
using Xamarin.Essentials;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs;

namespace RauCuXanh.ViewModels.HomePageViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        private Raucu _selectedProduct;
        public Raucu SelectedProduct
        {
            get { return _selectedProduct; }
            set 
            {
                if (value != null)
                {
                    Application.Current.MainPage.Navigation.PushAsync(new ProductDetailPage(value));
                    value = null;
                };
                SetProperty(ref _selectedProduct, value); 
            }
        }
        public ObservableCollection<Raucu> Raucus { get; set; }
        public ObservableCollection<Raucu> SuggestionCollection { get; set; }
        public ObservableCollection<Receipt_list> ReceiptList { get; set; }
        public Command LoadRaucusCommand { get; }
        public Command ButtonCommand { get; set; }
        public Command NavigateToDetailPage { get; set; }
        public Command PerformSearch { get; set; }
        public Command NavToProfile { get; set; }
        public Command NavToCart { get; set; }

        public HomeViewModel()
        {
            Title = "Trang chủ";
            Raucus = new ObservableCollection<Raucu>();
            LoadRaucusCommand = new Command(async () => await ExecuteLoadRaucusCommand());
            //_ = ExecuteLoadRaucusCommand();
            ButtonCommand = new Command<object>(ExecuteButtonCommand);
            NavigateToDetailPage = new Command<Raucu>(ExecuteNavToDetailPage);
            SuggestionCollection = new ObservableCollection<Raucu>();
            PerformSearch = new Command<string>(ExePerformSearch);
            NavToProfile = new Command(ExeNavToProfile);
            NavToCart = new Command(ExeNavToCart);
            ReceiptList = new ObservableCollection<Receipt_list>();
        }

        public async Task ExecuteLoadRaucusCommand()
        {
            IsBusy = true;
            try
            {
                Raucus.Clear();
                var apiClient = RestService.For<IRaucuApi>(RestClient.BaseUrl);
                var raucus = await apiClient.GetRaucuList();
                foreach (var raucu in raucus)
                {
                    Raucus.Add(raucu);
                }
            }
            catch (Exception ex)
            {
                await MaterialDialog.Instance.AlertAsync(message: ex.Message);
            }
            finally
            {
                IsBusy = false;
            }
        }
        public void OnAppearing()
        {
            IsBusy = true;
        }

        public async void ExecuteButtonCommand(object o)
        {
            var button = o as Button;
            await App.Current.MainPage.Navigation.PushAsync(new SpecificPage(button.Text));
        }

        public async void ExecuteNavToDetailPage(Raucu p)
        {
            await App.Current.MainPage.Navigation.PushAsync(new Views.HomePageViews.ProductDetailPage(p));
        }

        public async void ExePerformSearch(string s)
        {
            await App.Current.MainPage.Navigation.PushAsync(new Views.HomePageViews.SearchPage(s));
        }

        public async void ExeNavToProfile()
        {
            await Shell.Current.GoToAsync("//ProfilePage");
        }

        public async void ExeNavToCart()
        {
            await App.Current.MainPage.Navigation.PushAsync(new Views.HomePageViews.CartPage());
        }
    }
}
