﻿using akka_microservices_proj.Domain;

namespace akka_microservices_proj.Messages
{
    public class AddProductToBasketMessage : CustomerMessage
    {
        public BasketProduct Product { get; set; }

        public AddProductToBasketMessage(long customerId) : base(customerId)
        {
        }
    }
}
