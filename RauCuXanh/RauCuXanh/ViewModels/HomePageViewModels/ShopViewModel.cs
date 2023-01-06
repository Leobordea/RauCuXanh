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
        public ObservableCollection<ProductDetailViewModel> ModelData { get; set; }

        public ShopViewModel() { }
        public ShopViewModel(Shop s) 
        {
            Title = s.Name;
            Shop = s;
            DangBan = new ObservableCollection<Raucu>();
            KhuyenMai = new ObservableCollection<Raucu>();
            LoadShopCommand = new Command(async () => await ExeLoadShopCommand());
            ModelData = new ObservableCollection<ProductDetailViewModel>();
            Stars = new List<string>();
        }

        async Task ExeLoadShopCommand()
        {
            IsBusy = true;
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
                            ModelData.Add(new ProductDetailViewModel() { Review = r, User = user, Stars = stars });
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
    }
}
