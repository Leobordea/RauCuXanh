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
        public Command LoadRaucusCommand { get; }
        public Command ButtonCommand { get; set; }
        public Command NavigateToDetailPage { get; set; }
        public Command PerformSearch { get; set; }
        public Command NavToProfile { get; set; }
        public Command NavToCart { get; set; }
        public Command AddToCart { get; set; }

        public HomeViewModel()
        {
            Title = "Trang chủ";
            Raucus = new ObservableCollection<Raucu>();
            LoadRaucusCommand = new Command(async () => await ExecuteLoadRaucusCommand());
            ButtonCommand = new Command<object>(ExecuteButtonCommand);
            NavigateToDetailPage = new Command<Raucu>(ExecuteNavToDetailPage);
            PerformSearch = new Command<string>(ExePerformSearch);
            NavToProfile = new Command(ExeNavToProfile);
            NavToCart = new Command(ExeNavToCart);
            AddToCart = new Command<Raucu>(ExeAddToCart);
        }

        public async Task ExecuteLoadRaucusCommand()
        {
            IsBusy = true;
            try
            {
                Raucus.Clear();
                var raucuService = new RaucuService();
                var raucus = await raucuService.getRaucuList();
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

        public async void ExeAddToCart(Raucu r)
        {
            var cartService = new CartService();

            var response = await cartService.createCart(new Cart() { quantity = 1, raucu_id = r.Id, timestamp = DateTime.Now.ToString("yyyy-MM-dd"), user_id = "1" });
            if (response.StatusCode == System.Net.HttpStatusCode.Created)
            {
                await App.Current.MainPage.DisplayAlert("Thành công", "Thêm vào giỏ hàng thành công!", "OK");
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Lỗi", "Có lỗi xảy ra!", "OK");
            }
        }
    }
}
