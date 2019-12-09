namespace akka_microservices_proj.Messages
{
    public class GetProductMessage
    {
        public long ProductId { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
    }
}
