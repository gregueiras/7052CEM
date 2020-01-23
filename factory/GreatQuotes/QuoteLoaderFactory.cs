using System;
using System.Collections.Generic;
using System.Text;

namespace GreatQuotes
{
    public class QuoteLoaderFactory
    {
        public static Func<IQuoteLoader> Create { get; set; }
    }
}
