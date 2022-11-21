using RauCuXanh.Models;
using RauCuXanh.Services;
using Refit;
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
    public partial class TestAPI : ContentPage
    {
            public TestAPI()
            {
                InitializeComponent();
                button_getf2p.Clicked += Button_getf2p_Clicked; ;
            }

            private async void Button_getf2p_Clicked(object sender, EventArgs e)
            {
                try
                {
                    var apiClient = RestService.For<IFreeToPlayApi>(BaseFreeToPlayApi.BaseUrl);
                    var listF2P = await apiClient.GetF2PAsync();
                    StacklayoutListF2P.ItemsSource = listF2P;
                }

                catch (Exception ex)
                {
                    Console.WriteLine("Oups " + ex.Message);
                }
            }
    }
}