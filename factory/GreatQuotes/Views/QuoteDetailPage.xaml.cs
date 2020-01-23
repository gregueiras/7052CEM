using System;
using System.Collections.Generic;
using GreatQuotes.ViewModels;
using Xamarin.Forms;

namespace GreatQuotes.Views {
    public partial class QuoteDetailPage : ContentPage {
        public QuoteDetailPage() {
            InitializeComponent();
            BindingContext = App.GreatQuotesViewModel.ItemSelected;

            tapQuoteRecognizer.Tapped += Handle_Tapped;
        }

        async void Handle_Clicked(object sender, System.EventArgs e) {
            await this.Navigation.PopAsync();
        }

        void Handle_Tapped(object sender, System.EventArgs e)
        {
            App.GreatQuotesViewModel.SayQuotes(BindingContext as GreatQuoteViewModel);
        }

        async void Handle_ToolbarItem_Clicked(object sender, System.EventArgs e) {
            await this.Navigation.PushModalAsync(new NavigationPage(new EditQuotePage()));
        }
    }
}
