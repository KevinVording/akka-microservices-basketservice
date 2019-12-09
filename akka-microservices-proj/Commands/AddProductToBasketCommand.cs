using System.Threading.Tasks;
using Akka.Actor;
using akka_microservices_proj.Actors.Provider;
using akka_microservices_proj.Domain;
using akka_microservices_proj.Messages;
using akka_microservices_proj.Result;
using Microsoft.AspNetCore.Mvc;

namespace akka_microservices_proj.Commands
{
    public interface IAddProductToBasketCommand
    {
        Task<IActionResult> ExecuteAsync(AddProductToBasketMessage msg);
    }

    public class AddProductToBasketCommand : IAddProductToBasketCommand
    {
        private readonly IActorRef _productActor;
        private readonly IActorRef _basketActor;

        public AddProductToBasketCommand(ActorProvider actorProvider)
        {
            _productActor = actorProvider.GetProductActor();
            _basketActor = actorProvider.GetBasketActor();
        }

        public async Task<IActionResult> ExecuteAsync(AddProductToBasketMessage msg)
        {
            if (msg.Product.AmountRemoved > 0 && msg.Product.AmountAdded > 0)
                return new BadRequestObjectResult("Please do not add AND remove product at the same time.");

            var product = await _productActor.Ask<Product>(new GetProductMessage
                {ProductId = msg.Product.BasketProductId, Name = msg.Product.Name, Price = msg.Product.Price });

            if (product == null)
                return new BadRequestObjectResult("Nonexistent Product");

            var productResult = await _productActor.Ask<ProductResult>(new CheckProductMessage(msg.CustomerId){ Product = product, BasketProductAmount = msg.Product.AmountAdded, ProductAdded = true });

            if (productResult.GetType() == typeof(ProductFound))
            {
                var result = await _basketActor.Ask<BasketResult>(msg);
                if (result.GetType() == typeof(BasketProductAdded))
                {
                    var stockResult = await _productActor.Ask<ProductResult>(new UpdateStockMessage(msg.CustomerId) { CustomerId = msg.CustomerId, BasketProductAmount = msg.Product.AmountAdded,  Product = product, ProductAdded = true });
                    if (stockResult.GetType() == typeof(ProductNotFound))
                        return new BadRequestObjectResult("Product not found.");

                    return new OkObjectResult(result);
                }
                if (result.GetType() == typeof(BasketDoesNotExist))
                    return new BadRequestObjectResult(result);
            }
            if (productResult.GetType() == typeof(ProductNotFound))
            {
                return new BadRequestObjectResult("No such Product");
            }
            if (productResult.GetType() == typeof(ProductOutOfStock))
            {
                return new BadRequestObjectResult("No Stock");
            }
            if (productResult.GetType() == typeof(ProductInsufficientStock))
            {
                return new BadRequestObjectResult("Insufficient Stock");
            }

            return new BadRequestObjectResult("Something has gone wrong");
        }
    }
}
