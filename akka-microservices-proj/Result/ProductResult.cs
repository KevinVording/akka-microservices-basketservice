using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace akka_microservices_proj.Result
{
    public abstract class ProductResult
    {
    }

    public class ProductFound : ProductResult { }
    public class ProductNotFound : ProductResult { }
}
