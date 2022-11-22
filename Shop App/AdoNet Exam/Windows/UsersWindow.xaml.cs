using AdoNet_Exam.Services;
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
    /// Логика взаимодействия для UsersWindow.xaml
    /// </summary>
    public partial class UsersWindow : Window, INotifyPropertyChanged
    {
        #region PropertyChanged Realization

        public event PropertyChangedEventHandler PropertyChanged;
        private void InvokePropChanged([CallerMemberName] string prop = default)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        #endregion

        #region Books Binding

        private ObservableCollection<Book> _books = new();
        public ObservableCollection<Book> Books
        {
            get { return _books; }
            set { _books = value; InvokePropChanged(); }
        }
        private Book _selectedBook;
        public Book SelectedBook
        {
            get { return _selectedBook; }
            set { _selectedBook = value; InvokePropChanged(); }
        }


        #endregion

        #region Current User Data

        public int UserId { get; set; }
        public string UserLogin { get; set; }

        #endregion

        public void UpdateBooks()
        {

            Books = new ObservableCollection<Book>(Storage.GetBooks());
        }

        DataStorage Storage = new DataStorage();
        InputChecker Checker = new();
        
        public UsersWindow()
        {
            InitializeComponent();
            DataContext = this;
            Books = Storage.GetBooks();
            OrderDatePicker.SelectedDate = DateTime.Today;
            OrderDatePicker.DisplayDateStart = DateTime.Now;
            ListOfBooks.ItemsSource = Books.Where(x=>x.AmountOfBook > 0).ToList();
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(ListOfBooks.ItemsSource);
            view.Filter = BookFilter;
        }

        #region Filter

        private bool BookFilter(object item)
        {

            if (String.IsNullOrEmpty(SearchingTextBox.Text))
                return true;
            else
            {

                return
                ((item as Book).Name.StartsWith(SearchingTextBox.Text, StringComparison.OrdinalIgnoreCase)) ||
                ((item as Book).BookGenre.StartsWith(SearchingTextBox.Text, StringComparison.OrdinalIgnoreCase)) ||
                ((item as Book).BookAuthor.StartsWith(SearchingTextBox.Text, StringComparison.OrdinalIgnoreCase));
                


            }


        }

        private void txtFilter_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

            CollectionViewSource.GetDefaultView(ListOfBooks.ItemsSource).Refresh();


        }

        #endregion

        #region Window Buttons

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            var res = MessageBox.Show("Are you sure you want to quit", "Library Shop", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (res == MessageBoxResult.Yes)  this.Close(); 
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        #endregion
        private void ListOfBooks_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (SelectedBook != null) { MessageBox.Show(SelectedBook.Name.ToString()); }
        }

        private void MakeAnOrder_Click(object sender, RoutedEventArgs e)
        {
            if (Checker.AddOrderChecker(SelectedBook))
            {
                Storage.AddNewOrder(SelectedBook, UserId, (DateTime)OrderDatePicker.SelectedDate);
                MessageBox.Show("Order has been added", "Order Add", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else 
            {
                MessageBox.Show("Input Propper Date And Select A Book", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        
        private void MyOrdersButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            var currentUserOrders = new CurrentUserOrders(UserId);
            currentUserOrders.UserLogin = UserLogin;
            currentUserOrders.UserId = UserId;
            currentUserOrders.ShowDialog();
            UpdateBooks();
            this.ShowDialog();
        }
    }
}
