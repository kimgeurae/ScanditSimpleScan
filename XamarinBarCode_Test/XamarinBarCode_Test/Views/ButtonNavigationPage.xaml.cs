using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinBarCode_Test.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ButtonNavigationPage : ContentPage
    {
        public ButtonNavigationPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MatrixScanPage());
        }
    }
}