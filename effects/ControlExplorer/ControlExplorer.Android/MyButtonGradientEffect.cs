using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Graphics.Drawables;
using ControlExplorer.Droid;
using System.ComponentModel;

[assembly: ExportEffect(typeof(MyButtonGradientEffect), "ButtonGradientEffect")]
namespace ControlExplorer.Droid
{
    class MyButtonGradientEffect : PlatformEffect
    {
        Drawable oldDrawable;

        protected override void OnAttached()
        {
            if (!(Element is Button))
                return;

            oldDrawable = Control.Background;
            SetGradient();
        }

        protected override void OnDetached()
        {
            if (oldDrawable != null)
            {
                Control.Background = oldDrawable;
            }
        }

        void SetGradient()
        {
            Button xfButton = Element as Button;
            Color colorTop = ButtonGradientEffect.GetBackgroundColor(xfButton);
            Color colorBottom = ButtonGradientEffect.GetGradientColor(xfButton);

            var gd = Gradient.GetGradientDrawable(colorTop.ToAndroid(), colorBottom.ToAndroid());
            Control.SetBackground(gd);
        }

        protected override void OnElementPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnElementPropertyChanged(args);

            if (!(Element is Button))
                return;

            if (args.PropertyName == ButtonGradientEffect.GradientColorProperty.PropertyName ||
                args.PropertyName == ButtonGradientEffect.BackgroundColorProperty.PropertyName)
            {
                SetGradient();
            }
        }
    }
}