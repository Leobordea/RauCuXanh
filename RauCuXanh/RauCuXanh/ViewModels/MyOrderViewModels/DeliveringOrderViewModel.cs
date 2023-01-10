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
    public class DeliveringOrderViewModel : BaseViewModel
    {
        public ObservableCollection<DeliveringOrderViewModel> DeliveringOrders { get; set; }
        public Command LoadReceipt { get; set; }
        public Command NavToDetail { get; set; }
        public Command CancelOrder { get; set; }

        public Receipt Receipt { get; set; }
        public int Quantity { get; set; }

        public DeliveringOrderViewModel()
        {
            Title = "Đang giao";
            DeliveringOrders = new ObservableCollection<DeliveringOrderViewModel>();
            LoadReceipt = new Command(async () => await ExeLoadReceiptCommand());
            NavToDetail = new Command<Receipt>(ExeNavToDetail);
            CancelOrder = new Command<Receipt>(ExeCancelOrder);
        }

        async Task ExeLoadReceiptCommand()
        {
            IsBusy = true;
            try
            {
                DeliveringOrders.Clear();
                var receiptService = RestService.For<IReceiptApi>(RestClient.BaseUrl);
                var receipts = await receiptService.GetReceiptsByUser(new Dictionary<string, object>() { { "user_id", userid } });
                foreach (Receipt receipt in receipts)
                {
                    if (receipt.Order_status == "chuathanhtoan")
                    {
                        var receiptlist = await receiptService.GetReceiptList();
                        DeliveringOrders.Add(new DeliveringOrderViewModel()
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

        public async void ExeCancelOrder(Receipt r)
        {
            var res = await App.Current.MainPage.DisplayAlert("Thông báo", "Bạn có muốn hủy đơn không!", "Có", "Không");
            if (res)
            {
                try
                {
                    var receiptService = RestService.For<IReceiptApi>(RestClient.BaseUrl);
                    var response = await receiptService.UpdateReceipt(new Dictionary<string, object>() { { "id", r.Id }, { "order_status", "dahuy" } });

                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        await ExeLoadReceiptCommand();
                    }
                }
                catch (Exception ex)
                {
                    await MaterialDialog.Instance.AlertAsync(message: ex.Message);
                }
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
        }
    }
}
