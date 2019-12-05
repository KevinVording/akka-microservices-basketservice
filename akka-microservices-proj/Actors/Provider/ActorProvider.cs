using Akka.Actor;

namespace akka_microservices_proj.Actors.Provider
{
    /// <summary>
    /// Provider created to prevent already existent actor after page refresh
    /// </summary>
    public class ActorProvider
    {
        private IActorRef BasketActor { get; }
        private IActorRef ProductActor { get; }

        public ActorProvider(ActorSystem actorSystem)
        {
            BasketActor = actorSystem.ActorOf(Props.Create(() => new BasketActor()), "basket");
            ProductActor = actorSystem.ActorOf(Props.Create(() => new BasketActor()), "product"); ;
        }

        public IActorRef GetBasketActor()
        {
            return BasketActor;
        }

        public IActorRef GetProductActor()
        {
            return ProductActor;
        }
    }
}
