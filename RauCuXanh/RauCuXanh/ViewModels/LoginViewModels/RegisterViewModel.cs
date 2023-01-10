using RauCuXanh.Models;
using RauCuXanh.Services;
using RauCuXanh.Utils;
using RauCuXanh.Views;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs;

namespace RauCuXanh.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {
        public INavigation Navigation { get; set; }
        public Command RegisterBtn { get; }
        public Command OnClickTermOfUse { get; }

        private bool _usernameError = false;
        public bool UsernameError
        {
            get { return _usernameError; }
            set { SetProperty(ref _usernameError, value); }
        }

        private string _username;
        public string Username
        {
            get { return _username; }
            set
            {
                SetProperty(ref _username, value);
                if (!string.IsNullOrEmpty(value))
                {
                    UsernameError = false;
                } 
            }
        }

        private bool _emailError = false;
        public bool EmailError
        {
            get { return _emailError; }
            set { SetProperty(ref _emailError, value); }
        }

        private string _email;
        public string Email
        {
            get { return _email; }
            set
            {
                SetProperty(ref _email, value);
                if (!string.IsNullOrWhiteSpace(value))
                {
                    EmailError = !RegexUtil.ValidateEmailAddress().IsMatch(value);
                }
            }
        }

        private bool _passwordError = false;
        public bool PasswordError
        {
            get { return _passwordError; }
            set { SetProperty(ref _passwordError, value); }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set 
            { 
                SetProperty(ref _password, value);
                if (!string.IsNullOrWhiteSpace(value))
                {
                    PasswordError = !RegexUtil.ValidatePassword().IsMatch(value);
                }
            }
        }

        private bool _checkbox = false;
        public bool Checkbox
        {
            get { return _checkbox; }
            set { SetProperty(ref _checkbox, value);}
        }

        public RegisterViewModel(INavigation navigation)
        {
            Navigation = navigation;
            RegisterBtn = new Command(async () => await OnRegisterClicked());
            OnClickTermOfUse = new Command<string>(url => TermOfUse(url));
        }

        private async Task OnRegisterClicked()
        {
            if(string.IsNullOrEmpty(Username))
            {
                UsernameError = true;
            }
            if(string.IsNullOrEmpty(Email))
            {
                EmailError = true;
            }
            if(string.IsNullOrEmpty(Password))
            {
                PasswordError = true;
            }
            if (!PasswordError && !EmailError && Checkbox && !UsernameError)
            {
                try
                {
                    var userService = RestService.For<IUserApi>(RestClient.BaseUrl);
                    var response = await userService.CreateUser(new Dictionary<string, object> {
                    {"username", Username},
                    {"password", Password},
                    {"email", Email}
                });
                    if (response.IsSuccessStatusCode)
                    {
                        await Navigation.PushAsync(new RegisterCompletedPage());
                    } else
                    {
                        await MaterialDialog.Instance.AlertAsync(message: "Tên tài khoản hoặc email tồn tại. Hãy dùng tài khoản/email khác.");
                    }
                }
                catch (Exception ex)
                {
                    await MaterialDialog.Instance.AlertAsync(message: ex.Message);
                }
            }
        }

        private async void TermOfUse(string url)
        {
            await Browser.OpenAsync(url);
        }
    }
}
