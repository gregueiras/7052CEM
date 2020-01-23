using System;
using System.Collections.Generic;
using System.Text;
using GreatQuotes.ViewModels;

namespace GreatQuotes
{
    public interface IQuoteLoader
    {
        IEnumerable<GreatQuoteViewModel> Load();
        void Save(IEnumerable<GreatQuoteViewModel> quotes);
    }
}
