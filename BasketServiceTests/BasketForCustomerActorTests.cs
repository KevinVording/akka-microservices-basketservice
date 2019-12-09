using Akka.Actor;
using Akka.TestKit.NUnit3;
using akka_microservices_proj.Actors;
using akka_microservices_proj.Domain;
using akka_microservices_proj.Messages;
using akka_microservices_proj.Result;
using NUnit.Framework;
using System.Collections.Generic;

namespace BasketServiceTests
{
    public class BasketForCustomerActorTests : TestKit
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
        public void GetBasketForCustomer_HappyFlow()
        {
            _basketForCustomerActor.Tell(new GetBasketMessage(_customerId1));
            var result = ExpectMsg<Basket>();

            Assert.AreEqual(result.CustomerId, _customerId1);
        }

        [Test]
        public void AddProductToBasket_HappyFlow()
        {
            _basketForCustomerActor.Tell(new GetBasketMessage(_customerId1));
            ExpectMsg<Basket>();
            _basketForCustomerActor.Tell(new AddProductToBasketMessage(_customerId1)
            {
                CustomerId = _customerId1,
                Product = new BasketProduct { BasketProductId = 1, AmountAdded = 2, AmountRemoved = 0, Name = "Lamp", Price = 2499 }
            });
            var result = ExpectMsg<BasketResult>();

            Assert.IsTrue(result.GetType() == typeof(BasketProductAdded));
        }

        [Test]
        public void RemoveProductFromBasket_HappyFlow()
        {
            _basketForCustomerActor.Tell(new GetBasketMessage(_customerId1));
            ExpectMsg<Basket>();
            _basketForCustomerActor.Tell(new AddProductToBasketMessage(_customerId1)
            {
                CustomerId = _customerId1,
                Product = new BasketProduct { BasketProductId = 1, AmountAdded = 2, AmountRemoved = 0, Name = "Lamp", Price = 2499 }
            });
            ExpectMsg<BasketResult>();
            _basketForCustomerActor.Tell(new RemoveProductFromBasketMessage(_customerId1)
            {
                CustomerId = _customerId1,
                Product = new BasketProduct { BasketProductId = 1, AmountAdded = 0, AmountRemoved = 1, Name = "Lamp", Price = 2499 }
            });
            var result = ExpectMsg<BasketResult>();

            Assert.IsTrue(result.GetType() == typeof(BasketProductRemoved));
        }

        /// <summary>
        /// This logic also applies for Remove product with non-existent basket
        /// </summary>
        [Test]
        public void AddProductToBasket_NonExistentBasket()
        {
            _basketForCustomerActor.Tell(new AddProductToBasketMessage(_customerId1)
            {
                CustomerId = _customerId1,
                Product = new BasketProduct { BasketProductId = 1, AmountAdded = 2, AmountRemoved = 0, Name = "Lamp", Price = 2499 }
            });
            var result = ExpectMsg<BasketResult>();

            Assert.IsTrue(result.GetType() == typeof(BasketDoesNotExist));
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