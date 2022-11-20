using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using RauCuXanh.Models;

namespace RauCuXanh.ViewModels.HomePageViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        private string _selectedOption;
        public string SelectedOption
        {
            get { return _selectedOption; }
            set { SetProperty(ref _selectedOption, value); }
        }
        public ObservableCollection<Product> Products { get; set; }
        public List<string> PickerOptions { get; set; }
        public HomeViewModel()
        {
            Title = "Trang chủ";
            Products = new ObservableCollection<Product>()
            {
                new Product(1, "Nấm kim châm (500g)", "Rau", 50000, "https://cdn.tgdd.vn/Files/2020/12/08/1312301/11-cach-lam-nam-kim-cham-xao-thom-ngon-don-gian-tai-nha-202201071113027145.jpg", 1),
                new Product(2, "Ớt (500g)", "Gia vị", 50000, "https://suckhoedoisong.qltns.mediacdn.vn/thumb_w/640/324455921873985536/2022/6/18/cach-an-che-bien-bao-quan-ot-2-1655566236587196946971.jpg", 2),
                new Product(3, "Cà rốt (500g)", "Củ", 50000, "https://image.thanhnien.vn/w1024/Uploaded/2022/wpxlcqjwq/2022_03_03/ca-rot-2275.jpg", 3),
                new Product(4, "Nấm kim châm (500g)", "Rau", 50000, "https://cdn.tgdd.vn/Files/2020/12/08/1312301/11-cach-lam-nam-kim-cham-xao-thom-ngon-don-gian-tai-nha-202201071113027145.jpg", 1),
                new Product(5, "Nấm kim châm (500g)", "Rau", 50000, "https://cdn.tgdd.vn/Files/2020/12/08/1312301/11-cach-lam-nam-kim-cham-xao-thom-ngon-don-gian-tai-nha-202201071113027145.jpg", 2),
                new Product(6, "Nấm kim châm (500g)", "Rau", 50000, "https://cdn.tgdd.vn/Files/2020/12/08/1312301/11-cach-lam-nam-kim-cham-xao-thom-ngon-don-gian-tai-nha-202201071113027145.jpg", 3),
                new Product(7, "Nấm kim châm (500g)", "Rau", 50000, "https://cdn.tgdd.vn/Files/2020/12/08/1312301/11-cach-lam-nam-kim-cham-xao-thom-ngon-don-gian-tai-nha-202201071113027145.jpg", 3),
                new Product(8, "Nấm kim châm (500g)", "Rau", 50000, "https://cdn.tgdd.vn/Files/2020/12/08/1312301/11-cach-lam-nam-kim-cham-xao-thom-ngon-don-gian-tai-nha-202201071113027145.jpg", 3),
                new Product(9, "Nấm kim châm (500g)", "Rau", 50000, "https://cdn.tgdd.vn/Files/2020/12/08/1312301/11-cach-lam-nam-kim-cham-xao-thom-ngon-don-gian-tai-nha-202201071113027145.jpg", 3),
                new Product(10, "Nấm kim châm (500g)", "Rau", 50000, "https://cdn.tgdd.vn/Files/2020/12/08/1312301/11-cach-lam-nam-kim-cham-xao-thom-ngon-don-gian-tai-nha-202201071113027145.jpg", 3),
            };
            PickerOptions = new List<string>()
            {
                "Giá tăng dần", "Giá giảm dần", "Mới nhất", "Cũ nhất"
            };
        }
    }
}
