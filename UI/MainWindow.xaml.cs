using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Controllers;
using Domain;
using UI.View;

namespace UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private OrderController _oc;

        /// <summary>
        /// Initialize a new MainWindow and navigates to a new OrdersView Page
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            _oc = new OrderController();
            myFrame.Navigate(new OrdersView(_oc));
        }
    }

}
