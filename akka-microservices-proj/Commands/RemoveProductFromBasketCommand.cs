using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace akka_microservices_proj.Commands
{
    public interface IRemoveProductFromBasketCommand
    {
        Task<IActionResult> ExecuteAsync();
    }

    public class RemoveProductFromBasketCommand
    {

    }
}
