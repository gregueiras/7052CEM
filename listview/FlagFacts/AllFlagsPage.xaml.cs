using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FlagData;

namespace FlagFacts
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AllFlagsPage : ContentPage
    {
        public AllFlagsPage()
        {
            InitializeComponent();
            this.BindingContext = DependencyService.Get<FlagDetailsViewModel>();
        }

        private async Task ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Flag tappedFlag = (Flag)e.Item;
            await this.Navigation.PushAsync(new FlagDetailsPage());
        }
    }
}