using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Planets
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlanetsDetailPage : ContentPage
    {
        public PlanetsDetailPage(int id)
        {
            InitializeComponent();

            Planet planet = PlanetDataRepository.GetPlanetFromId(id);
            BindingContext = planet;
        }
    }
}