using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using RauCuXanh.Models;
using RauCuXanh.Services;
using Refit;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs;

namespace RauCuXanh.ViewModels.HomePageViewModels
{
    public class ShopViewModel : BaseViewModel
    {
        private Shop _shop;
        public Shop Shop { get { return _shop; } set { SetProperty(ref _shop, value); } }
        public ObservableCollection<Raucu> DangBan { get; set; }
        public ObservableCollection<Raucu> KhuyenMai { get; set; }
        public Command LoadShopCommand { get; set; }

        public ICommand NavigateToDetailPage => new Command<Raucu>(async(Raucu r) =>
        {
            await App.Current.MainPage.Navigation.PushAsync(new Views.HomePageViews.ProductDetailPage(r));
        });

        public ShopViewModel() { }
        public ShopViewModel(Shop s) 
        {
            Title = s.Name;
            Shop = s;
            DangBan = new ObservableCollection<Raucu>();
            KhuyenMai = new ObservableCollection<Raucu>();
            LoadShopCommand = new Command(async () => await ExeLoadShopCommand());
        }

        async Task ExeLoadShopCommand()
        {
            IsBusy = true;
            try
            {
                var apiClient = RestService.For<IRaucuApi>(RestClient.BaseUrl);
                var raucus = await apiClient.GetRaucuList();
                foreach (var raucu in raucus)
                DangBan.Clear();
                KhuyenMai.Clear();
                foreach (Raucu r in raucus)
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

        public void OnAppearing()
        {
            IsBusy = true;
        }
    }
}
