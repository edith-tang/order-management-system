using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Domain;

namespace DataAccess
{
    public class StockRepo
    {
        string _connectionString = @"Data Source=localhost\SQLEXPRESS;Initial Catalog=OrderManagementDbTestData;Integrated Security=True";

        /// <summary>
        /// The method to get a collection of stock items from the database.
        /// </summary>
        /// <returns>A collection of stock items.</returns>
        public IEnumerable<StockItem> GetStockItems()
        {
            List<StockItem> allStocks = new List<StockItem>();

            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            SqlCommand command = new SqlCommand("dbo.sp_SelectStockItems", connection);
            command.CommandType = CommandType.StoredProcedure;
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                int id = (int)reader[0];
                string name = (string)reader[1];
                decimal price = (decimal)reader[2];
                int inStock = (int)reader[3];
                StockItem sItem = new StockItem(id, name, price, inStock);
                allStocks.Add(sItem);
            }
            reader.Close();
            connection.Close();
            return allStocks;
        }

        /// <summary>
        /// The method to get a specific stock item based on its id from the database.
        /// </summary>
        /// <param name="sItemId"></param>
        /// <returns>A specific stock item.</returns>
        public StockItem GetStockItemById(int sItemId)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            SqlCommand command = new SqlCommand("sp_SelectStockItemById", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@id", sItemId));
            SqlDataReader reader = command.ExecuteReader();
            reader.Read();
            StockItem sItem = new StockItem((int)reader[0], (string)reader[1], (decimal)reader[2], (int)reader[3]);
            command.Parameters.Clear();
            reader.Close();
            connection.Close();
            return sItem;
        }

        /// <summary>
        /// The method to update(reduce) the quantity in stock of a stock item.
        /// </summary>
        /// <param name="order"></param>
        public void UpdateStockItemAmount(OrderHeader order)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            SqlCommand command = new SqlCommand("sp_UpdateStockItemAmount", connection);
            command.CommandType = CommandType.StoredProcedure;
            foreach (OrderItem oItem in order.OrderItems)
            {
                command.Parameters.Add(new SqlParameter("@id", oItem.StockItemId));
                command.Parameters.Add(new SqlParameter("@amount", -oItem.Quantity));
                command.ExecuteNonQuery();
                command.Parameters.Clear();
            }
            connection.Close();
        }

    }
}
