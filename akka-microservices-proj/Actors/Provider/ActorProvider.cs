using System.Collections.Generic;
using Akka.Actor;
using akka_microservices_proj.Domain;

namespace akka_microservices_proj.Actors.Provider
{
    /// <summary>
    /// Provider created to prevent already existent actor after page refresh
    /// </summary>
    public class ActorProvider
    {
        private IActorRef BasketActor { get; }
        private IActorRef ProductActor { get; }

        public ActorProvider(ActorSystem actorSystem)
        {
            ProductActor = actorSystem.ActorOf(Props.Create(() => new ProductActor(GetMockProducts())), "product");
            BasketActor = actorSystem.ActorOf(Props.Create(() => new BasketActor(ProductActor)), "basket");
        }

        /// <summary>
        /// Mock Products found on my desk
        /// ¯\_(ツ)_/¯
        /// </summary>
        /// <returns></returns>
        private List<Product> GetMockProducts()
        {
            return new List<Product>
            {
                new Product
                {
                    Id = 1,
                    Name = "Lamp",
                    Price = 2499,
                    Stock = new Stock
                    {
                        StockAmount = 15,
                    }
                },
                new Product
                {
                    Id = 2,
                    Name = "Controller",
                    Price = 5999,
                    Stock = new Stock
                    {
                        StockAmount = 1,
                    }
                },
                new Product
                {
                    Id = 3,
                    Name = "Mug",
                    Price = 1499,
                    Stock = new Stock
                    {
                        StockAmount = 20,
                    }
                },
                new Product
                {
                    Id = 4,
                    Name = "Rubik's Cube",
                    Price = 1299,
                    Stock = new Stock
                    {
                        StockAmount = 12,
                    }
                },
                new Product
                {
                    Id = 5,
                    Name = "Lamp",
                    Price = 2499,
                    Stock = new Stock
                    {
                        StockAmount = 8,
                    }
                },
            };
        }

        public IActorRef GetBasketActor()
        {
            return BasketActor;
        }

        public IActorRef GetProductActor()
        {
            return ProductActor;
        }
    }
}
