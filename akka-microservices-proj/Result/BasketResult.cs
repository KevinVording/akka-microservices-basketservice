namespace akka_microservices_proj.Result
{
    public abstract class BasketResult
    {
        public long CustomerId { get; set; }
        public string Message { get; set; }
    }

    public class BasketDoesNotExist : BasketResult
    {

    }

    public class BasketProductFound : BasketResult
    {

    }

    public class BasketProductNotFound : BasketResult
    {

    }

    public class BasketProductAdded : BasketResult
    {

    }

    public class BasketProductRemoved : BasketResult
    {

    }
}
