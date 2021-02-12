using System.IO;
using System.Xml.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml;

namespace ShippingProcessor
{
    /// <summary>
    /// interface for quote input factory which constructs input values in their appropriate format and structure
    /// as specified by each api.
    /// </summary>
    public interface IQuoteInputFactory
    {
        string GetInputForApi(string sourceAddress, string destinationAddress, decimal[] cartonDimensions);
    }

    /// <summary>
    /// quote input factory which constructs input values in their appropriate format and structure
    /// as specified by its corresponding api.
    /// </summary>
    public class ApiOneQuoteInputFactory : IQuoteInputFactory
    {
        public class ApiOneInputModel 
        {
            public string ContactAddress { get; set; }
            public string WarehouseAddress { get; set; }
            public decimal[] PackageDimensions { get; set; }
        }

        public string GetInputForApi(string sourceAddress, string destinationAddress, decimal[] cartonDimensions)
        {
            var inputModel = new ApiOneInputModel
            {
                ContactAddress = sourceAddress,
                WarehouseAddress = destinationAddress,
                PackageDimensions = cartonDimensions
            };

            return JsonSerializer.Serialize(inputModel);
        }
    }

    /// <summary>
    /// quote input factory which constructs input values in their appropriate format and structure
    /// as specified by its corresponding api.
    /// </summary>
    public class ApiTwoQuoteInputFactory : IQuoteInputFactory
    {
        public class ApiTwoInputModel
        {
            public string Consignee { get; set; }
            public string Consignor { get; set; }
            public decimal[] Cartons { get; set; }
        }

        public string GetInputForApi(string sourceAddress, string destinationAddress, decimal[] cartonDimensions)
        {
            var inputModel = new ApiTwoInputModel
            {
                Consignee = sourceAddress,
                Consignor = destinationAddress,
                Cartons = cartonDimensions
            };

            return JsonSerializer.Serialize(inputModel);
        }
    }

    /// <summary>
    /// quote input factory which constructs input values in their appropriate format and structure
    /// as specified by its corresponding api.
    /// </summary>
    public class ApiThreeQuoteInputFactory : IQuoteInputFactory
    {
        [XmlRoot("xml")]
        public class ApiThreeInputModel
        {
            public string Source { get; set; }
            public string Destination { get; set; }
            [XmlArrayItem("Package")]
            public decimal[] Packages { get; set; }
        }

        public string GetInputForApi(string sourceAddress, string destinationAddress, decimal[] cartonDimensions)
        {
            var ns = new XmlSerializerNamespaces();
            ns.Add("", "");            
            var serializer = new XmlSerializer(typeof(ApiThreeInputModel));
            
            var inputModel = new ApiThreeInputModel
            {
                Source = sourceAddress,
                Destination = destinationAddress,
                Packages = cartonDimensions
            };

            using (StringWriter textWriter = new StringWriter())
            {
                var xmlWriter = XmlWriter.Create(textWriter, new XmlWriterSettings() { OmitXmlDeclaration = true });
                serializer.Serialize(xmlWriter, inputModel, ns);
                return textWriter.ToString();
            }
        }
    }
}