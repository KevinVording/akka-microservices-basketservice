namespace akka_microservices_proj.Messages
{
    public class GetBasketMessage : CustomerMessage
    {
        public GetBasketMessage(long customerId) : base(customerId)
        {
        }
    }
}
