using AdoNet_Exam.Storage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AdoNet_Exam.Windows
{
    /// <summary>
    /// Логика взаимодействия для CurrentUserOrders.xaml
    /// </summary>
    public partial class CurrentUserOrders : Window, INotifyPropertyChanged
    {
        #region PropertyChanged Realization

        public event PropertyChangedEventHandler PropertyChanged;
        private void InvokePropChanged([CallerMemberName] string prop = default)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        #endregion

        #region Books Binding

        private ObservableCollection<Order> _orders= new();
        public ObservableCollection<Order> Orders
        {
            get { return _orders; }
            set { _orders = value; InvokePropChanged(); }
        }
        private Order _selectedOrder;
        public Order SelectedOrder
        {
            get { return _selectedOrder; }
            set { _selectedOrder = value; InvokePropChanged(); }
        }


        #endregion

        #region Current User Data
        public int UserId { get; set; }
        public string UserLogin { get; set; }

        #endregion

        

        
        DataStorage Storage = new DataStorage();
        public CurrentUserOrders(int Id)
        {
            int CurrentUserId = Id;
            InitializeComponent();
            DataContext = this;
            Orders = Storage.GetOrdersOfCurrentUser(CurrentUserId);
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void ListOfBooks_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (SelectedOrder != null) { MessageBox.Show(SelectedOrder.BookName); }
            else { MessageBox.Show("No selection"); } 
        }

        private void DeleteBook_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedOrder != null) 
            {
                var res = MessageBox.Show("Are you sure you want to delete this book from your order", "Delete Order", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (res == MessageBoxResult.Yes) 
                {
                    Storage.RemoveOrder(SelectedOrder);
                    Orders.Remove(SelectedOrder);
                    MessageBox.Show("Deleted");
                }
            }
            else { MessageBox.Show("No Selection"); }
        }
    }
}
