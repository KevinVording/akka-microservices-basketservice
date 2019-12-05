using System;
using System.Collections.Generic;
using System.Linq;
using Akka.Actor;
using akka_microservices_proj.Domain;
using akka_microservices_proj.Messages;
using akka_microservices_proj.Result;

namespace akka_microservices_proj.Actors
{
    public class ProductActor : ReceiveActor
    {
        public List<Product> Products { get; set; }

        public ProductActor(List<Product> products)
        {
            Products = products;

            Receive<CheckProductMessage>(msg => Sender.Tell(AddProductToBasket(msg)));
        }

        private ProductResult AddProductToBasket(CheckProductMessage msg)
        {
            if (Products.Exists(x => x.Id.Equals(msg.Product.Id)))
                return new ProductFound();

            return new ProductNotFound();
        }
    }
}
