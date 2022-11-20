using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using RauCuXanh.Models;

namespace RauCuXanh.ViewModels.MyOrderViewModels
{
    public class DeliveredOrderViewModel : OrderViewModel
    {
        public ObservableCollection<Order> DeliveredOrders { get; set; }
        public DeliveredOrderViewModel()
        {
            Title = "Đã giao";
            DeliveredOrders = new ObservableCollection<Order>();
            foreach (var o in Orders)
            {
                if (o.OrderStatus == "Da giao")
                {
                    DeliveredOrders.Add(o);
                }
            }
        }
    }
}
