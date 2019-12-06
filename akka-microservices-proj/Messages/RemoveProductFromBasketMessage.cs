using akka_microservices_proj.Domain;

namespace akka_microservices_proj.Messages
{
    public class RemoveProductFromBasketMessage : CustomerMessage
    {
        public RemoveProductFromBasketMessage(long customerId) : base(customerId)
        {
        }

        public BasketProduct Product { get; set; }
    }
}
