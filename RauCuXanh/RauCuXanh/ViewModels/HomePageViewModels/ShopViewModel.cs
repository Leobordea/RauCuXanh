using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using RauCuXanh.Models;
using Xamarin.Forms;

namespace RauCuXanh.ViewModels.HomePageViewModels
{
    public class ShopViewModel : HomeViewModel
    {
        private Shop _shop;
        public Shop Shop { get { return _shop; } set { SetProperty(ref _shop, value); } }
        public ObservableCollection<Raucu> DangBan { get; set; }
        public ObservableCollection<Raucu> KhuyenMai { get; set; }
        public Command LoadRaucuCommand { get; set; }

        public ShopViewModel() { }
        public ShopViewModel(Shop s) 
        {
            Title = s.Name;
            Shop = s;
            DangBan = new ObservableCollection<Raucu>();
            KhuyenMai = new ObservableCollection<Raucu>();
            LoadRaucuCommand = new Command(async () => await ExeLoadRaucuCommand());
            _ = ExeLoadRaucuCommand();
        }

        async Task ExeLoadRaucuCommand()
        {
            IsBusy = true;
            await Task.Delay(2000);
            DangBan.Clear();
            KhuyenMai.Clear();
            foreach (Raucu r in Products)
            {
                if (r.Shop_id == Shop.Id)
                {
                    DangBan.Add(r);
                    if (r.Discount > 0)
                    {
                        KhuyenMai.Add(r);
                    }
                }
            }
            IsBusy = false;
        }
    }
}
