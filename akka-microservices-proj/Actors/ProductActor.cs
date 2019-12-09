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

            Receive<GetProductsMessage>(msg => Sender.Tell(GetProducts(msg)));
            Receive<GetProductMessage>(msg => Sender.Tell(GetProduct(msg)));
            Receive<CheckProductMessage>(msg => Sender.Tell(CheckProduct(msg)));
            Receive<UpdateStockMessage>(msg => Sender.Tell(UpdateStock(msg)));
        }

        private Product GetProduct(GetProductMessage msg)
        {
            return Products.FirstOrDefault(x => x.Id.Equals(msg.ProductId) && x.Name.Equals(msg.Name) && x.Price.Equals(msg.Price));
        }

        private List<Product> GetProducts(GetProductsMessage msg)
        {
            return Products;
        }

        private ProductResult CheckProduct(CheckProductMessage msg)
        {
            if (Products.Exists(x => x.Id.Equals(msg.Product.Id)))
            {
                var index = Products.FindIndex(i => i.Id.Equals(msg.Product.Id));
                var product = Products[index];
                if (product.Stock.StockAmount <= 0)
                    return new ProductOutOfStock();
                if (product.Stock.StockAmount < msg.BasketProductAmount)
                    return new ProductInsufficientStock();

                return new ProductFound();
            }

            return new ProductNotFound();
        }

        private ProductResult UpdateStock(UpdateStockMessage msg)
        {
            if (Products.Exists(x => x.Id.Equals(msg.Product.Id)))
            {
                var index = Products.FindIndex(i => i.Id.Equals(msg.Product.Id));
                var product = Products[index];

                    if (msg.ProductAdded)
                    Products[index].Stock.StockAmount -= msg.BasketProductAmount;
                if (!msg.ProductAdded)
                    Products[index].Stock.StockAmount += msg.BasketProductAmount;

                return new ProductStockUpdated();
            }

            return new ProductNotFound();
        }
    }
}
