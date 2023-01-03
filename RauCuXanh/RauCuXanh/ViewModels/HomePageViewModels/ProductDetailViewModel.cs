using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RauCuXanh.Models;
using RauCuXanh.Services;
using RauCuXanh.Views.HomePageViews;
using Refit;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs;
using static System.Net.WebRequestMethods;

namespace RauCuXanh.ViewModels.HomePageViewModels
{
    public class ProductDetailViewModel : BaseViewModel
    {
        private Raucu _raucu;
        public Raucu Raucu
        {
            get { return _raucu; }
            set { SetProperty(ref _raucu, value); }
        }

        private int _quantity = 1;
        public int Quantity
        {
            get { return _quantity; }
            set
            {
                if (value >= 1)
                    SetProperty(ref _quantity, value);
            }
        }

        private Shop _shop;
        public Shop Shop
        {
            get { return _shop; }
            set { SetProperty(ref _shop, value); }
        }
        public Command LoadShopCommand { get; set; }
        public Command IncreaseQuantity { get; set; }
        public Command DecreaseQuantity { get; set; }
        public Command NavToShopCommand { get; set; }
        public Command AddToCart { get; set; }
        public Command BuyNow { get; set; }
        public Review Review { get; set; }
        public User User { get; set; }
        public List<string> Stars { get; set; }
        public ObservableCollection<ProductDetailViewModel> ModelData { get; set; }

        private string _myreview;
        public string MyReview
        {
            get { return _myreview; }
            set
            {
                SetProperty(ref _myreview, value);
            }
        }


        public ProductDetailViewModel() { }
        public ProductDetailViewModel(Raucu p)
        {
            Title = "Chi tiết sản phẩm";
            Raucu = p;
            LoadShopCommand = new Command(async () => await ExeLoadShopCommand());
            IncreaseQuantity = new Command(ExeIncreaseQuantity);
            DecreaseQuantity = new Command(ExeDecreaseQuantity);
            NavToShopCommand = new Command(ExeNavToShop);
            AddToCart = new Command(ExeAddToCart);
            BuyNow = new Command(ExeBuyNow);
            ModelData = new ObservableCollection<ProductDetailViewModel>();
            Stars = new List<string>();
        }

        async Task ExeLoadShopCommand()
        {
            IsBusy = true;
            try
            {
                ModelData.Clear();

                var shopClient = RestService.For<IShopApi>(RestClient.BaseUrl);
                Shop = await shopClient.GetShopById(Raucu.Shop_id);

                //var reviewClient = RestService.For<IReviewApi>(RestClient.BaseUrl);
                //var reviews = await reviewClient.GetReviews();

                var userClient = RestService.For<IUserApi>(RestClient.BaseUrl);

                var reviewSerview = new ReviewService();
                var reviews = await reviewSerview.getReviews();


                foreach (var r in reviews)
                {
                    if (r.Review_type == "raucu")
                    {
                        if (r.Raucu_id == Raucu.Id)
                        {
                            var user = await userClient.GetUserById(r.User_id);
                            var stars = new List<string>();
                            switch (r.Stars)
                            {
                                case 1:
                                    stars = new List<string>() { "Red"};
                                    break;
                                case 2:
                                    stars = new List<string>() { "Red", "Red"};
                                    break;
                                case 3:
                                    stars = new List<string>() { "Red", "Red", "Red"};
                                    break;
                                case 4:
                                    stars = new List<string>() { "Red", "Red", "Red", "Red"};
                                    break;
                                case 5:
                                    stars = new List<string>() { "Red", "Red", "Red", "Red", "Red" };
                                    break;
                            }
                            ModelData.Add(new ProductDetailViewModel() { Review = r, User = user, Stars = stars});
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

        public void ExeIncreaseQuantity()
        {
            Quantity++;
        }

        public void ExeDecreaseQuantity()
        {
            Quantity--;
        }

        public async void ExeNavToShop()
        {
            await App.Current.MainPage.Navigation.PushAsync(new ShopPage(Shop));
        }

        public async void ExeAddToCart()
        {
            var cartService = new CartService();

            var response = await cartService.createCart(new Cart() 
            { 
                quantity = Quantity, 
                raucu_id = Raucu.Id.ToString(), 
                timestamp = DateTime.Now.ToString("yyyy-MM-dd"), 
                user_id = "1" 
            });
            if (response.StatusCode == System.Net.HttpStatusCode.Created)
            {
                await App.Current.MainPage.DisplayAlert("Thành công", "Thêm vào giỏ hàng thành công!", "OK");
            } else
            {
                await App.Current.MainPage.DisplayAlert("Lỗi", "Có lỗi xảy ra!", "OK");
            }
        }

        public async void ExeBuyNow()
        {
            var cartService = new CartService();

            var response = await cartService.createCart(new Cart() { quantity = Quantity, raucu_id = Raucu.Id.ToString(), timestamp = DateTime.Now.ToString("yyyy-MM-dd"), user_id = "1" });
            if (response.StatusCode == System.Net.HttpStatusCode.Created)
            {
                await App.Current.MainPage.Navigation.PushAsync(new CartPage());
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Lỗi", "Có lỗi xảy ra!", "OK");
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
        }
    }
}
