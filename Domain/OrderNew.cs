using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class OrderNew: OrderState
    {
        public override OrderStates StateEnum => OrderStates.New;

        /// <summary>
        /// The OrderNew constructor
        /// </summary>
        /// <param name="orderHeader"></param>
        public OrderNew(OrderHeader orderHeader) : base(orderHeader)
        { 
        }

        /// <summary>
        /// Throws an exception when the user attempts to mark a new order as "complete".
        /// </summary>
        /// <param name="State"></param>
        public override void Complete(ref OrderState State)
        {
            throw new InvalidOrderStateException("Can't mark a new order complete.");
        }

        /// <summary>
        /// Throws an exception when the user attempts to mark a new order as "rejected".
        /// </summary>
        /// <param name="State"></param>
        public override void Reject(ref OrderState State)
        {
            throw new InvalidOrderStateException("Can't mark a new order rejected.");
        }

        /// <summary>
        /// Submit a new order and updates its state to "pending".
        /// </summary>
        /// <param name="State"></param>
        public override void Submit(ref OrderState State)
        {
            State = new OrderPending(_orderHeader);
        }
    }
}
