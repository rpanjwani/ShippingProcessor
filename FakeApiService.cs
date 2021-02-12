using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShippingProcessor
{
    /// <summary>
    /// interface for a fake api service useful for mocking/testing
    /// </summary>
    public interface IFakeApiService
    {
        Task<string> GetQuote(string input);
    }

    /// <summary>
    /// fake api service useful for testing
    /// </summary>
    public class FakeApiService : IFakeApiService
    {
        public async Task<string> GetQuote(string input)
        {
            return "";
        }
    }
}
