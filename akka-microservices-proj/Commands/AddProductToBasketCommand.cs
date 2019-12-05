using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace akka_microservices_proj.Commands
{
    public interface IAddProductToBasketCommand
    {
        Task<BasketResult> ExecuteAsync();
    }
    public class AddProductToBasketCommand : IAddProductToBasketCommand
    {
        public Task<BasketResult> ExecuteAsync()
        {
            throw new NotImplementedException();
        }
    }
}
