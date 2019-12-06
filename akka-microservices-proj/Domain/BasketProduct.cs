using System.Collections.Generic;

namespace akka_microservices_proj.Domain
{
    /// <summary>
    /// BasketProduct has no need for Stock, hence an extra domain for BasketProducts
    /// </summary>
    public class BasketProduct
    {
        public long BasketProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int AmountAdded { get; set; }
        public int AmountRemoved { get; set; }
    }
}
