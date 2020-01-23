using Android.Content;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XFDraw;
using XFDraw.Droid;

[assembly: ExportRenderer(typeof(SketchView), typeof(SketchViewRenderer))]
namespace XFDraw.Droid
{
    class SketchViewRenderer : ViewRenderer<SketchView, PaintView>
    {
        public SketchViewRenderer(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<SketchView> e)
        {
            base.OnElementChanged(e);

            if (Control == null)
            {
                PaintView paintView = new PaintView(Context);
                paintView.SetInkColor(this.Element.InkColor.ToAndroid());

                SetNativeControl(paintView);
                MessagingCenter.Subscribe<SketchView>(this, "Clear", OnMessageClear);
                paintView.LineDrawn += PaintViewLineDrawn;
            }

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                MessagingCenter.Unsubscribe<SketchView>(this, "Clear");
            }

            base.Dispose(disposing);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == SketchView.InkColorProperty.PropertyName)
            {
                Control.SetInkColor(this.Element.InkColor.ToAndroid());
            }
        }

        private void OnMessageClear(SketchView sender)
        {
            if (this.Element == sender)
            {
                Control.Clear();
            }
        }

        private void PaintViewLineDrawn(object sender, System.EventArgs e)
        {
            (Element as ISketchController)?.SendSketchUpdate();
        }
    }
}