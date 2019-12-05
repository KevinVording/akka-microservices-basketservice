using System.Collections.Generic;
using System.Threading.Tasks;
using Akka.Actor;
using akka_microservices_proj.Domain;
using akka_microservices_proj.Messages;
using akka_microservices_proj.Result;

namespace akka_microservices_proj.Actors
{
    public class BasketForCustomerActor : ReceiveActor
    {
        public Basket Basket { get; set; }
        private readonly IActorRef _productActor;
        public BasketForCustomerActor(long customerId, IActorRef productActor)
        {
            _productActor = productActor;
            Basket = new Basket();
            Receive<AddProductToBasketMessage>(msg => Sender.Tell(AddProductToBasket(msg)));
            Receive<GetBasketMessage>(msg => Sender.Tell(GetBasketForCustomer(msg)));
        }

        private Basket GetBasketForCustomer(GetBasketMessage msg)
        {
            Basket.CustomerId = msg.CustomerId;
            return Basket;
        }

        private BasketResult AddProductToBasket(AddProductToBasketMessage msg)
        {
            if (Basket.CustomerId.Equals(msg.CustomerId))
            {
                Basket.Products.Add(msg.Product);
                return new BasketProductAdded();
            }

            return new BasketProductNotFound();
        }
    }
}
