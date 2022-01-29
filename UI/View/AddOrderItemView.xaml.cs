using System;
using System.Collections.Generic;
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
    /// Interaction logic for AddOrderItemView.xaml
    /// </summary>
    public partial class AddOrderItemView : Page
    {
        private OrderController _oc;
        private OrderHeader _order;
        private StockItemController _sc;

        /// <summary>
        /// Initialize an AddOrderItemView Page.
        /// </summary>
        /// <param name="oc"></param>
        /// <param name="order"></param>
        public AddOrderItemView(OrderController oc, OrderHeader order)
        {
            InitializeComponent();
            _oc = oc;
            _order = order;
            _sc = new StockItemController();                      
            itemsList.DataContext = _sc.GetStockItems();
        }

        /// <summary>
        /// Exit the current page and navigates back to the AddOrderView Page for the same order.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Exit(object sender, RoutedEventArgs e)
        {
            //exit without doing anything to the database
            NavigationService.Navigate(new AddOrderView(_oc, _order));
        }

        /// <summary>
        /// Add an order item to the newly created order. Display warnings if inputs are invalid.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Add_Selection(object sender, RoutedEventArgs e)
        {
            //try and catch invalid submissions
            try
            {
                StockItem sItem = (StockItem)itemsList.SelectedItem;
                int qtyEntered = int.Parse(txtQty.Text);

                if (!(qtyEntered > 0) || (sItem == null))
                {
                    FormatException exc = new FormatException();
                    throw exc;
                }

                //display warning if qty required exceeds stock quantity
                if (qtyEntered > sItem.InStock)
                {
                    MessageBox.Show("Insufficient Stock!", "Warning", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                //upsert to database, go back to add order view
                _oc.UpsertOrderItem(_order.Id, sItem.Id, qtyEntered);
                var orderUpdated = _oc.GetOrderHeader(_order.Id);
                NavigationService.Navigate(new AddOrderView(_oc, orderUpdated));

            }
            catch (FormatException) 
            {
                MessageBox.Show("Invalid Submission!");
            }
        }
    }
}
