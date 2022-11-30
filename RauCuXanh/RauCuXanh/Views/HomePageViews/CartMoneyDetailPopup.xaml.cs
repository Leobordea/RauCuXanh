using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.Xaml;
using Xamarin.CommunityToolkit.PlatformConfiguration.iOSSpecific;
using Xamarin.CommunityToolkit.PlatformConfiguration.WindowsSpecific;


namespace RauCuXanh.Views.HomePageViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CartMoneyDetailPopup
    {
        public CartMoneyDetailPopup()
        {
            InitializeComponent();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            On<iOS>().UseArrowDirection(PopoverArrowDirection.Right);
            On<Windows>().SetBorderColor(Xamarin.Forms.Color.Red);
        }
    }
}