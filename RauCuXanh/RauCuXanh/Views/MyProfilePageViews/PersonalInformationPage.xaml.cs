using RauCuXanh.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RauCuXanh.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PersonalInformationPage : ContentPage
    {
        PersonalInformationViewModel _viewModel;

        public PersonalInformationPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new PersonalInformationViewModel();
        }

        protected override void OnAppearing()
        {
            Shell.SetNavBarIsVisible(this, false);
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}