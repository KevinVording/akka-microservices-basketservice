using Akka.Actor;
using Akka.TestKit.NUnit3;
using akka_microservices_proj.Actors;
using akka_microservices_proj.Domain;
using akka_microservices_proj.Messages;
using akka_microservices_proj.Result;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasketServiceTests
{
    public class ProductActorTests : TestKit
    {
        private IActorRef _productActor;
        private long _customerId1;

        [SetUp]
        public void Setup()
        {
            _productActor = Sys.ActorOf(Props.Create(() => new ProductActor(GetMockProducts())));
            _customerId1 = 1;
        }

        [Test]
        public void GetProduct_HappyFlow()
        {
            _productActor.Tell(new GetProductMessage { ProductId = 1, Name = "Lamp", Price = 2499 });
            var result = ExpectMsg<Product>();

            Assert.IsTrue(result.Id.Equals(1));
        }

        [Test]
        public void GetProducts_HappyFlow()
        {
            _productActor.Tell(new GetProductsMessage());
            var result = ExpectMsg<List<Product>>();
            var products = GetMockProducts();

            // Normale assertions op collecties gaan mis.
            products.Should().BeEquivalentTo(result);
        }

        [Test]
        public void CheckProduct_HappyFlow()
        {
            _productActor.Tell(new CheckProductMessage(1) { CustomerId = _customerId1, ProductAdded = true, BasketProductAmount = 2, Product = new Product
            {
                Id = 1,
                Name = "Lamp",
                Price = 2499,
                Stock = new Stock { StockAmount = 15 }
            }});
            var result = ExpectMsg<ProductResult>();

            Assert.IsTrue(result.GetType() == typeof(ProductFound));
        }

        [Test]
        public void CheckProduct_ProductNotFound()
        {
            _productActor.Tell(new CheckProductMessage(1)
            {
                CustomerId = _customerId1,
                ProductAdded = true,
                BasketProductAmount = 2,
                Product = new Product
                {
                    Id = 6,
                    Name = "Pepsi",
                    Price = 199,
                    Stock = new Stock { StockAmount = 5 }
                }
            });
            var result = ExpectMsg<ProductResult>();

            Assert.IsTrue(result.GetType() == typeof(ProductNotFound));
        }

        [Test]
        public void CheckProduct_ProductInsufficientStock()
        {
            _productActor.Tell(new CheckProductMessage(1)
            {
                CustomerId = _customerId1,
                ProductAdded = true,
                BasketProductAmount = 16,
                Product = new Product
                {
                    Id = 1,
                    Name = "Lamp",
                    Price = 2499,
                    Stock = new Stock { StockAmount = 15 }
                }
            });
            var result = ExpectMsg<ProductResult>();

            Assert.IsTrue(result.GetType() == typeof(ProductInsufficientStock));
        }

        [Test]
        public void CheckProduct_ProductOutOfStock()
        {
            _productActor.Tell(new CheckProductMessage(1)
            {
                CustomerId = _customerId1,
                ProductAdded = true,
                BasketProductAmount = 1,
                Product = new Product
                {
                    Id = 2,
                    Name = "Controller",
                    Price = 5999,
                    Stock = new Stock
                    {
                        StockAmount = 0,
                    }
                }
            });
            var result = ExpectMsg<ProductResult>();

            Assert.IsTrue(result.GetType() == typeof(ProductOutOfStock));
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
                        StockAmount = 0,
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
