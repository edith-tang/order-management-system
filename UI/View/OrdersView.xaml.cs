using System;
using System.Collections.Generic;
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
    /// Interaction logic for OrdersView.xaml
    /// </summary>
    public partial class OrdersView : Page
    {
        private OrderController _oc;
        private IEnumerable<OrderHeader> _allOrderHeaders;

        /// <summary>
        /// Initialize a new OrdersView Page.
        /// </summary>
        /// <param name="oc"></param>
        public OrdersView(OrderController oc)
        {
            InitializeComponent();
            _oc = oc;
            _allOrderHeaders = oc.GetOrderHeaders();
            orderList.DataContext = _allOrderHeaders;
        }

        /// <summary>
        /// Navigates to a new AddOrderView Page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Go_To_AddOrderView(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddOrderView(_oc));
        }
       
        /// <summary>
        /// Navigates to a new OrderDetailsView Page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Go_To_OrderDetailsView(object sender, RoutedEventArgs e)
        {
            //get the order from the button clicked
            OrderHeader order = (OrderHeader)((Button)e.Source).DataContext; //is the order items being passed? --yes

            //navigate to the order details page
            NavigationService.Navigate(new OrderDetailsView(_oc, order));
        }

        /// <summary>
        /// Delete an order and refresh the OrdersView Page to reflect the change.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Delete_Order(object sender, RoutedEventArgs e)
        {
            //get the order from the button clicked
            OrderHeader order = (OrderHeader)((Button)e.Source).DataContext;

            //delete the order from database
            _oc.DeleteOrderHeaderAndOrderItems(order.Id); 

            //refresh the orders list
            NavigationService.Navigate(new OrdersView(_oc));
        }
    }
}
