using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RauCuXanh.ViewModels.HomePageViewModels
{
    public class OrderViewModel : CartViewModel
    {
        public List<string> PaymentMethods { get; set; }
        public List<string> ShippingMethods { get; set; }
        public OrderViewModel()
        {
            Title = "Đặt hàng";
            PaymentMethods = new List<string>() { "COD" };
            ShippingMethods = new List<string>() { "Nhanh", "Tiết kiệm" };
            Task.Run(ExeLoadCart).Wait();
        }

        private string province;
        public string Province
        {
            get { return province; }
            set { SetProperty(ref province, value); }
        }

        private string district;
        public string District
        {
            get { return district; }
            set { SetProperty(ref district, value); }
        }

        private string block;
        public string Block
        {
            get { return block; }
            set { SetProperty(ref block, value); }
        }

        private string road;
        public string Road
        {
            get { return road; }
            set { SetProperty(ref road, value); }
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
    }
}
