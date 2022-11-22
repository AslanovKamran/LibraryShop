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
    /// Логика взаимодействия для UpdateBookWindow.xaml
    /// </summary>
    public partial class UpdateBookWindow : Window, INotifyPropertyChanged
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
        private Book _selectedBook = new Book();
        public Book SelectedBook
        {
            get { return _selectedBook; }
            set { _selectedBook = value; InvokePropChanged(); }
        }


        #endregion

        #region Publishers Binding
        private ObservableCollection<Publisher> publishers = new();

        public ObservableCollection<Publisher> Publishers
        {
            get { return publishers; }
            set { publishers = value; InvokePropChanged(); }
        }

        private Publisher _selectedPublisher;
        public Publisher SelectedPublisher
        {
            get { return _selectedPublisher; }
            set { _selectedPublisher = value; InvokePropChanged(); }
        }

        #endregion

        DataStorage Storage = new DataStorage();
        InputChecker Checker = new();
        public void UpdateBooks()
        {

            Books = new ObservableCollection<Book>(Storage.GetBooks());
        }

        public void ClearTextBoxes()
        {
            YearPicker.SelectedDate = DateTime.Today;
            IsEqualCheckBox.IsChecked = false;
            BookNameTextBlock.Clear();
            AmountOfPagesTextBlock.Clear();
            PrimeCostTextBlock.Clear();
            DiscountPercentTextBlock.Clear();
            BookAmountTextBlock.Clear();
        }
        public UpdateBookWindow()
        {
            InitializeComponent();
            DataContext = this;
            Books = Storage.GetBooks();
            Publishers = Storage.GetPublishers();
            YearPicker.DisplayDateEnd = DateTime.Now;
            YearPicker.SelectedDate = DateTime.Today;
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            var bookName = BookNameTextBlock.Text;
            var amount_of_pages = AmountOfPagesTextBlock.Text;
            var primeCost = PrimeCostTextBlock.Text;
            var discount = DiscountPercentTextBlock.Text;
            var amount_of_books = BookAmountTextBlock.Text;
            var IsSequel = IsEqualCheckBox.IsChecked;
            var _year = (DateTime)YearPicker.SelectedDate;

            AdoNet_Exam.Storage.Publisher _selectedPublisher = SelectedPublisher;
            if (SelectedBook != null && SelectedPublisher != null && Checker.AddBookChecker(bookName, amount_of_pages, primeCost, discount, amount_of_books) && SelectedPublisher != null)
            {
                Storage.UpdateBooks(SelectedBook, bookName, SelectedPublisher.Id, Convert.ToInt32(amount_of_pages), _year, Convert.ToDecimal(primeCost), Convert.ToInt32(discount), Convert.ToInt32(amount_of_books), Convert.ToBoolean(IsSequel));
                UpdateBooks();
                MessageBox.Show("Updated");
            }
            else { MessageBox.Show("Error"); }
        }
    }
}
