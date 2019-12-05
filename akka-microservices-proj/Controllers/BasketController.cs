using System;
using System.Threading.Tasks;
using akka_microservices_proj.Commands;
using akka_microservices_proj.Domain;
using akka_microservices_proj.Messages;
using Microsoft.AspNetCore.Mvc;

namespace akka_microservices_proj.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BasketController : Controller
    {
        private readonly Lazy<IGetBasketFromCustomerCommand> _getBasketFromCustomerCommand;
        private readonly Lazy<IAddProductToBasketCommand> _addProductToBasketCommand;
        public BasketController(Lazy<IGetBasketFromCustomerCommand> getBasketFromCustomerCommand, Lazy<IAddProductToBasketCommand> addProductToBasketCommand)
        {
            _getBasketFromCustomerCommand = getBasketFromCustomerCommand;
            _addProductToBasketCommand = addProductToBasketCommand;
        }

        // Get the basket from customer
        [HttpGet("{customerId}")]
        public async Task<IActionResult> Get(int customerId) =>
            await _getBasketFromCustomerCommand.Value.ExecuteAsync(new GetBasketMessage(customerId));

        // Place product in the basket.
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Product product) =>
            await _addProductToBasketCommand.Value.ExecuteAsync(new AddProductToBasketMessage(id)
                {Product = product});

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
