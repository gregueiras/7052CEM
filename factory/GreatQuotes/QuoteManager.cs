using System;
using System.Collections.Generic;
using System.Text;
using GreatQuotes.ViewModels;
using GreatQuotes.Data;
using System.Collections.ObjectModel;

namespace GreatQuotes
{
    public  class QuoteManager
    {
        public static QuoteManager Instance { get; private set; }
        readonly IQuoteLoader loader;

        ITextToSpeech tts;

        public IList<GreatQuoteViewModel> Quotes { get; private set; }

        private QuoteManager(IQuoteLoader quoteLoader, ITextToSpeech textToSpeech)
        {
            if (Instance != null)
            {
                throw new Exception("Can only create a single QuoteManager.");
            }
            Instance = this;
            this.loader = quoteLoader;
            this.tts = textToSpeech;

            Quotes = new ObservableCollection<GreatQuoteViewModel>(loader.Load());
        }

        public void Save()
        {
            loader.Save(Quotes);
        }

        public void SayQuote(GreatQuoteViewModel quote)
        {
            if (quote == null)
            {
                throw new ArgumentNullException("No Quote Set");
            }

            if (tts != null)
            {
                string text = $"{(quote.Author != null ? $"{quote.Author} said: "  : "" )}{quote.QuoteText}";
                tts.Speak(text);
            }
        }
    }
}
