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
        private readonly Lazy<IRemoveProductFromBasketCommand> _removeProductFromBasketCommand;
        public BasketController(Lazy<IGetBasketFromCustomerCommand> getBasketFromCustomerCommand,
            Lazy<IAddProductToBasketCommand> addProductToBasketCommand,
            Lazy<IRemoveProductFromBasketCommand> removeProductFromBasketCommand)
        {
            _getBasketFromCustomerCommand = getBasketFromCustomerCommand;
            _addProductToBasketCommand = addProductToBasketCommand;
            _removeProductFromBasketCommand = removeProductFromBasketCommand;
        }

        /// <summary>
        /// Get the basket from customer
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        [HttpGet("{customerId}")]
        public async Task<IActionResult> Get(int customerId) => await _getBasketFromCustomerCommand.Value.ExecuteAsync(new GetBasketMessage(customerId));

        /// <summary>
        /// Place product in the basket.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPut("{customerId}")]
        public async Task<IActionResult> Put(int customerId, [FromBody] BasketProduct product) =>
            await _addProductToBasketCommand.Value.ExecuteAsync(new AddProductToBasketMessage(customerId) { Product = product });

        /// <summary>
        /// Remove product from the basket.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpDelete("{customerId}")]
        public async Task<IActionResult> Delete(int customerId, [FromBody] BasketProduct product) =>
            await _removeProductFromBasketCommand.Value.ExecuteAsync(new RemoveProductFromBasketMessage(customerId) { Product = product });
    }
}
