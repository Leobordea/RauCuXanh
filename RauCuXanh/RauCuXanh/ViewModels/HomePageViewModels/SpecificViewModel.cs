using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using RauCuXanh.Models;
using Xamarin.Forms;

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
            _ = ExecuteLoadProductsCommand(str);

        }

        async Task ExecuteLoadProductsCommand(string str)
        {
            IsBusy = true;
            await Task.Delay(2000);
            SpecificProducts.Clear();
            foreach (Raucu product in Products)
            {
                if (product.Raucu_type == str)
                {
                    SpecificProducts.Add(product);
                }
            }
            IsBusy = false;
        }
    }
}
