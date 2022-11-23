using RauCuXanh.Models;
using RauCuXanh.Services;
using RauCuXanh.Views;
using Refit;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs;

namespace RauCuXanh.ViewModels.FavoriteViewModels
{
    public class FavoriteViewModel : BaseViewModel
    {
        public Command LoadRaucusCommand { get; }
        public ObservableCollection<Raucu> Raucus { get; }

        public FavoriteViewModel()
        {
            Title = "Favorite";
            Raucus = new ObservableCollection<Raucu>();
            LoadRaucusCommand = new Command(async () => await ExecuteLoadRaucusCommand());
        }

        async Task ExecuteLoadRaucusCommand()
        {
            IsBusy = true;
            try
            {
                Raucus.Clear();
                var apiClient = RestService.For<IRaucuApi>(RestClient.BaseUrl);
                var raucus = await apiClient.GetRaucuList();
                foreach (var raucu in raucus)
                {
                    Raucus.Add(raucu);
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