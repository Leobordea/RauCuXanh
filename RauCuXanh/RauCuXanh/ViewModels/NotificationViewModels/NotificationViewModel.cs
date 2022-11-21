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

namespace RauCuXanh.ViewModels.NotificationViewModels
{
    public class NotificationViewModel : BaseViewModel
    {
        public Command LoadGamesCommand { get; }
        public ObservableCollection<Game> Games { get; }

        public NotificationViewModel()
        {
            Title = "Browse";
            Games = new ObservableCollection<Game>();
            LoadGamesCommand = new Command(async () => await ExecuteLoadGamesCommand());
        }

        async Task ExecuteLoadGamesCommand()
        {
            IsBusy = true;
            try
            {
                Games.Clear();
                var apiClient = RestService.For<IFreeToPlayApi>(BaseFreeToPlayApi.BaseUrl);
                var games = await apiClient.GetF2PAsync();
                foreach (var game in games)
                {
                    Games.Add(game);
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