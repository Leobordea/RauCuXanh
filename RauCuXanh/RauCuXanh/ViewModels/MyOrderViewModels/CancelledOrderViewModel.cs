using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public class CancelledOrderViewModel : BaseViewModel
    {
        public ObservableCollection<CancelledOrderViewModel> CancelledOrders { get; set; }
        public Command LoadReceipt { get; set; }
        public Command NavToDetail { get; set; }

        public Receipt Receipt { get; set; }
        public int Quantity { get; set; }

        public CancelledOrderViewModel()
        {
            Title = "Đã hủy";
            CancelledOrders = new ObservableCollection<CancelledOrderViewModel>();
            LoadReceipt = new Command(async () => await ExeLoadReceiptCommand());
            NavToDetail = new Command<Receipt>(ExeNavToDetail);
        }

        async Task ExeLoadReceiptCommand()
        {
            IsBusy = true;
            try
            {
                CancelledOrders.Clear();
                Quantity = 0;
                var receiptService = RestService.For<IReceiptApi>(RestClient.BaseUrl);
                var receipts = await receiptService.GetReceiptsByUser(new Dictionary<string, object>() { { "user_id", 1 } });
                foreach (Receipt receipt in receipts)
                {
                    if (receipt.Order_status == "dahuy")
                    {
                        var receiptlist = await receiptService.GetReceiptList();
                        CancelledOrders.Add(new CancelledOrderViewModel() { Receipt = receipt, Quantity = receiptlist.Where(r => r.Receipt_id == receipt.Id).Count()});
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
            IsBusy= true;
        }
    }
}
