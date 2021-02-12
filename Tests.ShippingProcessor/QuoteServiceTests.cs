using Moq;
using ShippingProcessor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Tests.ShippingProcessor
{
    public class QuoteServiceTests
    {
       
        [Fact]
        public async Task GetBestQuote_Should_GetQuote_GivenApis()
        {
            var quoteApiFactory = new Mock<IQuoteApiFactory>();
            var apiMocks = new List<Mock<IQuoteApiHandler>>();
            for(var i=0; i<10; i++)
            {
                var apiMock = new Mock<IQuoteApiHandler>();
                apiMock.Setup(t => t.GetQuote(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<decimal[]>())).ReturnsAsync(i);
                apiMock.Setup(t => t.Id).Returns(i.ToString());
                apiMocks.Add(apiMock);
            }
            quoteApiFactory.Setup(f => f.RetrieveQuoteApis()).Returns(apiMocks.Select(m => m.Object).ToList());

            var quoteProcessor = new QuoteService(quoteApiFactory.Object);

            var bestQuote = await quoteProcessor.GetBestQuote("test", "test", new decimal[] { 1, 2, 3 });

            quoteApiFactory.Verify(f => f.RetrieveQuoteApis(), Times.Once);

            Assert.NotNull(bestQuote);
            Assert.Equal("0", bestQuote.ShipperId);
            Assert.Equal(0, bestQuote.Amount);
        }


        [Fact]
        public async Task GetBestQuote_Should_ReturnNull_GivenNoApi()
        {
            var quoteApiFactory = new Mock<IQuoteApiFactory>();
            var apiMocks = new List<Mock<IQuoteApiHandler>>();

            quoteApiFactory.Setup(f => f.RetrieveQuoteApis()).Returns(apiMocks.Select(m => m.Object).ToList());

            var quoteProcessor = new QuoteService(quoteApiFactory.Object);

            var bestQuote = await quoteProcessor.GetBestQuote("test", "test", new decimal[] { 1, 2, 3 });

            quoteApiFactory.Verify(f => f.RetrieveQuoteApis(), Times.Once);

            Assert.Null(bestQuote);
        }
    }
}
