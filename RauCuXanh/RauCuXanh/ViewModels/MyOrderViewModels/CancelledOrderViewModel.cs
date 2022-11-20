using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using RauCuXanh.Models;
using RauCuXanh.Views.MyOrderViews;

namespace RauCuXanh.ViewModels.MyOrderViewModels
{
    public class CancelledOrderViewModel : OrderViewModel
    {
        public ObservableCollection<Order> CancelledOrders { get; set; }
        public CancelledOrderViewModel()
        {
            Title = "Đã hủy";
            CancelledOrders = new ObservableCollection<Order>();
            foreach (var o in Orders)
            {
                if (o.OrderStatus == "Da huy")
                {
                    CancelledOrders.Add(o);
                }
            }
        }
    }
}
