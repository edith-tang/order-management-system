using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class OrderHeader
    {
        /// <summary>
        /// Field: the collection of all order items of an order
        /// </summary>
        public List<OrderItem> OrderItems;

        /// <summary>
        /// Field: the OrderState of an order
        /// </summary>
        public OrderState State;        

        /// <summary>
        /// Property: the DateTime of an order
        /// </summary>
        public DateTime DateTime { get; private set; }

        /// <summary>
        /// Property: the id of an order
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Property: the total price of all order items of an order
        /// </summary>
        public decimal Total
        {
            get 
            {
                decimal total = 0;
                foreach (var oItem in OrderItems)
                {
                    total += oItem.Total;
                }
                return total;
            }
        }

        /// <summary>
        /// Property: the total number of all order items of an order
        /// </summary>
        public int TotalItems
        {
            get
            {
                int totalItems = 0;
                foreach (var oItem in OrderItems)
                {
                    totalItems += oItem.Quantity;
                }
                return totalItems;
            }
        }

        /// <summary>
        /// Property: the OrderStates of an order
        /// </summary>
        public OrderStates StateEnum
        {
            get
            {
                return State.StateEnum;
            }
        }

        /// <summary>
        /// The OrderHeader constructor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dateTime"></param>
        /// <param name="stateNum"></param>
        public OrderHeader(int id, DateTime dateTime, int stateNum)
        {
            Id = id;
            DateTime = dateTime;            
            setState(stateNum);
            OrderItems = new List<OrderItem>();
        }

        /// <summary>
        /// Set the state of an order based on stateNum upon constructing the order object.
        /// </summary>
        /// <param name="stateNum"></param>
        private void setState(int stateNum)
        {
            switch (stateNum)
            {
                case 1: State = new OrderNew(this); break;
                case 2: State = new OrderPending(this); break;
                case 3: State = new OrderRejected(this); break;
                case 4: State = new OrderComplete(this); break;
                default: throw new InvalidOrderStateException("Invalid state number:" + stateNum);
            }
        }
        
        /// <summary>
        /// Update the state of an order to "Complete".
        /// </summary>
        public void Complete()
        {
            State.Complete(ref State);
        }

        /// <summary>
        /// Update the state of an order to "Rejected".
        /// </summary>
        public void Reject()
        {
            State.Reject(ref State);
        }

        /// <summary>
        /// Submit an order.
        /// </summary>
        public void Submit()
        {
            State.Submit(ref State);
        }

        /// <summary>
        /// Add a new order item to the order, or update the quantity of an existing order item in the order.
        /// </summary>
        /// <param name="stockItemId"></param>
        /// <param name="description"></param>
        /// <param name="price"></param>
        /// <param name="quantity"></param>
        /// <returns>The order item being added or updated.</returns>
        public OrderItem AddOrUpdateOrderItem(int stockItemId, string description, decimal price, int quantity)
        {
            var item = OrderItems.FirstOrDefault(i => i.StockItemId == stockItemId);
            if (item != null)
            {
                item.Quantity = quantity;
            }
            else
            {
                item = new OrderItem(this, stockItemId, description, price, quantity);
                OrderItems.Add(item);
            }
            return item;
        }

    }
}
