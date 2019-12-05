using akka_microservices_proj.Domain;

namespace akka_microservices_proj.Messages
{
    public class CheckProductMessage : CustomerMessage
    {
        public Product Product { get; set; }

        public CheckProductMessage(long customerId) : base(customerId)
        {
        }
    }
}
