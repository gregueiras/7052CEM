using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using ControlExplorer.iOS;
using CoreAnimation;
using System.ComponentModel;

[assembly: ExportEffect(typeof(MyButtonGradientEffect), "ButtonGradientEffect")]
namespace ControlExplorer.iOS
{
    class MyButtonGradientEffect : PlatformEffect
    {
        CAGradientLayer oldGradLayer;
        CAGradientLayer gradLayer;

        protected override void OnAttached()
        {
            if (!(Element is Button))
                return;

            SetGradient();
        }

        protected override void OnDetached()
        {
            gradLayer?.RemoveFromSuperLayer();
        }

        void SetGradient()
        {
            gradLayer?.RemoveFromSuperLayer();


            Button xfButton = Element as Button;
            Color colorTop = ButtonGradientEffect.GetBackgroundColor(xfButton);
            Color colorBottom = ButtonGradientEffect.GetGradientColor(xfButton);
            var width = (float)xfButton.Width;
            var height = (float)xfButton.Height;

            var gd = Gradient.GetGradientLayer(colorTop.ToCGColor(), colorBottom.ToCGColor(), width, height);
            Control.Layer.InsertSublayer(gd, 0);
        }

        protected override void OnElementPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnElementPropertyChanged(args);

            if (!(Element is Button))
                return;

            if (args.PropertyName == ButtonGradientEffect.GradientColorProperty.PropertyName 
             || args.PropertyName == ButtonGradientEffect.BackgroundColorProperty.PropertyName
             || args.PropertyName == VisualElement.WidthProperty.PropertyName
             || args.PropertyName == VisualElement.HeightProperty.PropertyName)
            {
                SetGradient();
            }
        }
    }
}