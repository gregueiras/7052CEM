using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ControlExplorer
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        Effect fontEffect;
        Effect buttonGradient;

        int count;
        public MainPage()
        {
            InitializeComponent();

            fontEffect = new LabelFontEffect();

            labelWelcome.Effects.Add(fontEffect);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            buttonGradient = new ButtonGradientEffect();
            buttonClick.Effects.Add(buttonGradient);
        }

        private void OnButtonClicked(object sender, System.EventArgs e)
        {
            buttonClick.Text = string.Format("Click Count = {0}", ++count);
        }

        private void OnSwitchToggled(object sender, ToggledEventArgs e)
        {
            labelWelcome.Effects.Remove(fontEffect);
            buttonClick.Effects.Remove(buttonGradient);

            if (switchEffects.IsToggled)
            {
                labelWelcome.Effects.Add(fontEffect);
                buttonClick.Effects.Add(buttonGradient);
            }
        }

        private void OnSliderColorValueChanged(object sender, ValueChangedEventArgs e)
        {
            var v = e.NewValue / 255.0;
            var newColor = Color.FromRgb(v, v, v);
            ButtonGradientEffect.SetGradientColor(buttonClick, newColor);
        }
    }
}