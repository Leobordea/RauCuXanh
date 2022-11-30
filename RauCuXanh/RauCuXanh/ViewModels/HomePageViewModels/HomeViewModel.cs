using System.Collections.Generic;
using System.Collections.ObjectModel;
using RauCuXanh.Models;
using RauCuXanh.Views.HomePageViews;
using Xamarin.Essentials;
using Xamarin.Forms;

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
        public ObservableCollection<Raucu> Products { get; set; }
        public ObservableCollection<Raucu> SuggestionCollection { get; set; }
        public ObservableCollection<Receipt_list> ReceiptList { get; set; }
        public ObservableCollection<Shop> Shops { get; set; }
        public Command ButtonCommand { get; set; }
        public Command NavigateToDetailPage { get; set; }
        public Command PerformSearch { get; set; }
        public Command NavToProfile { get; set; }
        public Command NavToCart { get; set; }

        public HomeViewModel()
        {
            Title = "Trang chủ";
            Products = new ObservableCollection<Raucu>()
            {
                new Raucu(1, "Nấm kim châm (500g)", "Nấm kim châm 500g", "Rau", 50000, 20, "https://cdn.tgdd.vn/Files/2020/12/08/1312301/11-cach-lam-nam-kim-cham-xao-thom-ngon-don-gian-tai-nha-202201071113027145.jpg", 1, 1, "24-11-2022"),
                new Raucu(2, "Ớt (500g)", "Ớt 500g", "Gia vị", 50000, 10, "https://suckhoedoisong.qltns.mediacdn.vn/thumb_w/640/324455921873985536/2022/6/18/cach-an-che-bien-bao-quan-ot-2-1655566236587196946971.jpg", 2, 1, "24-11-2022"),
                new Raucu(3, "Cà rốt (500g)", "Cà rốt 500g", "Củ", 50000, 20, "https://image.thanhnien.vn/w1024/Uploaded/2022/wpxlcqjwq/2022_03_03/ca-rot-2275.jpg", 3, 1, "24-11-2022"),
                new Raucu(4, "Nấm kim châm (1000g)", "Nấm kim châm 500g", "Rau", 50000, 0, "https://cdn.tgdd.vn/Files/2020/12/08/1312301/11-cach-lam-nam-kim-cham-xao-thom-ngon-don-gian-tai-nha-202201071113027145.jpg", 4, 1, "24-11-2022"),
                new Raucu(5, "Ớt (1000g)", "Ớt 500g", "Gia vị", 50000, 0, "https://suckhoedoisong.qltns.mediacdn.vn/thumb_w/640/324455921873985536/2022/6/18/cach-an-che-bien-bao-quan-ot-2-1655566236587196946971.jpg", 5, 1, "24-11-2022"),
                new Raucu(6, "Cà rốt (1000g)", "Cà rốt 500g", "Củ", 50000, 0, "https://image.thanhnien.vn/w1024/Uploaded/2022/wpxlcqjwq/2022_03_03/ca-rot-2275.jpg", 6, 1, "24-11-2022"),
            };
            Shops = new ObservableCollection<Shop>()
            {
                new Shop(1, "Bách hóa xanh", "244 Huỳnh Văn Bánh, Phường 11, Phú Nhuận, Thành phố Hồ Chí Minh", "1900 1908", "bach_hoa_xanh.png", 100, 473, 1, "24-11-2022"),
            };
            ButtonCommand = new Command<object>(ExecuteButtonCommand);
            NavigateToDetailPage = new Command<Raucu>(ExecuteNavToDetailPage);
            SuggestionCollection = new ObservableCollection<Raucu>
            {
                new Raucu(1, "Nấm kim châm (500g)", "Nấm kim châm 500g", "Rau", 50000, 20, "https://cdn.tgdd.vn/Files/2020/12/08/1312301/11-cach-lam-nam-kim-cham-xao-thom-ngon-don-gian-tai-nha-202201071113027145.jpg", 1, 1, "24-11-2022"),
                new Raucu(2, "Ớt (500g)", "Ớt 500g", "Gia vị", 50000, 10, "https://suckhoedoisong.qltns.mediacdn.vn/thumb_w/640/324455921873985536/2022/6/18/cach-an-che-bien-bao-quan-ot-2-1655566236587196946971.jpg", 2, 1, "24-11-2022"),
                new Raucu(3, "Cà rốt (500g)", "Cà rốt 500g", "Củ", 50000, 20, "https://image.thanhnien.vn/w1024/Uploaded/2022/wpxlcqjwq/2022_03_03/ca-rot-2275.jpg", 3, 1, "24-11-2022"),
            };
            PerformSearch = new Command<string>(ExePerformSearch);
            NavToProfile = new Command(ExeNavToProfile);
            NavToCart = new Command(ExeNavToCart);
            ReceiptList = new ObservableCollection<Receipt_list>();
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
