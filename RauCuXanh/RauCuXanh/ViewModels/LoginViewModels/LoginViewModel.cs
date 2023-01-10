using RauCuXanh.Models;
using RauCuXanh.Services;
using RauCuXanh.Utils;
using RauCuXanh.Views;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs;

namespace RauCuXanh.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public Command LoginBtn { get; }
        public Command ForgetPasswordBtn { get; }
        public Command CreateNewAccountBtn { get; }
        public INavigation Navigation { get; set; }

        private bool _passwordError = false;
        public bool PasswordError
        {
            get { return _passwordError; }
            set { SetProperty(ref _passwordError, value); }
        }

        private bool _usernameError = false;
        public bool UsernameError
        {
            get { return _usernameError; }
            set { SetProperty(ref _usernameError, value); }
        }

        public LoginViewModel(INavigation navigation)
        {
            Navigation = navigation;
            LoginBtn = new Command(async () => await OnLoginClicked());
            ForgetPasswordBtn = new Command(async () => await OnForgetPasswordClicked());
            CreateNewAccountBtn = new Command(async () => await OnCreateNewAccountClicked());
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

        private async Task OnCreateNewAccountClicked()
        {
            await Navigation.PushAsync(new RegisterPage());
        }

        private async Task OnForgetPasswordClicked()
        {
            await Navigation.PushAsync(new ForgetPasswordPage());
        }

        private async Task OnLoginClicked()
        {
            if(string.IsNullOrEmpty(Username))
            {
                UsernameError = true;
            }
            if(string.IsNullOrEmpty(Password))
            {
                PasswordError = true;
            }
            if (!PasswordError && !UsernameError)
            {
                User user = new User()
                {
                    Username = Username,
                    Password = Password
                };
                try
                {
                    var userService = RestService.For<IUserApi>(RestClient.BaseUrl);
                    var response = await userService.Login(user);
                    if (response.IsSuccessStatusCode)
                    {
                        await Navigation.PushAsync(new CheckingPage());
                    }
                    else
                    {
                        await MaterialDialog.Instance.AlertAsync(message: "Tài khoản hoặc mật khẩu không hợp lệ.");
                    }
                }
                catch (Exception ex)
                {
                    await MaterialDialog.Instance.AlertAsync(message: ex.Message);
                }
            }
        }
    }
}
