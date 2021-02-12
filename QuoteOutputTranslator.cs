using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ShippingProcessor
{
    /// <summary>
    /// interface for quote translator which constructs output values returned by their corresponding api in 
    /// their appropriate format and structure as specified.
    /// </summary>
    public interface IQuoteOutputTranslator
    {
        decimal? ExtractQuoteAmount(string result);
    }

    /// <summary>
    /// quote translator which constructs output values returned by its corresponding api in 
    /// their appropriate format and structure as specified.
    /// </summary>
    public class ApiOneQuoteOutputTranslator : IQuoteOutputTranslator
    {
        public decimal? ExtractQuoteAmount(string result)
        {
            try
            {
                dynamic json = JValue.Parse(result);
                string quoteString = json.Total;
                if (quoteString != null)
                {
                    if (decimal.TryParse(quoteString, out decimal quote))
                    {
                        return quote;
                    }
                }
                return null;
            }
            catch(Exception)
            {
                //TODO: logging
                return null;
            }
        }
    }

    /// <summary>
    /// quote translator which constructs output values returned by its corresponding api in 
    /// their appropriate format and structure as specified.
    /// </summary>
    public class ApiTwoQuoteOutputTranslator : IQuoteOutputTranslator
    {
        public decimal? ExtractQuoteAmount(string result)
        {
            try
            {
                dynamic json = JValue.Parse(result);
                string quoteString = json.Amount;
                if (quoteString != null)
                {
                    if (decimal.TryParse(quoteString, out decimal quote))
                    {
                        return quote;
                    }
                }
                return null;
            }
            catch (Exception)
            {
                //TODO: logging
                return null;
            }
        }
    }

    /// <summary>
    /// quote translator which constructs output values returned by its corresponding api in 
    /// their appropriate format and structure as specified.
    /// </summary>
    public class ApiThreeQuoteOutputTranslator : IQuoteOutputTranslator
    {
        public decimal? ExtractQuoteAmount(string result)
        {
            try
            {
                var quoteString = ExtractQuote(new StringReader(result));
                if (quoteString != null)
                {
                    if (decimal.TryParse(quoteString, out decimal quote))
                    {
                        return quote;
                    }
                }
                return null;
            }
            catch (Exception)
            {
                //TODO: logging
                return null;
            }
        }

        private static string ExtractQuote(StringReader stringReader)
        {
            using (XmlReader reader = XmlReader.Create(stringReader))
            {
                reader.MoveToContent();
                // Parse the file and display each of the nodes.
                while (reader.Read())
                {
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element:
                            if (reader.Name == "Quote")
                            {
                                XElement el = XElement.ReadFrom(reader) as XElement;
                                if (el != null)
                                    return (string)el;
                            }
                            break;
                    }
                }
            }
            return null;
        }
    }
}