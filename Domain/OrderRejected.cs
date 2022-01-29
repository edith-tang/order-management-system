using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class OrderRejected: OrderState
    {
        public override OrderStates StateEnum => OrderStates.Rejected;

        /// <summary>
        /// The OrderRejected constructor
        /// </summary>
        /// <param name="orderHeader"></param>
        public OrderRejected(OrderHeader orderHeader) : base(orderHeader)
        {            
        }

        /// <summary>
        /// Throws an exception when the user attempts to update an already rejected order.
        /// </summary>
        /// <param name="State"></param>
        public override void Complete(ref OrderState State)
        {
            throw new InvalidOrderStateException("This order has already been rejected.");
        }

        /// <summary>
        /// Throws an exception when the user attempts to update an already rejected order.
        /// </summary>
        /// <param name="State"></param>
        public override void Reject(ref OrderState State)
        {
            throw new InvalidOrderStateException("This order has already been rejected.");
        }

        /// <summary>
        /// Throws an exception when the user attempts to submit an already rejected order.
        /// </summary>
        /// <param name="State"></param>
        public override void Submit(ref OrderState State)
        {
            throw new InvalidOrderStateException("This order has already been rejected.");
        }
    }
}