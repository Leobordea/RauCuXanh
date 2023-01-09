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

        public List<string> Genderlist { get; set; }
        public Command LoadUserDetail { get; set; }
        public Command UpdateCommand { get; set; }

        private User _user;
        public User User
        {
            get { return _user; }
            set { SetProperty(ref _user, value); }
        }

        private int _selectedgender = 0;
        public int SelectedGender 
        { 
            get { return _selectedgender; } 
            set { 
                SetProperty(ref _selectedgender, value);
                if (value == 0)
                {
                    User.Gender = "male";
                }
                else User.Gender = "female";
            } 
        }

        public PersonalInformationViewModel()
        {
            Genderlist = new List<string> { "male", "female" };
            LoadUserDetail = new Command(async () => await LoadUserInfo());
            Title = "Thông tin cá nhân";
            UpdateCommand = new Command(ExeUpdateCommand);
        }

        public async Task LoadUserInfo()
        {
            IsBusy = true;
            try
            {
                var apiClient = RestService.For<IUserApi>(RestClient.BaseUrl);
                var user = await apiClient.GetUserById(userid);
                User = user;
                SelectedGender = 0;
                if (user.Gender == "female")
                {
                    SelectedGender = 1;
                }
            }
            catch (Exception ex)
            {
                await MaterialDialog.Instance.AlertAsync(message: ex.Message);
            }
            finally { IsBusy = false; }
        }

        public async void ExeUpdateCommand()
        {
            try
            {
                var userService = RestService.For<IUserApi>(RestClient.BaseUrl);
                var response = await userService.UpdateUser(userid, new User()
                {
                    Profile_pic = User.Profile_pic,
                    Username = User.Username,
                    Email = User.Email,
                    Phone_no = User.Phone_no,
                    Gender = User.Gender,
                    Birthday = DateTime.Parse(User.Birthday).ToString("MM-dd-yyyy")
                });

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    await App.Current.MainPage.DisplayAlert("Thành công", "Cập nhật thành công", "OK");
                    IsBusy = true;
                }
            }
            catch (Exception ex)
            {
                await MaterialDialog.Instance.AlertAsync(message: ex.Message);
            }
            
        }

        internal void OnAppearing()
        {
            IsBusy = true;
        }
    }
}
