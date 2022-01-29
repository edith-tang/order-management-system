using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Controllers;
using Domain;

namespace UI.View
{
    /// <summary>
    /// Interaction logic for AddOrderView.xaml
    /// </summary>
    public partial class AddOrderView : Page
    {
        private OrderController _oc;
        private OrderHeader _order;
        private OrderHeader _orderUpdated;

        /// <summary>
        /// Initialize an AddOrderView Page.
        /// </summary>
        /// <param name="oc"></param>
        public AddOrderView(OrderController oc)
        {
            InitializeComponent();
            _oc = oc;
            _order = null;
        }

        /// <summary>
        /// Press add order button to create a new order.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Add_Order(object sender, RoutedEventArgs e)
        {
            _order = _oc.CreateNewOrderHeader();
            //display order info and hide the add order button
            addedOrder.DataContext = _order;
            btnAddItem.Visibility = Visibility.Visible;
            btnAdd.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Navigates to the AddOrderItemView Page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Go_To_AddOrderItemView(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddOrderItemView(_oc, _order));
        }

        /// <summary>
        /// Navigates back from the AddOrderItemView Page.
        /// </summary>
        /// <param name="oc"></param>
        /// <param name="order"></param>
        public AddOrderView(OrderController oc, OrderHeader order)
        {
            InitializeComponent();
            btnAdd.Visibility = Visibility.Hidden;
            _oc = oc;
            _order = order;
            addedOrder.DataContext = _order;
            if (_order.TotalItems > 0)
                selectionList.DataContext = _order.OrderItems;
            else
                selectionList.DataContext = null;
            btnAddItem.Visibility = Visibility.Visible;
            btnSubmit.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Delete an order item from the newly created order.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Delete_Selection(object sender, RoutedEventArgs e)
        {
            OrderItem oI = (OrderItem)((Button)e.Source).DataContext;
            _oc.DeleteOrderItem(_order.Id, oI.StockItemId);
            _orderUpdated = _oc.GetOrderHeader(_order.Id);

            addedOrder.DataContext = null;
            addedOrder.DataContext = _orderUpdated;

            selectionList.DataContext = null;
            //if at least one order item remains
            if (_orderUpdated.TotalItems > 0)
            {
                selectionList.DataContext = _orderUpdated.OrderItems;
            }
        }

        /// <summary>
        /// Submit the newly created order and navigates to an updated OrdersView Page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Submit_Order(object sender, RoutedEventArgs e)
        {
            try
            {
                //submission not allowed if no order item is selected
                if (selectionList.DataContext == null)
                {
                    FormatException exc = new FormatException();
                    throw exc;
                }

                //submit the order
                _oc.SubmitOrder(_order.Id);

                //navigate to a new order list page
                NavigationService.Navigate(new OrdersView(_oc));
            }
            catch (FormatException)
            {
                MessageBox.Show("Can't submit an empty order.");
            }
        }

        /// <summary>
        /// Exit the current page, delete unsaved order information, and navigates back to the OrdersView Page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Exit(object sender, RoutedEventArgs e) 
        {
            if (_order == null) //no order created, exit directly
            {
                NavigationService.Navigate(new OrdersView(_oc));
            }
            else //delete the created order
            {
                _oc.DeleteOrderHeaderAndOrderItems(_order.Id);
                NavigationService.Navigate(new OrdersView(_oc));

                //problem: if the user closes the window prior to pressing exit or submit, the newly created order stays in the db 
            }
        }

    }
}
