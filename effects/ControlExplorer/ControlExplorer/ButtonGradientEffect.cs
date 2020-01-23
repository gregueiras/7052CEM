using Xamarin.Forms;

namespace ControlExplorer
{
    public class ButtonGradientEffect : RoutingEffect
    {
        public static readonly BindableProperty GradientColorProperty = 
            BindableProperty.CreateAttached("GradientColor", typeof(Color), typeof(ButtonGradientEffect), Color.Black);

        public static readonly BindableProperty BackgroundColorProperty =
            BindableProperty.CreateAttached("BackgroundColor", typeof(Color), typeof(ButtonGradientEffect), Color.Accent);

        public ButtonGradientEffect() : base("Xamarin.ButtonGradientEffect")
        {
           
        }

        public static void SetGradientColor(VisualElement visualElement, Color color)
        {
            visualElement.SetValue(GradientColorProperty, color);
        }

        public static Color GetGradientColor(VisualElement visualElement)
        {
            return (Color)visualElement.GetValue(GradientColorProperty);
        }

        public static void SetBackgroundColor(VisualElement visualElement, Color color)
        {
            visualElement.SetValue(BackgroundColorProperty, color);
        }

        public static Color GetBackgroundColor(VisualElement visualElement)
        {
            return (Color)visualElement.GetValue(BackgroundColorProperty);
        }
    }
}
