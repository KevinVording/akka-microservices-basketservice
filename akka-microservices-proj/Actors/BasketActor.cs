using System.Threading.Tasks;
using Akka.Actor;
using akka_microservices_proj.Messages;

namespace akka_microservices_proj.Actors
{
    public class BasketActor : ReceiveActor
    {
        public BasketActor()
        {
            //ReceiveAny(msg =>
            //{
            //    if (msg.GetType() == typeof(GetBasketMessage))
            //    {
            //        var message = (GetBasketMessage) msg;
            //        var actor = Context.Child(message.CustomerId.ToString()) is Nobody
            //            ? Context.ActorOf(Props.Create(() => new BasketForCustomerActor(message.CustomerId)),
            //                message.CustomerId.ToString())
            //            : Context.Child(message.CustomerId.ToString());
            //        actor.Forward(msg);
            //    }
            //});
            Receive<GetBasketMessage>(GetBasketForCustomer);
        }

        public void GetBasketForCustomer(GetBasketMessage msg)
        {
            var actor = Context.Child(msg.CustomerId.ToString()) is Nobody ? Context.ActorOf(Props.Create(() => new BasketForCustomerActor(msg.CustomerId)), msg.CustomerId.ToString()) : Context.Child(msg.CustomerId.ToString());
            actor.Forward(msg);
        }
    }
}
