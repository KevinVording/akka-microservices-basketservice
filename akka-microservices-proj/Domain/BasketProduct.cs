using System.Collections.Generic;

namespace akka_microservices_proj.Domain
{
    public class BasketProduct
    {
        public BasketProduct()
        {
            Products = new List<Product>();
        }
        public IEnumerable<Product> Products { get; set; }
    }
}
