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
    /// Логика взаимодействия для ViewStocksWindow.xaml
    /// </summary>
    public partial class ViewStocksWindow : Window, INotifyPropertyChanged
    {
        #region PropertyChanged Realization

        public event PropertyChangedEventHandler PropertyChanged;
        private void InvokePropChanged([CallerMemberName] string prop = default)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        #endregion

        #region Stocks Binding
        private ObservableCollection<Stock> _stocks = new();
        public ObservableCollection<Stock> Stocks
        {
            get { return _stocks; }
            set { _stocks = value; InvokePropChanged(); }
        }

        private Stock _selectedStock;

        public Stock SelectedStock
        {
            get { return _selectedStock; }
            set { _selectedStock = value; }
        }


        DataStorage Storage = new();


        #endregion

        public void UpdateStock() 
        {
            Stocks = new ObservableCollection<Stock>(Storage.GetStocks());
        }
        public ViewStocksWindow()
        {
            InitializeComponent();
            DataContext = this;
            Stocks = Storage.GetStocks();
        }

        private void DeleteGenre_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedStock != null)
            {
                var result = MessageBox.Show($"Are you sure you want to delete this data?", "Stocks Deleting", MessageBoxButton.YesNo, MessageBoxImage.Information);
                if (result == MessageBoxResult.Yes)
                {
                    MessageBox.Show($"Staock has been deleted");
                    Storage.RemoveStock(SelectedStock);
                    UpdateStock();
                }

            }
        }

        private void AddNewStockButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            var _addStockWindow = new AddNewStockWindow();
            _addStockWindow.ShowDialog();
            UpdateStock();
            this.ShowDialog();
        }

        private void UpdateStockButoon_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
