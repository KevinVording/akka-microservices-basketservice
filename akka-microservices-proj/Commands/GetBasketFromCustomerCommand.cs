using System.Threading.Tasks;
using Akka.Actor;
using akka_microservices_proj.Actors.Provider;
using akka_microservices_proj.Domain;
using akka_microservices_proj.Messages;
using Microsoft.AspNetCore.Mvc;

namespace akka_microservices_proj.Commands
{
    public interface IGetBasketFromCustomerCommand
    {
        Task<IActionResult> ExecuteAsync(GetBasketMessage msg);
    }

    public class GetBasketFromCustomerCommand : IGetBasketFromCustomerCommand
    {
        private readonly IActorRef _basketActor;

        public GetBasketFromCustomerCommand(ActorProvider actorProvider)
        {
            _basketActor = actorProvider.GetBasketActor();
        }

        public async Task<IActionResult> ExecuteAsync(GetBasketMessage msg)
        {
            var result = await _basketActor.Ask<Basket>(msg);
            return new OkObjectResult(result);
        }
    }
}
