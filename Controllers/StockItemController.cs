using System;
using System.Collections.Generic;
using DataAccess;
using Domain;

namespace Controllers
{
    public class StockItemController
    {
        public StockRepo StockRepo;

        /// <summary>
        /// The StockItemController constructor
        /// </summary>
        public StockItemController()
        {
            StockRepo = new StockRepo();
        }

        /// <summary>
        /// The method to get all stock items from the database.
        /// </summary>
        /// <returns>A collection of all stock items.</returns>
        public IEnumerable<StockItem> GetStockItems()
        {
            return StockRepo.GetStockItems();
        }

    }
}
