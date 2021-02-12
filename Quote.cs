namespace ShippingProcessor
{
    /// <summary>
    /// quote model used to store shipper id and their provided quote
    /// </summary>
    public class Quote
    {
        public decimal Amount { get; set; }
        public string ShipperId { get; set; }
    }
}