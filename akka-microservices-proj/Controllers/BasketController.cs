using System;
using System.Threading.Tasks;
using akka_microservices_proj.Commands;
using akka_microservices_proj.Messages;
using Microsoft.AspNetCore.Mvc;

namespace akka_microservices_proj.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BasketController : Controller
    {
        private readonly Lazy<IGetBasketFromCustomerCommand> _getBasketFromCustomerCommand;
        public BasketController(Lazy<IGetBasketFromCustomerCommand> getBasketFromCustomerCommand)
        {
            _getBasketFromCustomerCommand = getBasketFromCustomerCommand;
        }

        // Get the basket from customer
        [HttpGet("{customerId}")]
        public async Task<IActionResult> Get(int customerId) =>
            await _getBasketFromCustomerCommand.Value.ExecuteAsync(new GetBasketMessage {CustomerId = customerId});

        // Place product in the basket.
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {

        }

        // Remove product from the basket.
        [HttpDelete("{id}")]
        public void Delete(int id)
        {

        }
    }

    //public interface IGetBasketFromCustomerCommand
    //{
    //    Task<IActionResult> ExecuteAsync(int customerId);
    //}

    //public class GetBasketFromCustomerCommand : IGetBasketFromCustomerCommand
    //{
    //    private readonly IActorRef _basketActor;

    //    public GetBasketFromCustomerCommand(IActorRef basketActor) => _basketActor = basketActor;

    //    public Task<IActionResult> ExecuteAsync(int customerId)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}
