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
        public Command LoadNotificationsCommand { get; }
        public ObservableCollection<Notification> Notifications { get; }

        public NotificationViewModel()
        {
            Title = "Notifications";
            Notifications = new ObservableCollection<Notification>();
            LoadNotificationsCommand = new Command(async () => await ExecuteLoadNotificationsCommand());
        }

        async Task ExecuteLoadNotificationsCommand()
        {
            IsBusy = true;
            try
            {
                Notifications.Clear();
                var apiClient = RestService.For<INotificationApi>(RestClient.BaseUrl);
                var notifications = await apiClient.GetNotifications();
                notifications.Reverse();
                foreach (var notification in notifications)
                {
                    if (notification.User_id == userid)
                    {
                        Notifications.Add(notification);
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