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
    /// Логика взаимодействия для ViewOrdersWindow.xaml
    /// </summary>
    public partial class ViewOrdersWindow : Window, INotifyPropertyChanged
    {
        #region PropertyChanged Realization

        public event PropertyChangedEventHandler PropertyChanged;
        private void InvokePropChanged([CallerMemberName] string prop = default)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        #endregion

        private ObservableCollection<Order> _orders = new();

        public ObservableCollection<Order> Orders
        {
            get { return _orders; }
            set { _orders = value; }
        }

      
        DataStorage Storage = new();


        public ViewOrdersWindow()
        {
            InitializeComponent();
            DataContext = this;
            Orders = Storage.GetOrders();
        }
    }
}
