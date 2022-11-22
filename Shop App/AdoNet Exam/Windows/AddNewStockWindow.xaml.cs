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
    /// Логика взаимодействия для AddNewStockWindow.xaml
    /// </summary>
    public partial class AddNewStockWindow : Window, INotifyPropertyChanged
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

        #region Books Binding

        private ObservableCollection<Book> _books = new();
        public ObservableCollection<Book> Books
        {
            get { return _books; }
            set { _books = value; InvokePropChanged(); }
        }

        private Book _selectedBook = new Book();
        public Book SelectedBook
        {
            get { return _selectedBook; }
            set { _selectedBook = value; InvokePropChanged(); }
        }

        #endregion

        public void UpdateStocks()
        {
            Stocks = new ObservableCollection<Stock>(Storage.GetStocks());
        }

        public void UpdateBooks() 
        {
            Books = new ObservableCollection<Book>(Storage.GetBooks());    
        }
        public AddNewStockWindow()
        {
            InitializeComponent(); 
            DataContext = this;
            Stocks = Storage.GetStocks();
            Books = Storage.GetBooks();
            IntegerUpDown.Minimum = 0;
            IntegerUpDown.Maximum = 100;
            IntegerUpDown.Value = 10;
        }

        private void AddStockButton_Click(object sender, RoutedEventArgs e)
        {


            if (SelectedBook != null)
            {
                var newStock = new Stock();
                newStock.BookId = SelectedBook.Id;
                Storage.AddNewStock(newStock, SelectedBook, Convert.ToInt32(IntegerUpDown.Value));
                UpdateStocks();
                MessageBox.Show("Stock has been updated");
                UpdateBooks();
            }
            else { MessageBox.Show("Select a book and input the a propper discount", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
        }
    }
}
