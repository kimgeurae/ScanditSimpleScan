using System;
using System.ComponentModel;
using System.Diagnostics;
using Android.Graphics;
using Android.Views;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XamarinBarCode_Test.Effects;
using XamarinBarCode_Test.Droid.Effects;

[assembly: ResolutionGroupName("XamarinBarCode_Test")]
[assembly: ExportEffect(typeof(RoundCornersEffectAndroid), nameof(RoundCornersEffect))]

namespace XamarinBarCode_Test.Droid.Effects
{
    public class RoundCornersEffectAndroid : PlatformEffect
    {
        private Android.Views.View View => this.Control ?? this.Container;

        protected override void OnAttached()
        {
            try
            {
                this.PrepareContainer();
                this.SetCornerRadius();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
        }

        protected override void OnDetached()
        {
            try
            {
                this.View.OutlineProvider = ViewOutlineProvider.Background;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
        }

        protected override void OnElementPropertyChanged(PropertyChangedEventArgs args)
        {
            if (args.PropertyName == RoundCornersEffect.CornerRadiusProperty.PropertyName)
            {
                this.SetCornerRadius();
            }
        }

        private void PrepareContainer()
        {
            this.View.ClipToOutline = true;
        }

        private void SetCornerRadius()
        {
            var cornerRadius = RoundCornersEffect.GetCornerRadius(Element) * GetDensity();
            this.View.OutlineProvider = new RoundedOutlineProvider(cornerRadius);
        }

        private static double GetDensity() => DeviceDisplay.MainDisplayInfo.Density;

        private class RoundedOutlineProvider : ViewOutlineProvider
        {
            private readonly double radius;

            public RoundedOutlineProvider(double radius)
            {
                this.radius = radius;
            }

            public override void GetOutline(Android.Views.View view, Outline outline)
            {
                outline?.SetRoundRect(0, 0, view.Width, view.Height, (float)this.radius);
            }
        }
    }
}
