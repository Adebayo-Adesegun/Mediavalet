using System;

namespace MediaValet.Core.Entities
{
    public class Order
    {
        public int OrderId { get;  set; }
        public int RandomNumber { get; set; } 
        public string OrderText { get; set; }

        public Order(string orderText, int orderId)
        {
            OrderId = orderId;
            RandomNumber = new Random().Next(1, 10);
            OrderText = orderText;
        }

    }
}
