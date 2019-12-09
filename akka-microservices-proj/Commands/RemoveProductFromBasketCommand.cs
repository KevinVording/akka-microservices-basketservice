using System.Threading.Tasks;
using Akka.Actor;
using akka_microservices_proj.Actors.Provider;
using akka_microservices_proj.Domain;
using akka_microservices_proj.Messages;
using akka_microservices_proj.Result;
using Microsoft.AspNetCore.Mvc;

namespace akka_microservices_proj.Commands
{
    public interface IRemoveProductFromBasketCommand
    {
        Task<IActionResult> ExecuteAsync(RemoveProductFromBasketMessage msg);
    }

    public class RemoveProductFromBasketCommand : IRemoveProductFromBasketCommand
    {
        private readonly IActorRef _productActor;
        private readonly IActorRef _basketActor;

        public RemoveProductFromBasketCommand(ActorProvider actorProvider)
        {
            _productActor = actorProvider.GetProductActor();
            _basketActor = actorProvider.GetBasketActor();
        }
        public async Task<IActionResult> ExecuteAsync(RemoveProductFromBasketMessage msg)
        {
            if (msg.Product.AmountRemoved > 0 && msg.Product.AmountAdded > 0)
                return new BadRequestObjectResult("Please do not add AND remove product at the same time.");

            var product = await _productActor.Ask<Product>(new GetProductMessage
                { ProductId = msg.Product.BasketProductId, Name = msg.Product.Name, Price = msg.Product.Price });
            var productResult = await _productActor.Ask<ProductResult>(new CheckProductMessage(msg.CustomerId) { Product = product, BasketProductAmount = msg.Product.AmountRemoved });

            if (productResult.GetType() == typeof(ProductFound))
            {
                var result = await _basketActor.Ask<BasketResult>(msg);
                if (result.GetType() == typeof(BasketProductRemoved))
                    return new OkObjectResult(result);
            }

            return new BadRequestResult();
        }
    }
}
