using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Restaurant.Helps;
using Restaurant.Models.BusinessLogicLayer;
using Restaurant.Models.Entity;
using Restaurant.Helps;
using System.Windows;

namespace Restaurant.ViewModels
{
    public class UserOrdersViewModel : NotifyPropertyChangedHelp
    {
        public UserOrdersViewModel()
        {
            OrderBLL order = new OrderBLL();
            ActiveOrders = new ObservableCollection<OrdersDisplay>(order.GetActiveOrders());
            AllOrders = new ObservableCollection<OrdersDisplay>(order.GetAllOrders());
        }

        #region DataMembers
        private ObservableCollection<OrdersDisplay> activeOrders;
        public ObservableCollection<OrdersDisplay> ActiveOrders
        {
            get
            {
                return activeOrders;
            }
            set
            {
                activeOrders = value;
                NotifyPropertyChanged("ActiveOrders");
            }
        }

        private ObservableCollection<OrdersDisplay> allOrders;
        public ObservableCollection<OrdersDisplay> AllOrders
        {
            get
            {
                return allOrders;
            }
            set
            {
                allOrders = value;
                NotifyPropertyChanged("AllOrders");
            }
        }

        private OrdersDisplay selectedActiveOrder;
        public OrdersDisplay SelectedActiveOrder
        {
            get
            {
                return selectedActiveOrder;
            }
            set
            {
                selectedActiveOrder = value;
                NotifyPropertyChanged("SelectedActiveOrder");
            }
        }

        private OrdersDisplay selecteAllOrder;
        public OrdersDisplay SelectedAllOrder
        {
            get
            {
                return selecteAllOrder;
            }
            set
            {
                selecteAllOrder = value;
                NotifyPropertyChanged("SelectedAllOrder");
            }
        }
        #endregion


        #region CommandMembers
        private bool CanExecuteCommand { get; set; } = false;
        private ICommand cancelCommand;
        public ICommand CancelCommand
        {
            get
            {
                if(cancelCommand == null)
                {
                    cancelCommand = new RelayCommand(CancelOrder, param => CanExecuteCommand);
                }
                return cancelCommand;
            }
        }

        public void CancelOrder(object param)
        {
            if (SelectedActiveOrder == null)
            {
                MessageBox.Show("Select an order to cancel it!");
            }
            else
            {
                if(MessageBox.Show("Are you sure you want to cancel this order?", "Cancel order", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    MessageBox.Show("Order canceled!");
                }
            }
        }
        #endregion
    }
}
