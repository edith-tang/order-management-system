using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class OrderItem
    {            
        public OrderHeader OrderHeader { get; private set; }
        public int OrderHeaderId { get; private set; }
        public int StockItemId { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public int Quantity { get; set; }
        public decimal Total => Price * Quantity;

        /// <summary>
        /// The OrderItem constructor
        /// </summary>
        /// <param name="order"></param>
        /// <param name="stockItemId"></param>
        /// <param name="description"></param>
        /// <param name="price"></param>
        /// <param name="quantity"></param>
        public OrderItem (OrderHeader order, int stockItemId, string description, decimal price, int quantity)
        {
            OrderHeader = order;
            OrderHeaderId = OrderHeader.Id;
            StockItemId = stockItemId;
            Description = description;
            Price = price;
            Quantity = quantity;
        }
              
    }
}
