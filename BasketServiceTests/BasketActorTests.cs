using System;
using System.Collections.Generic;
using Akka.Actor;
using Akka.TestKit.NUnit3;
using akka_microservices_proj.Actors;
using akka_microservices_proj.Domain;
using akka_microservices_proj.Messages;
using BasketServiceTests;
using NUnit.Framework;

namespace BasketServiceTests
{
    public class BasketActorTests : TestKit
    {
        private IActorRef _basketForCustomerActor, _productActor;
        private long _customerId1;

        [SetUp]
        public void Setup()
        {
            _productActor = Sys.ActorOf(Props.Create(() => new ProductActor(GetMockProducts())));
            _customerId1 = 1;
            _basketForCustomerActor = Sys.ActorOf(Props.Create(() => new BasketForCustomerActor(_customerId1, _productActor)));
        }

        [Test]
        public void GetBasketFromChildBasketActorForCustomer_HappyFlow()
        {
            _basketForCustomerActor.Tell(new GetBasketMessage(_customerId1));
            var result = ExpectMsg<Basket>();

            Assert.AreEqual(result.CustomerId, _customerId1);
        }

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
                    Name = "MousePad",
                    Price = 2499,
                    Stock = new Stock
                    {
                        StockAmount = 8,
                    }
                },
            };
        }
    }
}
