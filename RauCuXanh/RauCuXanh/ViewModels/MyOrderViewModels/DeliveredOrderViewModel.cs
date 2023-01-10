using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RauCuXanh.Models;
using RauCuXanh.Services;
using RauCuXanh.Views.MyOrderViews;
using Refit;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs;

namespace RauCuXanh.ViewModels.MyOrderViewModels
{
    public class DeliveredOrderViewModel : BaseViewModel
    {
        public ObservableCollection<DeliveredOrderViewModel> DeliveredOrders { get; set; }
        public Command LoadReceipt { get; set; }
        public Command NavToDetail { get; set; }

        public Receipt Receipt { get; set; }
        public int Quantity { get; set; }

        public DeliveredOrderViewModel()
        {
            Title = "Đã giao";
            DeliveredOrders = new ObservableCollection<DeliveredOrderViewModel>();
            LoadReceipt = new Command(async () => await ExeLoadReceiptCommand());
            NavToDetail = new Command<Receipt>(ExeNavToDetail);
        }

        async Task ExeLoadReceiptCommand()
        {
            IsBusy = true;
            try
            {
                DeliveredOrders.Clear();
                var receiptService = RestService.For<IReceiptApi>(RestClient.BaseUrl);
                var receipts = await receiptService.GetReceiptsByUser(new Dictionary<string, object>() { { "user_id", userid } });
                foreach (Receipt receipt in receipts)
                {
                    if (receipt.Order_status == "dathanhtoan")
                    {
                        var receiptlist = await receiptService.GetReceiptList();
                        DeliveredOrders.Add(new DeliveredOrderViewModel()
                        {
                            Receipt = new Receipt()
                            {
                                Id = receipt.Id,
                                Timestamp = receipt.Timestamp,
                                Total_price = receipt.Total_price,
                                User_id = receipt.User_id
                            },
                            Quantity = receiptlist.Where(r => r.Receipt_id == receipt.Id).Count()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                await MaterialDialog.Instance.AlertAsync(message: ex.Message);
            }
            finally { IsBusy = false; }
        }

        public async void ExeNavToDetail(Receipt r)
        {
            await App.Current.MainPage.Navigation.PushAsync(new Views.MyOrderViews.OrderDetailPage(r));
        }

        public void OnAppearing()
        {
            IsBusy = true;
        }
    }
}
