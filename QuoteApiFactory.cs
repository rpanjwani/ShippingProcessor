using System.Collections.Generic;

namespace ShippingProcessor
{
    /// <summary>
    /// interface for a quote api handler factory which constructs all the available api handlers
    /// </summary>
    public interface IQuoteApiFactory
    {
        IEnumerable<IQuoteApiHandler> RetrieveQuoteApis();
    }

    /// <summary>
    /// quote api handler factory which constructs all the available api handlers
    /// </summary>
    public class QuoteApiFactory : IQuoteApiFactory
    {
        public IEnumerable<IQuoteApiHandler> RetrieveQuoteApis()
        {
            var fakeService = new FakeApiService();
            return new List<IQuoteApiHandler>
            {
                new QuoteApiHandler(new ApiOneQuoteInputFactory(), new ApiOneQuoteOutputTranslator(), fakeService, "1", "url1", "credentials1"),
                new QuoteApiHandler(new ApiTwoQuoteInputFactory(), new ApiTwoQuoteOutputTranslator(), fakeService, "2", "url2", "credentials2"),
                new QuoteApiHandler(new ApiThreeQuoteInputFactory(), new ApiThreeQuoteOutputTranslator(), fakeService, "3", "url3", "credentials3"),
            };
        }
    }
}