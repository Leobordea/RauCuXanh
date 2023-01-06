using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RauCuXanh.Models;
using RauCuXanh.Services;
using Xamarin.Forms;
using Newtonsoft.Json;
using RauCuXanh.Views.HomePageViews;
using Refit;

namespace RauCuXanh.ViewModels.HomePageViewModels
{
    public class OrderViewModel : CartViewModel
    {
        public List<string> PaymentMethods { get; set; }
        public List<string> ShippingMethods { get; set; }
        public Command OrderCommand { get; set; }
        public OrderViewModel()
        {
            Title = "Đặt hàng";
            PaymentMethods = new List<string>() { "Thanh toán khi nhận hàng" };
            ShippingMethods = new List<string>() { "Nhanh", "Tiết kiệm" };
            OrderCommand = new Command(async () => await ExeOrderCommand());
            Task.Run(ExeLoadCart).Wait();
        }

        async Task ExeOrderCommand()
        {
            //var receiptService = new ReceiptService();
            //if (Error1 || Error2 || Error3 || Error4)
            //{
            //    await App.Current.MainPage.DisplayAlert("Lỗi", "Vui lòng điền đầy đủ thông tin!", "OK");
            //    return;
            //}
            //var response = await receiptService.createReceipt(new Receipt()
            //{
            //    Timestampt = DateTime.Now.ToString("yyyy-MM-dd"),
            //    User_id = "1",
            //    Shipping_addr = $"{Province}, {District}, {Block}, {Road}",
            //    Shipping_cost = ShippingCost,
            //    Total_price = TotalCost,
            //    Order_status = "chuathanhtoan",
            //}, CartProducts);
            //if (response.StatusCode == System.Net.HttpStatusCode.Created)
            //{
            //    var cartService = RestService.For<ICartApi>(RestClient.BaseUrl);
            //    foreach (CartItem cart in CartProducts)
            //    {
            //        await cartService.DeleteCart(cart.Cart);
            //    }
            //    await App.Current.MainPage.Navigation.PushAsync(new OrderSuccessPage());
            //}
        }

        private string province;
        public string Province
        {
            get { return province; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    Error1 = true;
                }
                else Error1 = false;
                SetProperty(ref province, value);
            }
        }

        private string district;
        public string District
        {
            get { return district; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    Error2 = true;
                }
                else Error2 = false;
                SetProperty(ref district, value);
            }
        }

        private string block;
        public string Block
        {
            get { return block; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    Error3 = true;
                }
                else Error3 = false;
                SetProperty(ref block, value);
            }
        }

        private string road;
        public string Road
        {
            get { return road; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    Error4 = true;
                }
                else Error4 = false;
                SetProperty(ref road, value);
            }
        }

        private int selectedPaymentMethod = 0;
        public int SelectedPaymentMethod
        {
            get { return selectedPaymentMethod; }
            set { SetProperty(ref selectedPaymentMethod, value); }
        }

        private int selectedShippingMethod = 0;
        public int SelectedShippingMethod
        {
            get { return selectedShippingMethod; }
            set
            {
                if (value == 0)
                {
                    ShippingCost = 32000;
                }
                else
                {
                    ShippingCost = 16000;
                }
                SetProperty(ref selectedShippingMethod, value);
            }
        }

        private float shippingCost = 32000;
        public float ShippingCost
        {
            get { return shippingCost; }
            set
            {
                SetProperty(ref shippingCost, value);
                FinalPrice = TotalCost + ShippingCost;
            }
        }

        private float finalPrice;
        public float FinalPrice
        {
            get { return finalPrice; }
            set { SetProperty(ref finalPrice, value); }
        }

        private bool error1 = false;
        public bool Error1 { get => error1; set { SetProperty(ref error1, value); } }

        private bool error2 = false;
        public bool Error2 { get => error2; set { SetProperty(ref error2, value); } }

        private bool error3 = false;
        public bool Error3 { get => error3; set { SetProperty(ref error3, value); } }

        private bool error4 = false;
        public bool Error4 { get => error4; set { SetProperty(ref error4, value); } }
    }
}
