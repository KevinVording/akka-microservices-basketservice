using akka_microservices_proj.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace akka_microservices_proj.Messages
{
    public class UpdateStockMessage : CustomerMessage
    {
        public Product Product { get; set; }
        public int BasketProductAmount { get; set; }
        public bool ProductAdded { get; set; } = false;

        public UpdateStockMessage(long customerId) : base(customerId)
        {
        }
    }
}
