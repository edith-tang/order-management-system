using System;

namespace Domain
{
    public class StockItem
    {
        public int Id {get; private set;}
        
        public string Name { get; private set; }
      
        public decimal Price { get; private set; }       

        public int InStock { get; private set; }

        /// <summary>
        /// The StockItem constructor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="price"></param>
        /// <param name="inStock"></param>
        public StockItem(int id, string name, decimal price, int inStock)
        {
            Id = id;
            Name = name;
            Price = price;
            InStock = inStock;
        }
    }
}
