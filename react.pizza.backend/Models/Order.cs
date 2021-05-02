using System.Collections.Generic;

namespace react.pizza.backend.Models
{
    public class Order
    {
        public IEnumerable<OrderItem> Items { get; set; }
    }

    public class OrderItem
    {
        public int Id { get; set; }
        public int Count { get; set; }
    }
}