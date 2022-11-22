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
    /// Логика взаимодействия для AddBookWindow.xaml
    /// </summary>
    public partial class AddBookWindow : Window, INotifyPropertyChanged
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


        DataStorage Storage = new DataStorage();
        InputChecker Checker = new();
        public AddBookWindow()
        {
            InitializeComponent();
            DataContext = this;
            Books = Storage.GetBooks();
            Publishers = Storage.GetPublishers();
            YearPicker.DisplayDateEnd = DateTime.Now;
            YearPicker.SelectedDate = DateTime.Today;
        }

        private void ChooseAuthorsButton_Click(object sender, RoutedEventArgs e)
        {
            var bookName = BookNameTextBlock.Text;
            var amount_of_pages = AmountOfPagesTextBlock.Text;
            var primeCost = PrimeCostTextBlock.Text;
            var discount = DiscountPercentTextBlock.Text;
            var amount_of_books = BookAmountTextBlock.Text;
            var IsSequel = IsEqualCheckBox.IsChecked;
            AdoNet_Exam.Storage.Publisher _selectedPublisher = SelectedPublisher;

            if (Checker.AddBookChecker(bookName, amount_of_pages, primeCost, discount, amount_of_books) && SelectedPublisher != null)
            {
                var _newBook = new Book();

                _newBook.Name = bookName;
                _newBook.AmountOfPages = Convert.ToInt32(amount_of_pages);
                _newBook.Year = (DateTime)YearPicker.SelectedDate;
                _newBook.PrimeCost = Convert.ToDecimal(primeCost);
                _newBook.DiscountPercent = Convert.ToInt32(discount);
                _newBook.AmountOfBook = Convert.ToInt32(amount_of_books);
                _newBook.IsSequel = (bool)IsSequel;

                Storage.AddNewBook(_newBook, SelectedPublisher.Id);
                ClearTextBoxes();
                this.Hide();
                var _ChooseGenresAndAuthorsWindow = new ChooseGenresAndAuthrosWindow();
                _ChooseGenresAndAuthorsWindow.BookName = bookName;
                _ChooseGenresAndAuthorsWindow.ShowDialog();
                UpdateBooks();
                
                this.ShowDialog();
            }
            else
            {
                MessageBox.Show("Choose One Publisher And Set Proper Data ", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                ClearTextBoxes();
            }


        }
    }
}
