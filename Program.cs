using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace ShippingProcessor
{
    class Program
    {
        /// <summary>
        /// Sets up dependency injection for services and runs the quote service to get the best quote
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        static async Task Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IQuoteApiFactory, QuoteApiFactory>()
                .AddSingleton<IQuoteService, QuoteService>()
                .AddSingleton<IFakeApiService, FakeApiService>()
                .BuildServiceProvider();

            var quoteService = serviceProvider.GetService<IQuoteService>();
            var quote = await quoteService.GetBestQuote("from", "to", new decimal[] { 1, 2, 3 });
        }
    }
}
