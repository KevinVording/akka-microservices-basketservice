namespace akka_microservices_proj.Domain
{
    public class Product
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public Stock Stock { get; set; }
    }
}
