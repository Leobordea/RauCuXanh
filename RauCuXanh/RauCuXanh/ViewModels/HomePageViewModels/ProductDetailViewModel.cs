using System;
using System.Collections.Generic;
using System.Text;
using RauCuXanh.Models;
using Xamarin.Forms;

namespace RauCuXanh.ViewModels.HomePageViewModels
{
    public class ProductDetailViewModel : BaseViewModel
    {
        private Product _product;
        public Product Product
        {
            get { return _product; }
            set { SetProperty(ref _product, value); }
        }

        private int _quantity = 1;
        public int Quantity
        {
            get { return _quantity; }
            set {
                if (value >= 1)
                    SetProperty(ref _quantity, value);
            }
        }

        public Command IncreaseQuantity { get; set; }
        public Command DecreaseQuantity { get; set; }

        public ProductDetailViewModel() { }
        public ProductDetailViewModel(Product p)
        {
            Title = "Chi tiết sản phẩm";
            Product = p;
            IncreaseQuantity = new Command(ExeIncreaseQuantity);
            DecreaseQuantity = new Command(ExeDecreaseQuantity);

        }

        public void ExeIncreaseQuantity()
        {
            Quantity++;
        }

        public void ExeDecreaseQuantity()
        {
            Quantity--;
        }
    }
}
