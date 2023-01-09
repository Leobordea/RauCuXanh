using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using RauCuXanh.Models;
using RauCuXanh.Services;
using Refit;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs;

namespace RauCuXanh.ViewModels.HomePageViewModels
{
    public class ShopViewModel : HomeViewModel
    {
        private Shop _shop;
        public Shop Shop { get { return _shop; } set { SetProperty(ref _shop, value); } }
        public ObservableCollection<Raucu> DangBan { get; set; }
        public ObservableCollection<Raucu> KhuyenMai { get; set; }
        public Command LoadShopCommand { get; set; }

        public Review Review { get; set; }
        public User User { get; set; }
        public List<string> Stars { get; set; }
        public ObservableCollection<ShopViewModel> ModelData { get; set; }


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

        private ObservableCollection<ShopViewModel> _mystars = new ObservableCollection<ShopViewModel>();
        public ObservableCollection<ShopViewModel> MyStars
        {
            get { return _mystars; }
            set { SetProperty(ref _mystars, value); }
        }

        public Command StarCommand { get; set; }
        public Command SendReview { get; set; }

        public ShopViewModel() { }
        public ShopViewModel(Shop s)
        {
            Title = s.Name;
            Shop = s;
            DangBan = new ObservableCollection<Raucu>();
            KhuyenMai = new ObservableCollection<Raucu>();
            LoadShopCommand = new Command(async () => await ExeLoadShopCommand());
            ModelData = new ObservableCollection<ShopViewModel>();
            Stars = new List<string>();
            StarCommand = new Command<Object>(ExeStarCommand);
            MyStars = new ObservableCollection<ShopViewModel>() {
                new ShopViewModel() { Source = "white_star.png", Id = 1 },
                new ShopViewModel() { Source = "white_star.png", Id = 2 },
                new ShopViewModel() { Source = "white_star.png", Id = 3 },
                new ShopViewModel() { Source = "white_star.png", Id = 4 },
                new ShopViewModel() { Source = "white_star.png", Id = 5 }
            };
            SendReview = new Command(ExeSendReview);
        }

        async Task ExeLoadShopCommand()
        {
            try
            {
                var raucuService = RestService.For<IRaucuApi>(RestClient.BaseUrl);
                var raucus = await raucuService.GetRaucuList();
                DangBan.Clear();
                KhuyenMai.Clear();
                foreach (Raucu r in raucus)
                {
                    if (r.Shop_id == Shop.Id)
                    {
                        r.PriceAfterDiscount = r.Price * (1 - r.Discount);
                        DangBan.Add(r);
                        if (r.Discount > 0)
                        {
                            KhuyenMai.Add(r);
                        }
                    }
                }

                ModelData.Clear();
                var shopService = RestService.For<IShopApi>(RestClient.BaseUrl);
                var reviewService = RestService.For<IReviewApi>(RestClient.BaseUrl);
                var reviews = await reviewService.GetReviews();

                var userClient = RestService.For<IUserApi>(RestClient.BaseUrl);
                foreach (var r in reviews)
                {
                    if (r.Review_type == "shop")
                    {
                        if (r.Shop_id == Shop.Id)
                        {
                            var user = await userClient.GetUserById(r.User_id);
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
                            ModelData.Add(new ShopViewModel() { Review = r, User = user, Stars = stars });
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

        public void ExeStarCommand(Object o)
        {
            int id = (int)o;
            switch (id)
            {
                case 1:
                    MyStars = new ObservableCollection<ShopViewModel>() {
                        new ShopViewModel() { Source = "star.png", Id = 1 },
                        new ShopViewModel() { Source = "white_star.png", Id = 2 },
                        new ShopViewModel() { Source = "white_star.png", Id = 3 },
                        new ShopViewModel() { Source = "white_star.png", Id = 4 },
                        new ShopViewModel() { Source = "white_star.png", Id = 5 }
                    };
                    break;
                case 2:
                    MyStars = new ObservableCollection<ShopViewModel>() {
                        new ShopViewModel() { Source = "star.png", Id = 1 },
                        new ShopViewModel() { Source = "star.png", Id = 2 },
                        new ShopViewModel() { Source = "white_star.png", Id = 3 },
                        new ShopViewModel() { Source = "white_star.png", Id = 4 },
                        new ShopViewModel() { Source = "white_star.png", Id = 5 }
                    };
                    break;
                case 3:
                    MyStars = new ObservableCollection<ShopViewModel>() {
                        new ShopViewModel() { Source = "star.png", Id = 1 },
                        new ShopViewModel() { Source = "star.png", Id = 2 },
                        new ShopViewModel() { Source = "star.png", Id = 3 },
                        new ShopViewModel() { Source = "white_star.png", Id = 4 },
                        new ShopViewModel() { Source = "white_star.png", Id = 5 }
                    };
                    break;
                case 4:
                    MyStars = new ObservableCollection<ShopViewModel>() {
                        new ShopViewModel() { Source = "star.png", Id = 1 },
                        new ShopViewModel() { Source = "star.png", Id = 2 },
                        new ShopViewModel() { Source = "star.png", Id = 3 },
                        new ShopViewModel() { Source = "star.png", Id = 4 },
                        new ShopViewModel() { Source = "white_star.png", Id = 5 }
                    };
                    break;
                case 5:
                    MyStars = new ObservableCollection<ShopViewModel>() {
                        new ShopViewModel() { Source = "star.png", Id = 1 },
                        new ShopViewModel() { Source = "star.png", Id = 2 },
                        new ShopViewModel() { Source = "star.png", Id = 3 },
                        new ShopViewModel() { Source = "star.png", Id = 4 },
                        new ShopViewModel() { Source = "star.png", Id = 5 }
                    };
                    break;
            }
        }

        public async void ExeSendReview()
        {
            bool firstReview = true;
            foreach (var i in ModelData)
            {
                if (i.Review.User_id == 1)
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
                    var reviewData = new Dictionary<string, object>() { { "comments", MyReview }, { "user_id", 1 }, { "shop_id", Shop.Id }, { "stars", starcount }, {"review_type", "shop" } };
                    if (firstReview)
                    {
                        var response = await reviewService.CreateReview(reviewData);
                        if (response.StatusCode == System.Net.HttpStatusCode.Created || response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            await ExeLoadShopCommand();
                        }
                    } else
                    {
                        var response = await reviewService.UpdateReview(reviewData);
                        if (response.StatusCode == System.Net.HttpStatusCode.Created || response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            await ExeLoadShopCommand();
                        }
                    }
                }
                catch (Exception ex)
                {
                    await MaterialDialog.Instance.AlertAsync(message: ex.Message);
                }
            }
        }
    }
}
