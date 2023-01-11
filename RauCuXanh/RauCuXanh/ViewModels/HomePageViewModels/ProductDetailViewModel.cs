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
using Xamarin.Forms.Internals;
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
        public Command LoadDetailCommand { get; set; }
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

        private string _myreview = string.Empty;
        public string MyReview
        {
            get { return _myreview; }
            set
            {
                SetProperty(ref _myreview, value);
            }
        }

        public string Source { get; set; }
        public int Id { get; set; }

        private ObservableCollection<ProductDetailViewModel> _mystars = new ObservableCollection<ProductDetailViewModel>();
        public ObservableCollection<ProductDetailViewModel> MyStars
        {
            get { return _mystars; }
            set { SetProperty(ref _mystars, value); }
        }

        public Command StarCommand { get; set; }
        public Command SendReview { get; set; }


        public ProductDetailViewModel() { }
        public ProductDetailViewModel(Raucu p)
        {
            Title = "Chi tiết sản phẩm";
            Raucu = p;
            LoadDetailCommand = new Command(async () => await ExeLoadDetailCommand());
            IncreaseQuantity = new Command(ExeIncreaseQuantity);
            DecreaseQuantity = new Command(ExeDecreaseQuantity);
            NavToShopCommand = new Command(ExeNavToShop);
            AddBookmark = new Command(ExeAddBookmark);
            AddToCart = new Command(ExeAddToCart);
            ModelData = new ObservableCollection<ProductDetailViewModel>();
            Stars = new List<string>();
            StarCommand = new Command<Object>(ExeStarCommand);
            MyStars = new ObservableCollection<ProductDetailViewModel>() {
                new ProductDetailViewModel() { Source = "white_star.png", Id = 1 },
                new ProductDetailViewModel() { Source = "white_star.png", Id = 2 },
                new ProductDetailViewModel() { Source = "white_star.png", Id = 3 },
                new ProductDetailViewModel() { Source = "white_star.png", Id = 4 },
                new ProductDetailViewModel() { Source = "white_star.png", Id = 5 }
            };
            SendReview = new Command(ExeSendReview);
        }



        async Task ExeLoadDetailCommand()
        {
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
                    if (bookmark.Raucu_id == Raucu.Id && bookmark.User_id == userid)
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
                            if (user.Profile_pic == null || user.Profile_pic == "")
                            {
                                user.Profile_pic = "profile.png";
                            }
                            AverageStar += r.Stars;
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
                    if (cart.Raucu_id == Raucu.Id && cart.User_id == userid)
                    {
                        var response = await cartService.UpdateCart(new Cart() { Raucu_id = Raucu.Id, User_id = userid, Quantity = cart.Quantity + Quantity });
                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            await MaterialDialog.Instance.AlertAsync(message: "Thêm vào giỏ hàng thành công.");
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
                        User_id = userid
                    });

                    if (response.StatusCode == System.Net.HttpStatusCode.Created)
                    {
                        await MaterialDialog.Instance.AlertAsync(message: "Thêm vào giỏ hàng thành công.");
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
                        User_id = userid,
                    });
                    BookmarkIcon = "bookmarked_icon.png";
                }
                catch (Exception ex)
                {
                    await MaterialDialog.Instance.AlertAsync(message: ex.Message);
                }
            }
            else
            {
                try
                {
                    var bookmarkClient = RestService.For<IBookmarkApi>(RestClient.BaseUrl);
                    var response = await bookmarkClient.DeleteBookmark(new Bookmark()
                    {
                        Raucu_id = Raucu.Id,
                        User_id = userid,
                    });
                    BookmarkIcon = "bookmark_icon.png";
                }
                catch (Exception ex)
                {
                    await MaterialDialog.Instance.AlertAsync(message: ex.Message);
                }
            }
        }

        public void ExeStarCommand(Object o)
        {
            int id = (int)o;
            switch (id)
            {
                case 1:
                    MyStars = new ObservableCollection<ProductDetailViewModel>() {
                        new ProductDetailViewModel() { Source = "star.png", Id = 1 },
                        new ProductDetailViewModel() { Source = "white_star.png", Id = 2 },
                        new ProductDetailViewModel() { Source = "white_star.png", Id = 3 },
                        new ProductDetailViewModel() { Source = "white_star.png", Id = 4 },
                        new ProductDetailViewModel() { Source = "white_star.png", Id = 5 }
                    };
                    break;
                case 2:
                    MyStars = new ObservableCollection<ProductDetailViewModel>() {
                        new ProductDetailViewModel() { Source = "star.png", Id = 1 },
                        new ProductDetailViewModel() { Source = "star.png", Id = 2 },
                        new ProductDetailViewModel() { Source = "white_star.png", Id = 3 },
                        new ProductDetailViewModel() { Source = "white_star.png", Id = 4 },
                        new ProductDetailViewModel() { Source = "white_star.png", Id = 5 }
                    };
                    break;
                case 3:
                    MyStars = new ObservableCollection<ProductDetailViewModel>() {
                        new ProductDetailViewModel() { Source = "star.png", Id = 1 },
                        new ProductDetailViewModel() { Source = "star.png", Id = 2 },
                        new ProductDetailViewModel() { Source = "star.png", Id = 3 },
                        new ProductDetailViewModel() { Source = "white_star.png", Id = 4 },
                        new ProductDetailViewModel() { Source = "white_star.png", Id = 5 }
                    };
                    break;
                case 4:
                    MyStars = new ObservableCollection<ProductDetailViewModel>() {
                        new ProductDetailViewModel() { Source = "star.png", Id = 1 },
                        new ProductDetailViewModel() { Source = "star.png", Id = 2 },
                        new ProductDetailViewModel() { Source = "star.png", Id = 3 },
                        new ProductDetailViewModel() { Source = "star.png", Id = 4 },
                        new ProductDetailViewModel() { Source = "white_star.png", Id = 5 }
                    };
                    break;
                case 5:
                    MyStars = new ObservableCollection<ProductDetailViewModel>() {
                        new ProductDetailViewModel() { Source = "star.png", Id = 1 },
                        new ProductDetailViewModel() { Source = "star.png", Id = 2 },
                        new ProductDetailViewModel() { Source = "star.png", Id = 3 },
                        new ProductDetailViewModel() { Source = "star.png", Id = 4 },
                        new ProductDetailViewModel() { Source = "star.png", Id = 5 }
                    };
                    break;
            }
        }

        public async void ExeSendReview()
        {
            bool firstReview = true;
            foreach (var i in ModelData)
            {
                if (i.Review.User_id == userid)
                {
                    firstReview = false;
                    break;
                }
            }
            var starcount = 0;
            foreach (var item in MyStars)
            {
                if (item.Source == "star.png")
                {
                    starcount++;
                }
            }
            if (!string.IsNullOrEmpty(MyReview) && starcount > 0)
            {
                try
                {
                    var reviewService = RestService.For<IReviewApi>(RestClient.BaseUrl);
                    var reviewData = new Dictionary<string, object>() { { "comments", MyReview }, { "user_id", userid }, { "raucu_id", Raucu.Id }, { "stars", starcount }, { "review_type", "raucu" } }; var response = firstReview ? await reviewService.CreateReview(reviewData) : await reviewService.UpdateReview(reviewData);
                    if (response.StatusCode == System.Net.HttpStatusCode.Created || response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        await ExeLoadDetailCommand();
                    }
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
