using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Astronomy
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AstronomyMasterPage : ContentPage
    {
        
        public event EventHandler<PageType> PageSelected;

        public AstronomyMasterPage()
        {
            InitializeComponent();

            btnSunrise.Clicked += (s, e) => PageSelected?.Invoke(this, PageType.Sunrise);
            btnMoonPhase.Clicked += (s, e) => PageSelected?.Invoke(this, PageType.MoonPhase);
            btnSun.Clicked += (s, e) => PageSelected?.Invoke(this, PageType.Sun);

            btnMoon.Clicked += (s, e) => PageSelected?.Invoke(this, PageType.Moon);
            btnEarth.Clicked += (s, e) => PageSelected?.Invoke(this, PageType.Earth);
            btnAbout.Clicked += (s, e) => PageSelected?.Invoke(this, PageType.About);
        }


    }
}