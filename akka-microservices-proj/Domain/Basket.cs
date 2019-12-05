using System.Collections.Generic;

namespace akka_microservices_proj.Domain
{
    public class Basket
    {
        public Basket()
        {
            Products = new List<Product>();
        }
        public long CustomerId { get; set; }
        public List<Product> Products { get; set; }
    }
}
