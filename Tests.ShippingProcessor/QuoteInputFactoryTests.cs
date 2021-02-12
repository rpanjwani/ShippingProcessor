using ShippingProcessor;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Tests.ShippingProcessor
{
    public class QuoteInputFactoryTests
    {
        [Fact]
        public void ApiOneInputFactory_Should_Generate_Expected_PropertiesValuesFormat()
        {
            var factory = new ApiOneQuoteInputFactory();
            var result = factory.GetInputForApi("from address", "dest address", new decimal[] { 5.5m, 18.5m, 20.5m });
            var expected = "{\"ContactAddress\":\"from address\",\"WarehouseAddress\":\"dest address\",\"PackageDimensions\":[5.5,18.5,20.5]}";
            Assert.Equal(expected, result);
        }

        [Fact]
        public void ApiTwoInputFactory_Should_Generate_Expected_PropertiesValuesFormat()
        {
            var factory = new ApiTwoQuoteInputFactory();
            var result = factory.GetInputForApi("from address", "dest address", new decimal[] { 5.5m, 18.5m, 20.5m });
            var expected = "{\"Consignee\":\"from address\",\"Consignor\":\"dest address\",\"Cartons\":[5.5,18.5,20.5]}";
            Assert.Equal(expected, result);
        }

        [Fact]
        public void ApiThreeInputFactory_Should_Generate_Expected_PropertiesValuesFormat()
        {
            var factory = new ApiThreeQuoteInputFactory();
            var result = factory.GetInputForApi("from address", "dest address", new decimal[] { 5.5m, 18.5m, 20.5m });
            var expected = "<xml><Source>from address</Source><Destination>dest address</Destination><Packages><Package>5.5</Package><Package>18.5</Package><Package>20.5</Package></Packages></xml>";
            Assert.Equal(expected, result);
        }
    }
}
