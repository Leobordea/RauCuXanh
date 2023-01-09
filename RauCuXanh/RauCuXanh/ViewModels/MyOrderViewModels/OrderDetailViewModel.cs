using RauCuXanh.Models;
using RauCuXanh.Services;
using Refit;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs;

namespace RauCuXanh.ViewModels.MyOrderViewModels
{
    public class OrderDetailViewModel : BaseViewModel
    {
        private Receipt _receipt;
        public Receipt Receipt
        {
            get { return _receipt;}
            set { SetProperty(ref _receipt, value); }
        }

        public Receipt_list Detail { get; set; }
        public Raucu Raucu { get; set; }

        private ObservableCollection<OrderDetailViewModel> _modelData;
        public ObservableCollection<OrderDetailViewModel> ModelData
        {
            get { return _modelData; }
            set { SetProperty(ref _modelData, value); }
        }
        public Command LoadReceiptDetail { get; set; }
        public Command NavigateToDetailPage { get; set; }

        public OrderDetailViewModel() { }
        public OrderDetailViewModel(Receipt r)
        {
            Title = "Đơn hàng";
            Receipt = r;
            ModelData = new ObservableCollection<OrderDetailViewModel>() { };
            LoadReceiptDetail = new Command(async () => await ExeLoadReceiptDetail());
            NavigateToDetailPage = new Command<Raucu>(ExecuteNavToDetailPage);
        }

        async Task ExeLoadReceiptDetail()
        {
            IsBusy= true;
            try
            {
                ModelData.Clear();
                var receiptService = RestService.For<IReceiptApi>(RestClient.BaseUrl);
                var raucuService = RestService.For<IRaucuApi>(RestClient.BaseUrl);
                var receiptDetail = await receiptService.GetReceiptList();
                foreach (var receipt in receiptDetail)
                {
                    if (receipt.Receipt_id == Receipt.Id)
                    {
                        ModelData.Add(new OrderDetailViewModel() { Detail = receipt, Raucu = await raucuService.GetRaucuById(receipt.Raucu_id) });
                    }
                }
            } catch (Exception ex)
            {
                await MaterialDialog.Instance.AlertAsync(message: ex.Message);
            }
            finally { IsBusy= false; }
        }

        public async void ExecuteNavToDetailPage(Raucu p)
        {
            await App.Current.MainPage.Navigation.PushAsync(new Views.HomePageViews.ProductDetailPage(p));
        }

        public void OnAppearing()
        {
            IsBusy= true;
        }
    }
}
