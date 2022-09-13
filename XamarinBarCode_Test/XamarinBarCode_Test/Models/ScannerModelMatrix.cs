using Scandit.DataCapture.Barcode.Data.Unified;
using Scandit.DataCapture.Barcode.Tracking.Capture.Unified;
using Scandit.DataCapture.Core.Capture.Unified;
using Scandit.DataCapture.Core.Source.Unified;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace XamarinBarCode_Test.Models
{
    public class ScannerModelMatrix
    {
        // Enter your Scandit License key here.
        public const string SCANDIT_LICENSE_KEY = DevelopmentEnv.ANDROID_API_KEY;

        private static readonly Lazy<ScannerModelMatrix> instance = new Lazy<ScannerModelMatrix>(() => new ScannerModelMatrix(), LazyThreadSafetyMode.PublicationOnly);

        public static ScannerModelMatrix Instance => instance.Value;

        private ScannerModelMatrix()
        {
            this.CameraSettings.PreferredResolution = VideoResolution.Uhd4k;
            this.CurrentCamera?.ApplySettingsAsync(this.CameraSettings);

            // Create data capture context using your license key and set the camera as the frame source.
            this.DataCaptureContext = DataCaptureContext.ForLicenseKey(SCANDIT_LICENSE_KEY);
            this.DataCaptureContext.SetFrameSourceAsync(this.CurrentCamera);

            // The barcode tracking process is configured through barcode tracking settings
            // and are then applied to the barcode tracking instance that manages barcode tracking.
            this.BarcodeTrackingSettings = BarcodeTrackingSettings.Create(BarcodeTrackingScenario.A);

            // The settings instance initially has all types of barcodes (symbologies) disabled.
            // For the purpose of this sample we enable a very generous set of symbologies.
            // In your own app ensure that you only enable the symbologies that your app requires as
            // every additional enabled symbology has an impact on processing times.
            HashSet<Symbology> symbologies = new HashSet<Symbology>
            {
                Symbology.Ean13Upca,
                Symbology.Ean8,
                Symbology.Upce,
                Symbology.Code39,
                Symbology.Code128
            };

            this.BarcodeTrackingSettings.EnableSymbologies(symbologies);
            this.BarcodeTracking = BarcodeTracking.Create(this.DataCaptureContext, this.BarcodeTrackingSettings);
        }

        #region DataCaptureContext
        public DataCaptureContext DataCaptureContext { get; private set; }
        #endregion

        #region CameraSettings
        public Camera CurrentCamera { get; private set; } = Camera.GetCamera(CameraPosition.WorldFacing);
        public CameraSettings CameraSettings { get; } = BarcodeTracking.RecommendedCameraSettings;
        #endregion

        #region BarcodeTracking
        public BarcodeTracking BarcodeTracking { get; private set; }
        public BarcodeTrackingSettings BarcodeTrackingSettings { get; private set; }
        #endregion
    }
}
