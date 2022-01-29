using NUnit.Framework;
using Controllers;
using DataAccess;
using Domain;
using System.Linq;

namespace TestProject
{
    public class Tests
    {
        public OrderController oc;
        public StockItemController sc;

        [SetUp]
        public void Setup()
        {
            oc = new OrderController();
            sc = new StockItemController();
            oc.ResetDatabase();
        }

        [Test]
        public void TestGetStockItems()
        {
            var stockItems = sc.GetStockItems().ToArray();
            for (int i = 0; i < stockItems.Length; i++)
            {
                Assert.AreEqual(i + 1, stockItems[i].Id);
                Assert.AreEqual(10, stockItems[i].InStock);
            }
        }

        [Test]
        public void TestCreateOrder()
        {
            var order = oc.CreateNewOrderHeader();
            Assert.IsNotNull(order);
            Assert.IsNotNull(order.Id);
            Assert.AreEqual(OrderStates.New, order.StateEnum);
            Assert.IsNotNull(order.DateTime);
        }

        [Test]
        public void TestGetOrder()
        {
            var order = oc.CreateNewOrderHeader();
            var order1 = oc.GetOrderHeader(order.Id);
            Assert.IsTrue(order.Id==order1.Id);
            Assert.IsTrue(order.DateTime==order1.DateTime);
            Assert.IsTrue(order.StateEnum==order1.StateEnum);
            Assert.IsTrue(order.TotalItems==order1.TotalItems);
            Assert.IsTrue(order.Total==order1.Total);
        }

        [Test]
        public void TestInsertNewOrderItem()
        {
            var order = oc.CreateNewOrderHeader();
            for (int i = 0; i < 9; i++)
            {
                oc.UpsertOrderItem(order.Id, i + 1, 1);
            }
            var order1 = oc.GetOrderHeader(order.Id);
            var itemArray = order1.OrderItems.ToArray();
            for (int i = 0; i < 9; i++)
            {
                Assert.AreEqual(order.Id, itemArray[i].OrderHeaderId);
                Assert.AreEqual(i + 1, itemArray[i].StockItemId);
                Assert.AreEqual(1, itemArray[i].Quantity);
            }
        }

        [Test]
        public void TestUpdateExistingOrderItem()
        {
            var order = oc.CreateNewOrderHeader();
            for (int i = 0; i < 9; i++)
            {
                oc.UpsertOrderItem(order.Id, i + 1, 1);
            }
            for (int i = 0; i < 9; i++)
            {
                oc.UpsertOrderItem(order.Id, i + 1, 3);
            }
            var order1 = oc.GetOrderHeader(order.Id);
            var itemArray = order1.OrderItems.ToArray();
            for (int i = 0; i < 9; i++)
            {
                Assert.AreEqual(order.Id, itemArray[i].OrderHeaderId);
                Assert.AreEqual(i + 1, itemArray[i].StockItemId);
                Assert.AreEqual(3, itemArray[i].Quantity);
            }
        }

        [Test]
        public void TestGetOrders()
        {
            OrderHeader[] orders = new OrderHeader[5];
            for (int i = 0; i < 5; i++)
            {
                orders[i] = oc.CreateNewOrderHeader();
                oc.UpsertOrderItem(orders[i].Id, 1, 1);
            }

            var orders1 = oc.GetOrderHeaders().ToArray();
            for (int i = 0; i < 5; i++)
            {
                Assert.IsTrue(orders[i].Id == orders1[i].Id);
                Assert.IsTrue(orders[i].DateTime == orders1[i].DateTime);
                Assert.IsTrue(orders[i].StateEnum == orders1[i].StateEnum);
            }
        }

        [Test]
        public void TestSubmitNewOrder()
        {
            var order = oc.CreateNewOrderHeader();
            oc.UpsertOrderItem(order.Id, 1, 1);
            oc.SubmitOrder(order.Id);
            var order1 = oc.GetOrderHeader(order.Id);
            Assert.AreEqual(OrderStates.Pending, order1.StateEnum);
        }

        [Test]
        public void TestProcessPendingOrder1()
        {
            var order = oc.CreateNewOrderHeader();
            oc.UpsertOrderItem(order.Id, 1, 1);
            oc.SubmitOrder(order.Id);            
            oc.ProcessOrder(order.Id);
            var order1 = oc.GetOrderHeader(order.Id);
            var sItem = sc.StockRepo.GetStockItemById(1);
            Assert.AreEqual(OrderStates.Complete, order1.StateEnum);
            Assert.AreEqual(9, sItem.InStock);
        }

        [Test]
        public void TestProcessPendingOrder2()
        {
            var order = oc.CreateNewOrderHeader();
            oc.UpsertOrderItem(order.Id, 1, 100);
            oc.SubmitOrder(order.Id);
            oc.ProcessOrder(order.Id);
            var order1 = oc.GetOrderHeader(order.Id);
            var sItem = sc.StockRepo.GetStockItemById(1);
            Assert.AreEqual(OrderStates.Rejected, order1.StateEnum);
            Assert.AreEqual(10, sItem.InStock);
        }

        [Test]
        public void TestDeleteOrderItem()
        {
            var order = oc.CreateNewOrderHeader();
            oc.UpsertOrderItem(order.Id, 1, 1);
            oc.DeleteOrderItem(order.Id, 1);
            var order1 = oc.GetOrderHeader(order.Id);
            Assert.AreEqual(0, order1.OrderItems.Count);
        }

        [Test]
        public void TestDeleteOrder()
        {
            var order = oc.CreateNewOrderHeader();
            oc.UpsertOrderItem(order.Id, 1, 1);
            oc.DeleteOrderHeaderAndOrderItems(order.Id);
            var orders = oc.GetOrderHeaders();
            Assert.AreEqual(0, orders.Count());
        }


    }
}