using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class OrderPending: OrderState
    {
        public override OrderStates StateEnum => OrderStates.Pending;

        /// <summary>
        /// The OrderPending constructor
        /// </summary>
        /// <param name="orderHeader"></param>
        public OrderPending(OrderHeader orderHeader) :base(orderHeader)
        {            
        }

        /// <summary>
        /// Process a pending order and update its state to "complete".
        /// </summary>
        /// <param name="State"></param>
        public override void Complete(ref OrderState State)
        {
            State = new OrderComplete(_orderHeader);            
        }

        /// <summary>
        /// Process a pending order and update its state to "complete".
        /// </summary>
        /// <param name="State"></param>
        public override void Reject(ref OrderState State)
        {
            State = new OrderRejected(_orderHeader);
        }

        /// <summary>
        /// Throws an exception when the user attempts to submit a pending order.
        /// </summary>
        /// <param name="State"></param>
        public override void Submit(ref OrderState State)
        {
            throw new InvalidOrderStateException("Can't submit a pending order.");
        }
    }
}