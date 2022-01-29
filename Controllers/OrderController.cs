using System.Collections.Generic;
using System.Data.SqlClient;
using DataAccess;
using Domain;

namespace Controllers
{
    public class OrderController
    {
        private OrderRepo _orderRepository;
        private StockRepo _stockItemRepository;

        /// <summary>
        /// The OrderController constructor
        /// </summary>
        public OrderController()
        {
            _orderRepository = new OrderRepo();
            _stockItemRepository = new StockRepo();
        }

        /// <summary>
        /// Call the GetOrderHeaders() method in the OrderRepo Class.
        /// </summary>
        /// <returns>A collection of all exisitng orders.</returns>
        public IEnumerable<OrderHeader> GetOrderHeaders()
        {
            return _orderRepository.GetOrderHeaders();
        }

        /// <summary>
        /// Call the GetOrderHeader() method in the OrderRepo Class.
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns>A specific order.</returns>
        public OrderHeader GetOrderHeader(int orderId)
        {
            return _orderRepository.GetOrderHeader(orderId);
        }

        /// <summary>
        /// Call the InsertOrderHeader() method in the OrderRepo Class.
        /// </summary>
        /// <returns>The order created.</returns>
        public OrderHeader CreateNewOrderHeader()
        {
            return _orderRepository.InsertOrderHeader();
        }

        /// <summary>
        /// Insert a new order item or update an exisiting order item for an order, and call the UpsertOrderItem() method
        /// in the OrderRepo Class to update the order in the database.
        /// </summary>
        /// <param name="oItem"></param>
        /// <returns>The order with updated order items.</returns>
        public OrderHeader UpsertOrderItem(int orderHeaderId, int stockItemId, int quantity)
        {
            //create a variable "order" to store the order queried from the database
            OrderHeader order = _orderRepository.GetOrderHeader(orderHeaderId);

            //create a variable "stockItem" to store the stock item queried from the database
            StockItem sItem = _stockItemRepository.GetStockItemById(stockItemId);

            //based on the order info and stock item info queried from the database, create/update the corresponding order item for "order"
            OrderItem oItem = order.AddOrUpdateOrderItem(sItem.Id, sItem.Name, sItem.Price, quantity);

            //pass this order item to the database to update "order" in database
            _orderRepository.UpsertOrderItem(oItem);

            //return the updated "order"
            return order;
            //return _orderRepository.GetOrderHeader(orderHeaderId);
        }

        /// <summary>
        /// The method to submit a new order.
        /// </summary>
        /// <param name="order"></param>
        /// <returns>The submitted order.</returns>
        public OrderHeader SubmitOrder(int orderHeaderId)
        {
            OrderHeader order = _orderRepository.GetOrderHeader(orderHeaderId);
            order.Submit();
            _orderRepository.UpdateOrderState(order);
            return order;
            //return _orderRepository.GetOrderHeader(orderHeaderId);
        }

        /// <summary>
        /// The method to process a pending order.  Reject the order if there is insufficient quantity in stock.
        /// </summary>
        /// <param name="orderHeaderId"></param>
        /// <returns>The processed order.</returns>
        public OrderHeader ProcessOrder(int orderHeaderId)
        {
            OrderHeader order = _orderRepository.GetOrderHeader(orderHeaderId);
            try
            {
                try
                {
                    _stockItemRepository.UpdateStockItemAmount(order);
                    order.Complete();
                }
                catch (SqlException ex)
                {
                    //Check Constraint Violation
                    if (ex.Number == 547)
                    {
                        order.Reject();
                    }
                }
                _orderRepository.UpdateOrderState(order);
            }
            catch (InvalidOrderStateException ex)
            {
                throw ex;
            }
            return order;
        }

        /// <summary>
        ///  Call the DeleteOrderItem() method in the OrderRepo Class.
        /// </summary>
        /// <param name="orderHeaderId"></param>
        /// <param name="stockItemId"></param>
        public void DeleteOrderItem(int orderHeaderId, int stockItemId)
        {
            _orderRepository.DeleteOrderItem(orderHeaderId, stockItemId);
        }

        /// <summary>
        /// Call the DeleteOrderHeaderAndOrderItems() method in the OrderRepo Class.
        /// </summary>
        /// <param name="orderHeaderId"></param>
        public void DeleteOrderHeaderAndOrderItems(int orderHeaderId)
        {
            _orderRepository.DeleteOrderHeaderAndOrderItems(orderHeaderId);
        }

        /// <summary>
        /// Call the ResetDatabase() method in the OrderRepo Class.
        /// </summary>
        public void ResetDatabase()
        {
            _orderRepository.ResetDatabase();
        }

    }
}
