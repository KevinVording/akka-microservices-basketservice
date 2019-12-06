using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Akka.Actor;
using akka_microservices_proj.Domain;
using akka_microservices_proj.Messages;
using akka_microservices_proj.Result;

namespace akka_microservices_proj.Actors
{
    public class BasketForCustomerActor : ReceiveActor
    {
        public Basket Basket { get; set; } = new Basket();
        private readonly IActorRef _productActor;
        public BasketForCustomerActor(long customerId, IActorRef productActor)
        {
            _productActor = productActor;
            ReceiveAsync<AddProductToBasketMessage>(msg => AddProductToBasket(msg).PipeTo(Sender));
            Receive<GetBasketMessage>(msg => Sender.Tell(GetBasketForCustomer(msg)));
        }

        private Basket GetBasketForCustomer(GetBasketMessage msg)
        {
            Basket.CustomerId = msg.CustomerId;
            return Basket;
        }

        private async Task<BasketResult> AddProductToBasket(AddProductToBasketMessage msg)
        {
            if (Basket.CustomerId.Equals(msg.CustomerId))
            {
                var product = await _productActor.Ask<Product>(new GetProductMessage
                    {ProductId = msg.Product.BasketProductId});
                Basket.Products.AddRange(Enumerable.Repeat(product, msg.Product.AmountInBasket));
                return new BasketProductAdded{CustomerId = msg.CustomerId, Message = "Product successfully added!"};
            }

            return new BasketProductNotFound();
        }
    }
}
