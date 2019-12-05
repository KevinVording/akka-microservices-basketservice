using System.Collections.Generic;

namespace akka_microservices_proj.Domain
{
    public class Basket
    {
        public long Id { get; set; }
        public List<BasketProduct> Products { get; set; }
    }
}
