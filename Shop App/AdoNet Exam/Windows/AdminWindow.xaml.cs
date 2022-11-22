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
    /// Логика взаимодействия для AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window, INotifyPropertyChanged
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

        private Publisher _selectedPublisher = new();
        public Publisher SelectedPublisher
        {
            get { return _selectedPublisher; }
            set { _selectedPublisher = value; InvokePropChanged(); }
        }

        #endregion

        #region Authors Binding
        private ObservableCollection<Author> _authors = new();

        public ObservableCollection<Author> Authors
        {
            get { return _authors; }
            set { _authors = value; InvokePropChanged(); }
        }

        private Author _selectedAuthor = new();
        public Author SelectedAuthor
        {
            get { return _selectedAuthor; }
            set { _selectedAuthor = value; InvokePropChanged(); }
        }
        #endregion

        #region Genres Binding
        private ObservableCollection<Genre> _genres = new();

        public ObservableCollection<Genre> Genres
        {
            get { return _genres; }
            set { _genres = value; InvokePropChanged(); }
        }

        private Genre _selectedGenre = new();
        public Genre SelectedGenre
        {
            get { return _selectedGenre; }
            set { _selectedGenre = value; InvokePropChanged(); }
        }
        #endregion

        #region Update Data
        public void UpdateBooks()
        {

            Books = new ObservableCollection<Book>(Storage.GetBooks());
        }

        public void UpdateAuthors()
        {
            Authors = new ObservableCollection<Author>(Storage.GetAuthors());
        }

        public void UpdatePublishers()
        {
            Publishers = new ObservableCollection<Publisher>(Storage.GetPublishers());
        }

        public void UpdateGenres()
        {
            Genres = new ObservableCollection<Genre>(Storage.GetGenres());
        }
        #endregion

        DataStorage Storage = new DataStorage();
        public AdminWindow()
        {
            InitializeComponent();
            DataContext = this;
            Books = Storage.GetBooks();
            Publishers = Storage.GetPublishers();
            Authors = Storage.GetAuthors();
            Genres = Storage.GetGenres();

        }


        #region List Mouse Double Click

        private void ShowBooksName(object sender, MouseButtonEventArgs e)
        {
            if (SelectedBook != null) { MessageBox.Show(SelectedBook.Name); }
        }
        private void ShowAuthorsName(object sender, MouseButtonEventArgs e)
        {
            if (SelectedAuthor != null) { MessageBox.Show($"{SelectedAuthor.FirstName} "); }
        }

        private void ShowPublishersName(object sender, MouseButtonEventArgs e)
        {
            if (SelectedPublisher != null) { MessageBox.Show(SelectedPublisher.Name); }
        }
        private void ShowGenreType(object sender, MouseButtonEventArgs e)
        {
            if (SelectedGenre != null) { MessageBox.Show($"{SelectedGenre.GenreType}"); }
        }
        #endregion

        #region Data Deleting 
        private void DeleteBook_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedBook != null)
            {
                var result = MessageBox.Show($"Are you sure you want to delete {SelectedBook.Name}", "Books Deleting", MessageBoxButton.YesNo, MessageBoxImage.Information);
                if (result == MessageBoxResult.Yes)
                {
                    MessageBox.Show($"{SelectedBook.Name} has been deleted");
                    Storage.RemoveBook(SelectedBook);
                    UpdateBooks();
                }

            }
        }

        private void DeletePublisher_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedPublisher != null)
            {
                var result = MessageBox.Show($"Are you sure you want to delete {SelectedPublisher.Name}\nBooks by this publisher will also be deleted!", "Publisehrs Deleting", MessageBoxButton.YesNo, MessageBoxImage.Information);
                if (result == MessageBoxResult.Yes)
                {
                    MessageBox.Show($"{SelectedPublisher.Name} has been deleted");
                    Storage.RemovePublisher(SelectedPublisher);
                    UpdatePublishers();
                    UpdateBooks();
                }
            }
        }

        private void DeleteAuthor_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedAuthor != null)
            {
                var result = MessageBox.Show($"Are you sure you want to delete {SelectedAuthor.FirstName} {SelectedAuthor.LastName}" +
                    $"\nBooks by this author will also be deleted!", "Autors Deleting", MessageBoxButton.YesNo, MessageBoxImage.Information);
                if (result == MessageBoxResult.Yes)
                {
                    MessageBox.Show($"{SelectedAuthor.FirstName} {SelectedAuthor.LastName} has been deleted");
                    Storage.RemoveAuthor(SelectedAuthor);
                    UpdateAuthors();
                    UpdateBooks();
                }
            }
        }

        private void DeleteGenre_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedGenre != null)
            {
                var result = MessageBox.Show($"Are you sure you want to delete {SelectedGenre.GenreType}\nBooks in this genre will also be deleted!", "Genre Deleting", MessageBoxButton.YesNo, MessageBoxImage.Information);
                if (result == MessageBoxResult.Yes)
                {
                    MessageBox.Show($"{SelectedGenre.GenreType} has been deleted");
                    Storage.RemoveGenre(SelectedGenre);
                    UpdateGenres();
                    UpdateBooks();

                }

            }
        }
        #endregion


        #region DataAddition

        private void AddBooksButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            var addBooksWindow = new AddBookWindow();
            addBooksWindow.ShowDialog();
            UpdateBooks();
            this.Show();
        }

        private void AddAuthorsButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            var addAuthorsWindow = new AddAuthorWindow();
            addAuthorsWindow.ShowDialog();
            UpdateAuthors();
            this.Show();
        }

        private void AddGenreButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            var addGenreWindow = new AddGenreWindow();
            addGenreWindow.ShowDialog();
            UpdateGenres();
            this.Show();
        }

        private void AddPublishers_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            var addNewPublisher = new AddPublisherWindow();
            addNewPublisher.ShowDialog();
            UpdatePublishers();
            this.Show();
        }



        #endregion

        #region DataUpdate

        private void UpdateGenre_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            var updateTheGenre = new UpdateGenreWindow();
            updateTheGenre.ShowDialog();
            UpdateGenres();
            UpdateBooks();
            this.Show();
        }

        private void UpdatePublisher_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            var updateThePublisher = new UpdatePublisherWindow();
            updateThePublisher.ShowDialog();
            UpdatePublishers();
            UpdateBooks();
            this.Show();
        }
        
        private void UpdateAuthor_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            var updateAuthors = new UpdateAuthors();
            updateAuthors.ShowDialog();
            UpdateAuthors();
            UpdateBooks();
            this.Show();
        }
        #endregion

    

        private void ShowStocksButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            var showStocks = new ViewStocksWindow();
            showStocks.ShowDialog();
            UpdateBooks();
            this.Show();
        }

        #region Exit App Button
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        #endregion

        private void ShowOrdersButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            var _viewOrderWindow = new ViewOrdersWindow();
            _viewOrderWindow.ShowDialog();
            this.Show();
        }

        private void UpdateBookButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            var _updateBookWindow = new UpdateBookWindow();
            _updateBookWindow.ShowDialog();
            UpdateBooks();
            this.Show();
        }
    }
}

