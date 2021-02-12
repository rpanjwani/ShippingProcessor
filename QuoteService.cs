using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShippingProcessor
{
    /// <summary>
    /// interface for the main quote service which retrieves all the available apis in order to get
    /// the best quote for provided destination and package
    /// </summary>
    public interface IQuoteService
    {
        Task<Quote> GetBestQuote(string from, string to, decimal[] dimensions);
    }

    /// <summary>
    /// the main quote service which retrieves all the available apis in order to get
    /// the best quote for provided destination and package
    /// </summary>
    public class QuoteService : IQuoteService
    {
        IQuoteApiFactory _quoteApiFactory { get; set; }
        public QuoteService(IQuoteApiFactory quoteApiFactory)
        {
            _quoteApiFactory = quoteApiFactory;
        }

        public async Task<Quote> GetBestQuote(string from, string to, decimal[] dimensions)
        {
            Quote bestQuote = new Quote
            {
                ShipperId = null,
                Amount = decimal.MaxValue
            };
            
            var quoteApis = _quoteApiFactory.RetrieveQuoteApis();
            foreach(var api in quoteApis)
            {
                var amt = await api.GetQuote(from, to, dimensions);
                if(amt != null && amt.Value < bestQuote.Amount)
                {
                    bestQuote.Amount = amt.Value;
                    bestQuote.ShipperId = api.Id;
                }                
            }

            if (bestQuote.ShipperId == null) return null;

            return bestQuote;
        }
    }
}
