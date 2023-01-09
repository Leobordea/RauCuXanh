using RauCuXanh.Models;
using RauCuXanh.Services;
using Refit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs;

namespace RauCuXanh.ViewModels
{
    public class PersonalInformationViewModel : BaseViewModel
    {
        private int userid = 1;

        public List<string> Sex { get; set; }
        public Command UpdateCommand { get; set; }

        private User _user;
        public User User
        {
            get { return _user; }
            set { SetProperty(ref _user, value); }
        }

        public PersonalInformationViewModel()
        {
            Task.Run(async () => await LoadUserInfo());
            Title = "Thông tin cá nhân";
            Sex = new List<string> { "Male", "Female" };
            UpdateCommand = new Command(ExeUpdateCommand);
        }

        public async Task LoadUserInfo()
        {
            try
            {
                var apiClient = RestService.For<IUserApi>(RestClient.BaseUrl);
                var user = await apiClient.GetUserById(userid);
                User = user;
            }
            catch (Exception ex)
            {
                await MaterialDialog.Instance.AlertAsync(message: ex.Message);
            }
        }

        public async void ExeUpdateCommand()
        {
            try
            {
                var userService = RestService.For<IUserApi>(RestClient.BaseUrl);
                var response = await userService.UpdateUser(userid, new User() { Birthday = User.Birthday});

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    await App.Current.MainPage.DisplayAlert("Thành công", "Cập nhật thành công", "OK");
                }
            }
            catch (Exception ex)
            {
                await MaterialDialog.Instance.AlertAsync(message: ex.Message);
            }
        }
    }
}
