using System;
using Xamarin.Forms;

namespace Planets
{
    class PlanetsMasterDetail : MasterDetailPage
    {
        public PlanetsMasterDetail()
        {
            PlanetsMasterPage master = new PlanetsMasterPage();

            this.Master = master;
            this.MasterBehavior = MasterBehavior.Split;
            master.MasterItemSelected += MasterItemSelected;

            MasterItemSelected(this, 0);
        }

        private void MasterItemSelected(object sender, int id)
        {
            this.Detail = new NavigationPage(new PlanetsDetailPage(id));
            if (Device.Idiom == TargetIdiom.Phone)
            {
                IsPresented = false;
            }
        }
    }
}
