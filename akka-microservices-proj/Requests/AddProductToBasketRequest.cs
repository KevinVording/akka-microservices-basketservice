using akka_microservices_proj.Domain;

namespace akka_microservices_proj.Requests
{
    public class AddProductToBasketRequest
    {
        public long CustomerId { get; set; }
        public BasketProduct Product { get; set; }
    }
}
