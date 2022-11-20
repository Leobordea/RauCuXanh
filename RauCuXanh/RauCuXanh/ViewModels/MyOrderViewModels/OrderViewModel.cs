using System;
using System.Collections.Generic;
using System.Text;
using RauCuXanh.Models;
using Xamarin.CommunityToolkit.ObjectModel;

namespace RauCuXanh.ViewModels.MyOrderViewModels
{
    public class OrderViewModel : BaseViewModel
    {
        public ObservableRangeCollection<Order> Orders { get; set; }

        public OrderViewModel()
        {
            Title = "Đơn hàng của tôi";
            Orders = new ObservableRangeCollection<Order> {
                new Order {OrderID="1", OrderDate="20/03/2020", OrderQuantity=3, OrderPrice=150000, OrderStatus="Dang giao"},
                new Order {OrderID="2", OrderDate="20/03/2020", OrderQuantity=3, OrderPrice=150000, OrderStatus="Dang giao"},
                new Order {OrderID="3", OrderDate="20/03/2020", OrderQuantity=3, OrderPrice=150000, OrderStatus="Dang giao"},
                new Order {OrderID="4", OrderDate="20/03/2020", OrderQuantity=3, OrderPrice=150000, OrderStatus="Dang giao"},
                new Order {OrderID="5", OrderDate="20/03/2020", OrderQuantity=3, OrderPrice=150000, OrderStatus="Da giao"},
                new Order {OrderID="6", OrderDate="20/03/2020", OrderQuantity=3, OrderPrice=150000, OrderStatus="Da giao"},
                new Order {OrderID="7", OrderDate="20/03/2020", OrderQuantity=3, OrderPrice=150000, OrderStatus="Da giao"},
                new Order {OrderID="8", OrderDate="20/03/2020", OrderQuantity=3, OrderPrice=150000, OrderStatus="Da giao"},
                new Order {OrderID="9", OrderDate="20/03/2020", OrderQuantity=3, OrderPrice=150000, OrderStatus="Da huy"},
                new Order {OrderID="10", OrderDate="20/03/2020", OrderQuantity=3, OrderPrice=150000, OrderStatus="Da huy"},
                new Order {OrderID="11", OrderDate="20/03/2020", OrderQuantity=3, OrderPrice=150000, OrderStatus="Da huy"},
                new Order {OrderID="12", OrderDate="20/03/2020", OrderQuantity=3, OrderPrice=150000, OrderStatus="Da huy"},
            };
        }
    }
}
