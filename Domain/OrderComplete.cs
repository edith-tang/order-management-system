using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class OrderComplete: OrderState
    {
        public override OrderStates StateEnum => OrderStates.Complete;

        /// <summary>
        /// The OrderComplete constructor
        /// </summary>
        /// <param name="orderHeader"></param>
        public OrderComplete(OrderHeader orderHeader) : base(orderHeader)
        {
        }

        /// <summary>
        /// Throws an exception when the user attempts to update an already completed order.
        /// </summary>
        /// <param name="State"></param>
        public override void Complete(ref OrderState State)
        {
            throw new InvalidOrderStateException("This order has already been completed.");
        }

        /// <summary>
        /// Throws an exception when the user attempts to update an already completed order.
        /// </summary>
        /// <param name="State"></param>
        public override void Reject(ref OrderState State)
        {
            throw new InvalidOrderStateException("This order has already been completed.");
        }

        /// <summary>
        /// Throws an exception when the user attempts to submit an already completed order.
        /// </summary>
        /// <param name="State"></param>
        public override void Submit(ref OrderState State)
        {
            throw new InvalidOrderStateException("This order has already been completed.");
        }
    }
}