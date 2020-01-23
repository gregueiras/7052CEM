using System;
using System.Collections.ObjectModel;

namespace GreatQuotes.ViewModels {
    public class MainViewModel : BaseViewModel {

        private readonly QuoteManager quoteManager;
        public ObservableCollection<GreatQuoteViewModel> Quotes { get; set; }
        public GreatQuoteViewModel ItemSelected { get; set; }

        public MainViewModel() {
            quoteManager = QuoteManager.Instance;
            Quotes = new ObservableCollection<GreatQuoteViewModel>(quoteManager.Quotes);
        }

        public void SaveQuotes() {
            quoteManager.Save();
        }

        public void SayQuotes(GreatQuoteViewModel model)
        {
            quoteManager.SayQuote(model);
        }
    }
}
