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
    public class ForgetPasswordViewModel : BaseViewModel
    {
        public Command ResetPassBtn { get; }
        public INavigation Navigation { get; set; }

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

        public ForgetPasswordViewModel(INavigation navigation)
        {
            Navigation = navigation;
            ResetPassBtn = new Command(async () => await OnResetPassword());
        }

        public async Task OnResetPassword()
        {
            if (string.IsNullOrEmpty(Email))
            {
                EmailError = true;
            }
            if (!EmailError)
            {
                User user = new User()
                {
                    Email = Email,
                };
                try
                {
                    var userService = RestService.For<IUserApi>(RestClient.BaseUrl);
                    var response = await userService.ResetPassword(user);
                    if (response.IsSuccessStatusCode)
                    {
                        await Navigation.PushAsync(new RecoveryMailSentPage());
                    }
                    else
                    {
                        await MaterialDialog.Instance.AlertAsync(message: response.ReasonPhrase);
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
