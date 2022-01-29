using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Domain;

namespace DataAccess
{
    public class OrderRepo
    {
		string _connectionString = @"Data Source=localhost\SQLEXPRESS;Initial Catalog=OrderManagementDbTestData;Integrated Security=True";

        /// <summary>
        /// The method to get a collection of orders from the database.
        /// </summary>
        /// <returns>A collection of orders.</returns>
        public IEnumerable<OrderHeader> GetOrderHeaders()
        {
            //get all existing order headers - execute sp_SelectOrderHeaders
            SqlConnection connection = new SqlConnection(_connectionString);
			connection.Open();
			SqlCommand command = new SqlCommand("sp_SelectOrderHeaders", connection);
			command.CommandType = CommandType.StoredProcedure;
			SqlDataReader reader = command.ExecuteReader();

			List<OrderHeader> allOrderHeaders = new List<OrderHeader>();
			OrderHeader order = null;
			int orderId = -1;

			while (reader.Read())
			{
				if (orderId == -1 || (orderId != -1 && orderId != (int)reader[0]))
				{
					orderId = (int)reader[0];
					int stateId = (int)reader[1];
					DateTime dateTime = (DateTime)reader[2];
					order = new OrderHeader(orderId, dateTime, stateId);
					allOrderHeaders.Add(order);
                }
				if (!reader.IsDBNull(3))
				{
					int stockItemId = (int)reader[3];
					string itemDesc = (string)reader[4];
					decimal itemPrice = (decimal)reader[5];
					int itemQty = (int)reader[6];
					OrderItem oItem = new OrderItem(order, stockItemId, itemDesc, itemPrice, itemQty);
					order.OrderItems.Add(oItem);
				}
					
			}
			reader.Close();
            connection.Close();
			return allOrderHeaders;
		}

        /// <summary>
        /// The method to get a specific order by its id from the database.
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns>A specific order.</returns>
        public OrderHeader GetOrderHeader(int orderId)
        {
            //get an existing order header by id - execute sp_SelectOrderHeaderById
            //select an order and go to the order details page 
            SqlConnection connection = new SqlConnection(_connectionString);
			connection.Open();
			SqlCommand command = new SqlCommand("sp_SelectOrderHeaderById", connection);
			command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@id", orderId));
			SqlDataReader reader = command.ExecuteReader();
			OrderHeader order = null;
            int count = 0;
			while (reader.Read())
			{
                count++;
				if (count == 1)
				{
					int orderStateId = (int)reader[1];
					DateTime dateTime = (DateTime)reader[2];
                    order = new OrderHeader(orderId, dateTime, orderStateId);
				}
				if (!reader.IsDBNull(3))
				{
					int stockItemId = (int)reader[3];
					string itemDesc = (string)reader[4];
					decimal itemPrice = (decimal)reader[5];
					int itemQty = (int)reader[6];
					OrderItem oItem = new OrderItem(order, stockItemId, itemDesc, itemPrice, itemQty);
					order.OrderItems.Add(oItem);
                }
            }
			reader.Close();
            connection.Close();
			return order;
        }

        /// <summary>
        /// The method to create a new order in the database.
        /// </summary>
        /// <returns>The order created.</returns>
        public OrderHeader InsertOrderHeader()
        {
            //step 1: execute sp_InsertOrderHeader and get the generated id
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            SqlCommand command = new SqlCommand("sp_InsertOrderHeader", connection);
            command.CommandType = CommandType.StoredProcedure;
			SqlDataReader reader = command.ExecuteReader();
			reader.Read();
            int orderId = decimal.ToInt32((decimal)reader[0]);
            //step 2: call GetOrderHeader method and get the order header details
            return GetOrderHeader(orderId);
		}

        /// <summary>
        /// The method to insert a new order item or update an exisiting order item for an order in the database.
        /// </summary>
        /// <param name="oItem"></param>
        public void UpsertOrderItem(OrderItem oItem)
        {
            //insert or update an order item - execute sp_UpsertOrderItem
            SqlConnection connection = new SqlConnection(_connectionString);
			connection.Open();
			SqlCommand command = new SqlCommand("sp_UpsertOrderItem", connection);
			command.CommandType = CommandType.StoredProcedure;

			command.Parameters.Add(new SqlParameter("@orderHeaderId", oItem.OrderHeaderId));
            command.Parameters.Add(new SqlParameter("@stockItemId", oItem.StockItemId));
			command.Parameters.Add(new SqlParameter("@description", oItem.Description));
			command.Parameters.Add(new SqlParameter("@price", oItem.Price));
			command.Parameters.Add(new SqlParameter("@quantity", oItem.Quantity));

			command.ExecuteNonQuery();

			command.Parameters.Clear();
			connection.Close();
		}

        /// <summary>
        /// The method to update the state of an order.
        /// </summary>
        /// <param name="order"></param>
        public void UpdateOrderState(OrderHeader order)
        {
			//update the state of an existing order header - execute sp_UpdateOrderState
			SqlConnection connection = new SqlConnection(_connectionString);
			connection.Open();
			SqlCommand command = new SqlCommand("sp_UpdateOrderState", connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@orderHeaderId", order.Id));
			command.Parameters.Add(new SqlParameter("@stateId", (int)order.StateEnum));
			command.ExecuteNonQuery();
			command.Parameters.Clear();
            connection.Close();
		}

        /// <summary>
        /// The method to delete a specific order item from an order in the database.
        /// </summary>
        /// <param name="orderHeaderId"></param>
        /// <param name="stockItemId"></param>
        public void DeleteOrderItem(int orderHeaderId, int stockItemId)
        {
			//delete an existing order item - execute sp_DeleteOrderItem
			SqlConnection connection = new SqlConnection(_connectionString);
			connection.Open();
			SqlCommand command = new SqlCommand("sp_DeleteOrderItem", connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@orderHeaderId", orderHeaderId));
            command.Parameters.Add(new SqlParameter("@stockItemId", stockItemId));

			command.ExecuteNonQuery();

			command.Parameters.Clear();
            connection.Close();
		}

        /// <summary>
        /// The method to delete an order and all its order items from the database.
        /// </summary>
        /// <param name="orderHeaderId"></param>
        public void DeleteOrderHeaderAndOrderItems(int orderHeaderId)
		{
			//delete an existing order header and its items - execute sp_DeleteOrderHeaderAndOrderItems
			SqlConnection connection = new SqlConnection(_connectionString);
			connection.Open();
			SqlCommand command = new SqlCommand("sp_DeleteOrderHeaderAndOrderItems", connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@orderHeaderId", orderHeaderId));

			command.ExecuteNonQuery();

			command.Parameters.Clear();
			connection.Close();
        }

        /// <summary>
        /// The method to reset the database (delete all orders and reset all in stock quantity to 10).
        /// </summary>
        public void ResetDatabase()
        {
			SqlConnection connection = new SqlConnection(_connectionString);
			connection.Open();
			string queryString = "DELETE FROM OrderItems; DELETE FROM OrderHeaders; UPDATE StockItems SET InStock = 10; DBCC CHECKIDENT('OrderHeaders', RESEED, 0);";
			SqlCommand command = new SqlCommand(queryString, connection);
			command.ExecuteNonQuery();
			connection.Close();
		}
	}
}
