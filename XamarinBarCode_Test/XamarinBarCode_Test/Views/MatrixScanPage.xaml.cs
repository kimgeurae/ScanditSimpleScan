using System;
using XamarinBarCode_Test.ViewModels;
using Scandit.DataCapture.Barcode.Tracking.Data.Unified;
using Scandit.DataCapture.Core.Common.Geometry.Unified;
using Xamarin.Forms;

namespace XamarinBarCode_Test.Views
{
    public partial class MatrixScanPage : ContentPage
    {
        private readonly float barcodeToScreenTresholdRatio = 0.1f;
        private readonly MatrixScanPageViewModel viewModel;
        private bool freezeEnabled = true;

        public MatrixScanPage()
        {
            this.InitializeComponent();

            this.viewModel = this.BindingContext as MatrixScanPageViewModel;
            this.viewModel.ShouldHideOverlay = (TrackedBarcode trackedBarcode) =>
            {
                double GetWidth(Quadrilateral barcodeLocation)
                {
                    return Math.Max(barcodeLocation.BottomRight.X - barcodeLocation.BottomLeft.X, barcodeLocation.TopRight.X - barcodeLocation.TopLeft.X);
                }

                // If the barcode is wider than the desired percent of the data capture view's width,
                // show it to the user.
                var width = GetWidth(this.DataCaptureView.MapFrameQuadrilateralToView(trackedBarcode.Location));
                return (width / this.DataCaptureView.Width) <= this.barcodeToScreenTresholdRatio;
            };
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

        private void FreezeButtonClicked(object sender, EventArgs e)
        {
            if (sender is ImageButton button)
            {
                if (this.freezeEnabled)
                {
                    button.Source = ImageSource.FromFile("freeze_disabled.png");
                }
                else
                {
                    button.Source = ImageSource.FromFile("freeze_enabled.png");
                }

                this.freezeEnabled = !this.freezeEnabled;
            }
        }
    }
}