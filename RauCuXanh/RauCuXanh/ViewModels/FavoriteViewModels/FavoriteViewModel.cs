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
        private ObservableCollection<FavoriteViewModel> _modelData;
        public ObservableCollection<FavoriteViewModel> ModelData
        {
            get { return _modelData; }
            set { SetProperty(ref _modelData, value); }
        }

        public Raucu Raucu { get; set; }
        public Bookmark Bookmark { get; set; }

        public Command AddToCart { get; set; }

        public FavoriteViewModel()
        {
            Title = "Favorite";
            ModelData = new ObservableCollection<FavoriteViewModel>();
            LoadBookmarksCommand = new Command(async () => await ExecuteLoadBookmarksCommand());
            RemoveBookmark = new Command<Bookmark>(ExeRemoveBookmark);
            AddToCart = new Command<Raucu>(ExeAddToCart);
        }

        async Task ExecuteLoadBookmarksCommand()
        {
            IsBusy = true;
            try
            {
                ModelData.Clear();
                var apiClient = RestService.For<IBookmarkApi>(RestClient.BaseUrl);
                var raucuClient = RestService.For<IRaucuApi>(RestClient.BaseUrl);
                var bookmarks = await apiClient.GetBookmarks();
                foreach (var bookmark in bookmarks)
                {
                    if (bookmark.User_id == 1)
                    {
                        ModelData.Add(new FavoriteViewModel() { Raucu = await raucuClient.GetRaucuById(bookmark.Raucu_id), Bookmark = bookmark });
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

        public async void ExeRemoveBookmark(Bookmark bm)
        {
            var bookmarkClient = RestService.For<IBookmarkApi>(RestClient.BaseUrl);
            var response = await bookmarkClient.DeleteBookmark(bm);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                await ExecuteLoadBookmarksCommand();
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
        }

        public async void ExeAddToCart(Raucu r)
        {
            var cartService = RestService.For<ICartApi>(RestClient.BaseUrl);

            var response = await cartService.CreateCart(new Cart() { Quantity = 1, Raucu_id = r.Id, User_id = 1 });
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