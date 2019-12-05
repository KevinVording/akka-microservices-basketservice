using System;
using System.Threading.Tasks;
using Akka.Actor;
using akka_microservices_proj.Actors.Provider;
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
            var productResult = await _productActor.Ask<ProductResult>(new CheckProductMessage(msg.CustomerId){ Product = msg.Product});

            if (productResult.GetType() == typeof(ProductFound))
            {
                var result = await _basketActor.Ask<BasketResult>(msg);
                if (result.GetType() == typeof(BasketProductAdded))
                    return new OkObjectResult(result);
            }

            return new BadRequestResult();
        }
    }
}
