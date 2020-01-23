using Android.Content;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XFDraw;
using XFDraw.Droid;

[assembly: ExportRenderer(typeof(ShadowedLabel), typeof(ShadowedLabelRenderer))]
namespace XFDraw.Droid
{
    class ShadowedLabelRenderer : LabelRenderer
    {
        public ShadowedLabelRenderer(Context context) : base(context)
        {
            
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);
            Control.SetShadowLayer(10, 5, 5, Android.Graphics.Color.DarkGray);
        }
    }
}