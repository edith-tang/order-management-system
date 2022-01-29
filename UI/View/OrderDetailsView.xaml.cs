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
    /// Interaction logic for OrderDetailsView.xaml
    /// </summary>
    public partial class OrderDetailsView : Page
    {
        private OrderController _oc;
        private OrderHeader _order, _orderUpdated;

        /// <summary>
        /// Intialize an OrderDetailsView Page.
        /// </summary>
        /// <param name="oc"></param>
        /// <param name="order"></param>
        public OrderDetailsView(OrderController oc, OrderHeader order)
        {
            InitializeComponent();
            _oc = oc;
            _order = order;
                        
            orderInfoLine.DataContext = _order;
            orderItemList.DataContext = _order.OrderItems;

            //show process button if order is pending
            if (_order.StateEnum.Equals(OrderStates.Pending))
            {
                btnProcess.Visibility = Visibility.Visible;                
            }
        }

        /// <summary>
        /// Process a pending order and refreshes the OrderDetailsView Page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Process_Order(object sender, RoutedEventArgs e)
        {
            _oc.ProcessOrder(_order.Id);

            //refresh the order details page to reflect the updated order state
            _orderUpdated = _oc.GetOrderHeader(_order.Id);
            NavigationService.Navigate(new OrderDetailsView(_oc, _orderUpdated));
        }

        /// <summary>
        /// Exit the OrderDetailsView and return to the OrdersView Page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Exit(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new OrdersView(_oc));
        }
    }
}
