using System;
using System.Collections.Generic;
using System.Text;
using RauCuXanh.ViewModels;

namespace RauCuXanh.Models
{
    public class CartItem : BaseViewModel
    {
        public Raucu Raucu { get; set; }
        public Cart Cart { get; set; }
    }
}
