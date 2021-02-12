using Moq;
using ShippingProcessor;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Tests.ShippingProcessor
{
    public class QuoteApiHandlerTests
    {
        [Fact]
        public async Task ApiOneHandler_Should_ExtractExpectedValues()
        {
            var apiMock = new Mock<IFakeApiService>();
            apiMock.Setup(m => m.GetQuote(It.IsAny<string>())).ReturnsAsync("{Total:55.75}");

            var apiHandler = new QuoteApiHandler(new ApiOneQuoteInputFactory(), new ApiOneQuoteOutputTranslator(),
                apiMock.Object, "1", "url1", "credentials1");

            var result = await apiHandler.GetQuote("from", "to", new decimal[] { 1, 2, 3 });

            Assert.Equal(55.75m, result.Value);
        }

        [Fact]
        public async Task ApiTwoHandler_Should_ExtractExpectedValues()
        {
            var apiMock = new Mock<IFakeApiService>();
            apiMock.Setup(m => m.GetQuote(It.IsAny<string>())).ReturnsAsync("{Amount:55.75}");

            var apiHandler = new QuoteApiHandler(new ApiTwoQuoteInputFactory(), new ApiTwoQuoteOutputTranslator(),
                apiMock.Object, "2", "url2", "credentials2");

            var result = await apiHandler.GetQuote("from", "to", new decimal[] { 1, 2, 3 });

            Assert.Equal(55.75m, result.Value);
        }

        [Fact]
        public async Task ApiThreeHandler_Should_ExtractExpectedValues()
        {
            var apiMock = new Mock<IFakeApiService>();
            apiMock.Setup(m => m.GetQuote(It.IsAny<string>())).ReturnsAsync("<xml><Quote>55.75</Quote></xml>");

            var apiHandler = new QuoteApiHandler(new ApiThreeQuoteInputFactory(), new ApiThreeQuoteOutputTranslator(),
                apiMock.Object, "3", "url3", "credentials3");

            var result = await apiHandler.GetQuote("from", "to", new decimal[] { 1, 2, 3 });

            Assert.Equal(55.75m, result.Value);
        }
    }
}
