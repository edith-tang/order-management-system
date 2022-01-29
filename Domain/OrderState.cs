using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public enum OrderStates
    {
        New = 1,
        Pending = 2,
        Rejected = 3,
        Complete = 4
    }
    public abstract class OrderState
    {
        //field
        protected OrderHeader _orderHeader;

        //prop
        public abstract OrderStates StateEnum { get; }

        /// <summary>
        /// The OrderState constructor
        /// </summary>
        /// <param name="orderHeader"></param>
        public OrderState(OrderHeader orderHeader)
        {
            _orderHeader = orderHeader;
        }
        
        /// <summary>
        /// The abstract Complete() method
        /// </summary>
        /// <param name="State"></param>
        public abstract void Complete(ref OrderState State);

        /// <summary>
        /// The abstract Reject() method
        /// </summary>
        /// <param name="State"></param>
        public abstract void Reject(ref OrderState State);

        /// <summary>
        /// The abstract Submit() method
        /// </summary>
        /// <param name="State"></param>
        public abstract void Submit(ref OrderState State);
       
    }
}
