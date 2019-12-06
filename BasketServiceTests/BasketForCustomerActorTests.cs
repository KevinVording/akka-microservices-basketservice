using Akka.Actor;
using Akka.TestKit.NUnit3;
using akka_microservices_proj.Actors;
using akka_microservices_proj.Domain;
using akka_microservices_proj.Messages;
using NUnit.Framework;

namespace BasketServiceTests
{
    public class BasketForCustomerActorTests : TestKit
    {
        private IActorRef _identity;
        private IActorRef _productActor;
        private long _customerId1, _customerId2;

        [SetUp]
        public void Setup()
        {
            _productActor = CreateTestProbe("products").Ref;
            _identity = Sys.ActorOf(Props.Create(() => new BasketForCustomerActor(_customerId1, _productActor)));
            _customerId1 = 1;
            _customerId2 = 2;
        }

        [Test]
        public void GetBasketForCustomer_HappyFlow()
        {
            _identity.Tell(new GetBasketMessage(_customerId1));
            var result = ExpectMsg<Basket>();

            Assert.AreEqual(result.CustomerId, _customerId1);
        }

        [Test]
        public void AddProductToBasket_HappyFlow()
        {

        }
    }
}