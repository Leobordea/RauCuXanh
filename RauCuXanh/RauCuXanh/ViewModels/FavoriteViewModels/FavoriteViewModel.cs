using RauCuXanh.Models;
using RauCuXanh.Services;
using RauCuXanh.Views;
using Refit;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs;

namespace RauCuXanh.ViewModels.FavoriteViewModels
{
    public class FavoriteViewModel : BaseViewModel
    {
        public Command LoadBookmarksCommand { get; }

        public Command RemoveBookmark { get; set; }
        public Command NavToDetailPage { get; set; }
        public Command AddToCart { get; set; }

        public Collection<Bookmark> Bookmarks { get; }

        public ObservableCollection<Raucu> Raucus { get; }


        public FavoriteViewModel()
        {
            Title = "Favorite";
            Bookmarks = new Collection<Bookmark>();
            Raucus = new ObservableCollection<Raucu>();
            LoadBookmarksCommand = new Command(async () => await ExecuteLoadBookmarksCommand());


            RemoveBookmark = new Command<Raucu>(async (r) => await ExeRemoveBookmark(r));
            NavToDetailPage = new Command<Raucu>(async (r) => await ExecuteNavToDetailPage(r));
            AddToCart = new Command<Raucu>(async (r) => await ExeAddToCart(r));
        }

        async Task ExecuteLoadBookmarksCommand()
        {
            IsBusy = true;
            try
            {
                Bookmarks.Clear();
                Raucus.Clear();
                var apiClient = RestService.For<IBookmarkApi>(RestClient.BaseUrl);
                var raucuClient = RestService.For<IRaucuApi>(RestClient.BaseUrl);
                var bookmarks = await apiClient.GetBookmarks();
                foreach (var bookmark in bookmarks)
                {
                    if (bookmark.User_id == userid)
                    {
                        var raucu = await raucuClient.GetRaucuById(bookmark.Raucu_id);
                        raucu.PriceAfterDiscount = raucu.Price * (1 - raucu.Discount);
                        Raucus.Add(raucu);
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

        public async
        Task
ExeRemoveBookmark(Raucu r)
        {
            try
            {
                var bookmarkClient = RestService.For<IBookmarkApi>(RestClient.BaseUrl);
                Bookmark bm = new Bookmark()
                {
                    User_id = userid,
                    Raucu_id = r.Id
                };
                var response = await bookmarkClient.DeleteBookmark(bm);
                if (response.IsSuccessStatusCode)
                {
                    await MaterialDialog.Instance.SnackbarAsync(message: "Xóa thành công!",
                                                msDuration: MaterialSnackbar.DurationShort);
                    Raucus.Remove(r);
                }
            }
            catch (Exception ex)
            {
                await MaterialDialog.Instance.AlertAsync(message: ex.Message);
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
        }

        public async
        Task
ExeAddToCart(Raucu r)
        {
            try
            {

                var cartService = RestService.For<ICartApi>(RestClient.BaseUrl);
                var response = await cartService.CreateCart(new Cart() { Quantity = 1, Raucu_id = r.Id, User_id = userid });
                if (response.IsSuccessStatusCode)
                {
                    await MaterialDialog.Instance.SnackbarAsync(message: "Thêm vào giỏ hàng thành công!",
                                                msDuration: MaterialSnackbar.DurationShort);
                }
                else
                {
                    await MaterialDialog.Instance.AlertAsync(message: response.ReasonPhrase);
                }
            }
            catch (Exception ex)
            {
                await MaterialDialog.Instance.AlertAsync(message: ex.Message);
            }
        }
        public async Task ExecuteNavToDetailPage(Raucu p)
        {
            await App.Current.MainPage.Navigation.PushAsync(new Views.HomePageViews.ProductDetailPage(p));
        }
    }
}