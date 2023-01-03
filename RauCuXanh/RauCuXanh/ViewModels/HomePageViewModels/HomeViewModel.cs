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

        private string _selectedCategory = "all";
        public string SelectedCategory
        {
            get { return _selectedCategory; }
            set { SetProperty(ref _selectedCategory, value); }
        }

        private string _labelTitle = "Các sản phẩm phổ biến trong ngày";
        public string LabelTitle
        {
            get { return _labelTitle; }
            set { SetProperty(ref _labelTitle, value); }
        }

        private string _selected1 = "#303030";
        public string Selected1
        {
            get { return _selected1; }
            set { SetProperty(ref _selected1, value); }
        }
        private string _selected2 = "#F5F5F5";
        public string Selected2
        {
            get { return _selected2; }
            set { SetProperty(ref _selected2, value); }
        }
        private string _selected3 = "#F5F5F5";
        public string Selected3
        {
            get { return _selected3; }
            set { SetProperty(ref _selected3, value); }
        }
        private string _selected4 = "#F5F5F5";
        public string Selected4
        {
            get { return _selected4; }
            set { SetProperty(ref _selected4, value); }
        }
        private string _selected5 = "#F5F5F5";
        public string Selected5
        {
            get { return _selected5; }
            set { SetProperty(ref _selected5, value); }
        }

        public ObservableCollection<Raucu> Raucus { get; set; }
        public Command LoadRaucusCommand { get; }
        public Command ButtonCommand { get; set; }
        public Command NavigateToDetailPage { get; set; }
        public Command PerformSearch { get; set; }
        public Command NavToProfile { get; set; }
        public Command NavToCart { get; set; }
        public Command AddToCart { get; set; }

        public Command LoadTatcaCommand { get; set; }
        public Command LoadRauCommand { get; set; }
        public Command LoadCuCommand { get; set; }
        public Command LoadGiaviCommand { get; set; }
        public Command LoadTraicayCommand { get; set; }

        public string ImageSource { get; set; }

        public HomeViewModel()
        {
            Title = "Trang chủ";
            Raucus = new ObservableCollection<Raucu>();
            LoadRaucusCommand = new Command(async () => await ExecuteLoadRaucusCommand());
            ButtonCommand = new Command<object>(ExecuteButtonCommand);
            NavigateToDetailPage = new Command<Raucu>(ExecuteNavToDetailPage);
            PerformSearch = new Command(ExePerformSearch);
            NavToProfile = new Command(ExeNavToProfile);
            NavToCart = new Command(ExeNavToCart);
            AddToCart = new Command<Raucu>(ExeAddToCart);

            LoadTatcaCommand = new Command(ExeLoadTatcaCommand);
            LoadRauCommand = new Command(ExeLoadRauCommand);
            LoadCuCommand = new Command(ExeLoadCuCommand);
            LoadGiaviCommand = new Command(ExeGiaviCuCommand);
            LoadTraicayCommand = new Command(ExeTraicayCuCommand);
        }

        public void ExeLoadTatcaCommand()
        {
            SelectedCategory = "all";
            LabelTitle = "Các sản phẩm phổ biến trong ngày";
            Selected1 = "#303030";
            Selected2 = "#F5F5F5";
            Selected3 = "#F5F5F5";
            Selected4 = "#F5F5F5";
            Selected5 = "#F5F5F5";
            IsBusy = true;
        }

        public void ExeLoadRauCommand()
        {
            SelectedCategory = "rau";
            LabelTitle = "Các loại rau";
            Selected1 = "#F5F5F5";
            Selected2 = "#303030";
            Selected3 = "#F5F5F5";
            Selected4 = "#F5F5F5";
            Selected5 = "#F5F5F5";
            IsBusy = true;
        }

        public void ExeLoadCuCommand()
        {
            SelectedCategory = "cu";
            LabelTitle = "Các loại củ";
            Selected1 = "#F5F5F5";
            Selected2 = "#F5F5F5";
            Selected3 = "#303030";
            Selected4 = "#F5F5F5";
            Selected5 = "#F5F5F5";
            IsBusy = true;
        }

        public void ExeGiaviCuCommand()
        {
            SelectedCategory = "giavi";
            LabelTitle = "Các loại gia vị";
            Selected1 = "#F5F5F5";
            Selected2 = "#F5F5F5";
            Selected3 = "#F5F5F5";
            Selected4 = "#303030";
            Selected5 = "#F5F5F5";
            IsBusy = true;
        }

        public void ExeTraicayCuCommand()
        {
            SelectedCategory = "traicay";
            LabelTitle = "Các loại trái cây";
            Selected1 = "#F5F5F5";
            Selected2 = "#F5F5F5";
            Selected3 = "#F5F5F5";
            Selected4 = "#F5F5F5";
            Selected5 = "#303030";
            IsBusy = true;
        }

        public async Task ExecuteLoadRaucusCommand()
        {
            IsBusy = true;
            try
            {
                Raucus.Clear();
                var apiClient = RestService.For<IRaucuApi>(RestClient.BaseUrl);
                var raucus = await apiClient.GetRaucuList();
                if (SelectedCategory == "all")
                {
                    foreach (var raucu in raucus)
                    {
                        raucu.PriceAfterDiscount = raucu.Price * (1 - raucu.Discount);
                        Raucus.Add(raucu);
                    }
                }
                else
                {
                    foreach (var raucu in raucus)
                    {
                        if (raucu.Raucu_type == SelectedCategory)
                        {
                            raucu.PriceAfterDiscount = raucu.Price * (1 - raucu.Discount);
                            Raucus.Add(raucu);
                        }
                    }
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

        public async void ExePerformSearch()
        {
            await App.Current.MainPage.Navigation.PushAsync(new Views.HomePageViews.SearchPage());
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

            var response = await cartService.createCart(new Cart() { quantity = 1, raucu_id = r.Id.ToString(), timestamp = DateTime.Now.ToString("yyyy-MM-dd"), user_id = "1" });
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
