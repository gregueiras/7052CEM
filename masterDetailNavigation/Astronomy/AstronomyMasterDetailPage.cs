using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Astronomy.Pages;

namespace Astronomy
{
    class AstronomyMasterDetailPage : MasterDetailPage
    {
        AstronomyMasterPage master;
        Page page;

        public AstronomyMasterDetailPage()
        {
            master = new AstronomyMasterPage();

            if (Device.RuntimePlatform.Equals(Device.iOS))
            {
                master.IconImageSource = ImageSource.FromFile("nav-menu-icon.png");
            }

            master.PageSelected += MasterPageSelected;

            this.Master = master;
            this.Detail = new NavigationPage(new SunrisePage());
            this.MasterBehavior = MasterBehavior.Popover;
        }

        void MasterPageSelected(object sender, PageType pageType)
        {
            PresentDetailPage(pageType);
        }

        void PresentDetailPage(PageType pageType)
        {
            switch(pageType)
            {
                case PageType.Sunrise:
                    page = new SunrisePage();
                    break;

                case PageType.MoonPhase:
                    page = new MoonPhasePage();
                    break;

                case PageType.Sun:
                    page = new AstronomicalBodyPage(SolarSystemData.Sun);
                    break;

                case PageType.Moon:
                    page = new AstronomicalBodyPage(SolarSystemData.Moon);
                    break;

                case PageType.Earth:
                    page = new AstronomicalBodyPage(SolarSystemData.Earth);
                    break;

                case PageType.About:
                    page = new AboutPage();
                    break;

                default:
                    throw new ArgumentException("Invalid Page Type");
            }

            page = new NavigationPage(page);
            this.Detail = page;
            this.IsPresented = false;
        }
    }   
}
