using System.Threading.Tasks;
using Akka.Actor;
using akka_microservices_proj.Actors.Provider;
using akka_microservices_proj.Messages;

namespace akka_microservices_proj.Actors
{
    public class BasketActor : ReceiveActor
    {
        private readonly IActorRef _productActorRef;
        public BasketActor(IActorRef productActor)
        {
            _productActorRef = productActor;
            //ReceiveAny(msg =>
            //{
            //    if (msg is CustomerMessage)
            //    {
            //        // do nothing
            //        var test = (CustomerMessage)msg;
            //    }
            //});
            Receive<CustomerMessage>(GetBasketForCustomer);
        }

        private void GetBasketForCustomer(CustomerMessage msg)
        {
            var actor = Context.Child(msg.CustomerId.ToString()) is Nobody ? Context.ActorOf(Props.Create(() => new BasketForCustomerActor(msg.CustomerId, _productActorRef)), msg.CustomerId.ToString()) : Context.Child(msg.CustomerId.ToString());
            actor.Forward(msg);
        }
    }
}
