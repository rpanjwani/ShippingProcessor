using System.Threading.Tasks;

namespace ShippingProcessor
{
    /// <summary>
    /// interface for a handler which interacts directly with an api service in order to request a quote.
    /// </summary>
    public interface IQuoteApiHandler
    {
        string Id { get; }
        Task<decimal?> GetQuote(string sourceAddress, string destinationAddress, decimal[] cartonDimensions);
    }

    /// <summary>
    /// handler which interacts directly with an api service in order to request a quote.
    /// </summary>
    public class QuoteApiHandler : IQuoteApiHandler
    {
        private IQuoteInputFactory _quoteInputFactory;
        private IQuoteOutputTranslator _quoteOutputTranslator;
        private IFakeApiService _fakeApiService;
        private string _id;
        private string _url;
        private string _credentials;

        public QuoteApiHandler(IQuoteInputFactory quoteInputFactory, IQuoteOutputTranslator quoteOutputTranslator,
            IFakeApiService fakeApiService, string id, string url, string credentials)
        {
            _quoteInputFactory = quoteInputFactory;
            _quoteOutputTranslator = quoteOutputTranslator;
            _fakeApiService = fakeApiService;
            _url = url;
            _credentials = credentials;
            _id = id;
        }

        public string Id { get; }

        public async Task<decimal?> GetQuote(string sourceAddress, string destinationAddress, decimal[] cartonDimensions)
        {
            var input = _quoteInputFactory.GetInputForApi(sourceAddress, destinationAddress, cartonDimensions);

            //replace this with call to actual api
            var result = await _fakeApiService.GetQuote("");

            var output = _quoteOutputTranslator.ExtractQuoteAmount(result);
            return output;
        }
    }
}