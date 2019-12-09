namespace akka_microservices_proj.Result
{
    public abstract class ProductResult
    {
    }

    public class ProductFound : ProductResult { }
    public class ProductNotFound : ProductResult { }
    public class ProductOutOfStock : ProductResult { }
    public class ProductInsufficientStock : ProductResult { }
    public class ProductStockUpdated : ProductResult { }
}
