using System;
using Xamarin.Forms;
namespace XFDraw
{
    public class SketchView : View, ISketchController
    {
        public static readonly BindableProperty InkColorProperty = BindableProperty.Create("InkColor", typeof(Color), typeof(SketchView), Color.Blue);
        public event EventHandler SketchUpdated;

        public Color InkColor
        {
            get { return (Color)GetValue(InkColorProperty); }
            set { SetValue(InkColorProperty, value); }
        }

        public void Clear()
        {
            MessagingCenter.Send(this, "Clear");
        }

        public void SendSketchUpdate()
        {
            SketchUpdated?.Invoke(this, EventArgs.Empty);
        }
    }
}
