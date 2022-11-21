using RauCuXanh.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace RauCuXanh.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}