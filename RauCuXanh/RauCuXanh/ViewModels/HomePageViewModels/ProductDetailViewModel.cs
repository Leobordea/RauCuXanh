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
using Xamarin.CommunityToolkit.UI.Views;
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
        public Command AddBookmark { get; set; }
        public Command AddToCart { get; set; }
        public Review Review { get; set; }
        public User User { get; set; }
        public List<string> Stars { get; set; }
        public ObservableCollection<ProductDetailViewModel> ModelData { get; set; }
        private float _averageStar = 0;
        public float AverageStar
        {
            get { return _averageStar; }
            set { SetProperty(ref _averageStar, value); }
        }

        private string _bookmarkIcon;
        public string BookmarkIcon
        {
            get { return _bookmarkIcon; }
            set
            {
                SetProperty(ref _bookmarkIcon, value);
            }
        }

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
            AddBookmark = new Command(ExeAddBookmark);
            AddToCart = new Command(ExeAddToCart);
            ModelData = new ObservableCollection<ProductDetailViewModel>();
            Stars = new List<string>();
        }

        async Task ExeLoadShopCommand()
        {
            IsBusy = true;
            try
            {
                ModelData.Clear();
                AverageStar = 0;
                BookmarkIcon = "bookmark_icon.png";

                var shopClient = RestService.For<IShopApi>(RestClient.BaseUrl);
                Shop = await shopClient.GetShopById(Raucu.Shop_id);

                var reviewClient = RestService.For<IReviewApi>(RestClient.BaseUrl);
                var reviews = await reviewClient.GetReviews();

                var userClient = RestService.For<IUserApi>(RestClient.BaseUrl);

                var bookmarkClient = RestService.For<IBookmarkApi>(RestClient.BaseUrl);
                var bookmarks = await bookmarkClient.GetBookmarks();
                foreach (var bookmark in bookmarks)
                {
                    if (bookmark.Raucu_id == Raucu.Id && bookmark.User_id == 1)
                    {
                        BookmarkIcon = "bookmarked_icon.png";
                        break;
                    }
                }

                foreach (var r in reviews)
                {
                    if (r.Review_type == "raucu")
                    {
                        if (r.Raucu_id == Raucu.Id)
                        {
                            var user = await userClient.GetUserById(r.User_id);
                            AverageStar = AverageStar + r.Stars;
                            var stars = new List<string>();
                            switch (r.Stars)
                            {
                                case 1:
                                    stars = new List<string>() { "Red" };
                                    break;
                                case 2:
                                    stars = new List<string>() { "Red", "Red" };
                                    break;
                                case 3:
                                    stars = new List<string>() { "Red", "Red", "Red" };
                                    break;
                                case 4:
                                    stars = new List<string>() { "Red", "Red", "Red", "Red" };
                                    break;
                                case 5:
                                    stars = new List<string>() { "Red", "Red", "Red", "Red", "Red" };
                                    break;
                            }
                            ModelData.Add(new ProductDetailViewModel() { Review = r, User = user, Stars = stars });
                        }
                    }
                }

                AverageStar = AverageStar / ModelData.Count;
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
            try
            {
                bool flag = false;
                var cartService = RestService.For<ICartApi>(RestClient.BaseUrl);
                var carts = await cartService.GetCarts();
                foreach (Cart cart in carts)
                {
                    if (cart.Raucu_id == Raucu.Id && cart.User_id == 1)
                    {
                        var response = await cartService.UpdateCart(new Cart() { Raucu_id = Raucu.Id, User_id = 1, Quantity = cart.Quantity + Quantity });
                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            await App.Current.MainPage.DisplayAlert("Thành công", "Thêm vào giỏ hàng thành công!", "OK");
                            flag = true;
                        }
                        break;
                    }
                }
                if (flag == false)
                {
                    var response = await cartService.CreateCart(new Cart()
                    {
                        Quantity = Quantity,
                        Raucu_id = Raucu.Id,
                        User_id = 1
                    });

                    if (response.StatusCode == System.Net.HttpStatusCode.Created)
                    {
                        await App.Current.MainPage.DisplayAlert("Thành công", "Thêm vào giỏ hàng thành công!", "OK");
                    }
                }
            }
            catch (Exception ex)
            {
                await MaterialDialog.Instance.AlertAsync(message: ex.Message);
            }
        }

        public async void ExeAddBookmark()
        {
            if (BookmarkIcon == "bookmark_icon.png")
            {
                try
                {
                    var bookmarkClient = RestService.For<IBookmarkApi>(RestClient.BaseUrl);
                    var response = await bookmarkClient.CreateBookmark(new Bookmark()
                    {
                        Raucu_id = Raucu.Id,
                        User_id = 1,
                    });
                    BookmarkIcon = "bookmarked_icon.png";
                }
                catch (Exception ex)
                {
                    await MaterialDialog.Instance.AlertAsync(message: ex.Message);
                }
            } else
            {
                try
                {
                    var bookmarkClient = RestService.For<IBookmarkApi>(RestClient.BaseUrl);
                    var response = await bookmarkClient.DeleteBookmark(new Bookmark()
                    {
                        Raucu_id = Raucu.Id,
                        User_id = 1,
                    });
                    BookmarkIcon = "bookmark_icon.png";
                }
                catch (Exception ex)
                {
                    await MaterialDialog.Instance.AlertAsync(message: ex.Message);
                }
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
        }
    }
}
