using ShippingProcessor;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Tests.ShippingProcessor
{
    public class QuoteOutputTranslatorTests
    {
        [Fact]
        public void ApiOneOutputTranslator_Should_Extract_Expected_Values()
        {
            var factory = new ApiOneQuoteOutputTranslator();
            var result = factory.ExtractQuoteAmount("{Total:35.75}");
            Assert.Equal(35.75m, result);
        }

        [Fact]
        public void ApiTwoOutputTranslator_Should_Extract_Expected_Values()
        {
            var factory = new ApiTwoQuoteOutputTranslator();
            var result = factory.ExtractQuoteAmount("{Amount:35.75}");
            Assert.Equal(35.75m, result);
        }

        [Fact]
        public void ApiThreeOutputTranslator_Should_Extract_Expected_Values()
        {
            var factory = new ApiThreeQuoteOutputTranslator();
            var result = factory.ExtractQuoteAmount("<xml><Quote>35.75</Quote></xml>");
            Assert.Equal(35.75m, result);
        }
    }
}
