using Xamarin.Forms;
using XamarinBarCode_Test.ViewModels;

namespace XamarinBarCode_Test.Views
{
    public partial class MainPage : ContentPage
    {
        private readonly MainPageViewModel viewModel;

        public MainPage()
        {
            this.InitializeComponent();
            this.viewModel = this.BindingContext as MainPageViewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _ = this.viewModel.OnResumeAsync();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            this.viewModel.OnSleep();
        }
    }
}
