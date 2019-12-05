using System.Collections.Generic;
using System.Threading.Tasks;
using Akka.Actor;
using akka_microservices_proj.Domain;
using akka_microservices_proj.Messages;

namespace akka_microservices_proj.Actors
{
    public class BasketForCustomerActor : ReceiveActor
    {
        public Basket Basket { get; set; }
        public BasketForCustomerActor(int customerId)
        {
            Receive<GetBasketMessage>(msg => Sender.Tell(GetBasketForCustomer(msg)));
        }

        public Basket GetBasketForCustomer(GetBasketMessage msg)
        {
            Basket = new Basket{ Id = msg.CustomerId, Products = new List<BasketProduct> { new BasketProduct {Products = new List<Product> {new Product{Stock = new Stock{StockAmount = 15}, Id = 1,Name = "test", Price = 1599}}}}};
            return Basket;
        }
    }
}
