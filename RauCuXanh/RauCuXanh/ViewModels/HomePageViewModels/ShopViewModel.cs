using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using RauCuXanh.Models;
using RauCuXanh.Services;
using Refit;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs;

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
        }

        async Task ExeLoadRaucuCommand()
        {
            IsBusy = true;
            try
            {
                var apiClient = RestService.For<IRaucuApi>(RestClient.BaseUrl);
                var Raucus = await apiClient.GetRaucuList();
                DangBan.Clear();
                KhuyenMai.Clear();
                foreach (Raucu r in Raucus)
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
            }
            catch (Exception ex)
            {
                await MaterialDialog.Instance.AlertAsync(message: ex.Message);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public new void OnAppearing()
        {
            IsBusy = true;
        }
    }
}
