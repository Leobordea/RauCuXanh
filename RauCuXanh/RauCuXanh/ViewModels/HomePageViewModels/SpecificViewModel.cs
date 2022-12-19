using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using RauCuXanh.Models;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs;

namespace RauCuXanh.ViewModels.HomePageViewModels
{
    public class SpecificViewModel : HomeViewModel
    {
        public ObservableCollection<Raucu> SpecificProducts { get; set; }
        public Command LoadSpecificProductsCommand { get; set; }
        public SpecificViewModel() { }
        public SpecificViewModel(string str)
        {
            Title = str;
            SpecificProducts = new ObservableCollection<Raucu>();
            LoadSpecificProductsCommand = new Command(async () => await ExecuteLoadProductsCommand(str));
        }

        async Task ExecuteLoadProductsCommand(string str)
        {
            IsBusy = true;
            try
            {
                await ExecuteLoadRaucusCommand();
                SpecificProducts.Clear();
                foreach (Raucu product in Raucus)
                {
                    if (product.Raucu_type == str)
                    {
                        SpecificProducts.Add(product);
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
