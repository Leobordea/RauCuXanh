using RauCuXanh.Models;
using RauCuXanh.Services;
using System;
using System.Collections.Generic;
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
        private List<OrderDetailViewModel> _modelData;
        public List<OrderDetailViewModel> ModelData
        {
            get { return _modelData; }
            set { SetProperty(ref _modelData, value); }
        }
        public Command LoadReceiptDetail { get; set; }

        public OrderDetailViewModel() { }
        public OrderDetailViewModel(Receipt r)
        {
            Title = "Chi tiết hóa đơn";
            Receipt = r;
            ModelData = new List<OrderDetailViewModel>() { };
            LoadReceiptDetail = new Command(async () => await ExeLoadReceiptDetail());
        }

        async Task ExeLoadReceiptDetail()
        {
            IsBusy= true;
            try
            {
                ModelData.Clear();
                var receiptSerview = new ReceiptService();
                var receiptDetail = await receiptSerview.getReceiptDetail(Receipt.Id);
                var raucuService = new RaucuService();
                foreach (var receipt in receiptDetail)
                {
                    ModelData.Add(new OrderDetailViewModel() { Detail = receipt, Raucu = await raucuService.getRaucuById(receipt.Raucu_id) });
                }
            } catch (Exception ex)
            {
                await MaterialDialog.Instance.AlertAsync(message: ex.Message);
            }
            finally { IsBusy= false; }
        }

        public void OnAppearing()
        {
            IsBusy= true;
        }
    }
}
