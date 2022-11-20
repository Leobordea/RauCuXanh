using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using RauCuXanh.Models;

namespace RauCuXanh.ViewModels.MyOrderViewModels
{
    public class DeliveringOrderViewModel : OrderViewModel
    {
        public ObservableCollection<Order> DeliveringOrders { get; set; }
        public DeliveringOrderViewModel()
        {
            Title = "Đang giao";
            DeliveringOrders = new ObservableCollection<Order>();
            foreach (var o in Orders)
            {
                if (o.OrderStatus == "Dang giao")
                {
                    DeliveringOrders.Add(o);
                }
            }
        }
    }
}
