using System;
using System.Collections.Generic;
using System.Text;

namespace RauCuXanh.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductType { get; set; }
        public int ProductPrice { get; set; }
        public string ProductImage { get; set; }
        public int StoreId { get; set; }

        public Product() { }
        public Product(int productId, string productName, string productType, int productPrice, string productImage, int storeId)
        {
            ProductId = productId;
            ProductName = productName;
            ProductType = productType;
            ProductPrice = productPrice;
            ProductImage = productImage;
            StoreId = storeId;
        }
    }
}
