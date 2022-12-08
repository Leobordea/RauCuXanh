using RauCuXanh.Models;
using RauCuXanh.Services;
using Refit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using XF.Material.Forms.UI.Dialogs;

namespace RauCuXanh.ViewModels
{
    public class PersonalInformationViewModel : INotifyPropertyChanged
    {
        private int userid = 1;
        private string username = string.Empty;
        public String Username
        {
            get => username;
            set
            {
                username = value;
                OnPropertyChanged();
            }
        }
        private string email = string.Empty;
        public String Email
        {
            get => email;
            set
            {
                email = value;
                OnPropertyChanged();
            }
        }
        private string birthday = string.Empty;
        public String Birthday
        {
            get => birthday;
            set
            {
                birthday = value;
                OnPropertyChanged();
            }
        }
        private string gender = string.Empty;
        public String Gender
        {
            get => gender;
            set
            {
                gender = value;
                OnPropertyChanged();
            }
        }
        private string phone_no = string.Empty;
        public String Phone_no
        {
            get => phone_no;
            set
            {
                phone_no = value;
                OnPropertyChanged();
            }
        }

        private string profile_pic = string.Empty;
        public String Profile_pic
        {
            get => profile_pic;
            set
            {
                profile_pic = value;
                OnPropertyChanged();
            }
        }

        void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public string Title { get; }
        public List<string> Sex { get; set; }
        public bool IsBusy { get; private set; }
        public PersonalInformationViewModel()
        {
            Task.Run(async () => await LoadUserInfo());
            Title = "Thông tin cá nhân";
            Sex = new List<string> { "Male", "Female" };
        }

        public async Task LoadUserInfo()
        {
                try
                {
                    var apiClient = RestService.For<IUserApi>(RestClient.BaseUrl);
                    var user = await apiClient.GetUserById(userid);
                    username = user.Username;
                    OnPropertyChanged(nameof(Username));
                    email = user.Email;
                    OnPropertyChanged(nameof(Email));
                    phone_no = user.Phone_no;
                    OnPropertyChanged(nameof(Phone_no));
                    birthday = user.Birthday;
                    OnPropertyChanged(nameof(Birthday));
                    gender = user.Gender;
                    OnPropertyChanged(nameof(Gender));
                    profile_pic = user.Profile_pic;
                    OnPropertyChanged(nameof(Profile_pic));

                }
                catch (Exception ex)
                {
                    await MaterialDialog.Instance.AlertAsync(message: ex.Message);
                }
        }
    }
}
