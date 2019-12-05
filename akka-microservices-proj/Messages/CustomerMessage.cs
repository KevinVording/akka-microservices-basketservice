namespace akka_microservices_proj.Messages
{
    public abstract class CustomerMessage
    {
        public CustomerMessage(long customerId)
        {
            CustomerId = customerId;
        }
        public long CustomerId { get; set; }
    }
}
