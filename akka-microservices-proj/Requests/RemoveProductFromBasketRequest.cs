using akka_microservices_proj.Domain;

namespace akka_microservices_proj.Requests
{
    public class RemoveProductFromBasketRequest
    {
        public long CustomerId { get; set; }
        public BasketProduct Product { get; set; }
    }
}
