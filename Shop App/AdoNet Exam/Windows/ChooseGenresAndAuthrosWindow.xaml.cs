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
    /// Логика взаимодействия для ChooseGenresAndAuthrosWindow.xaml
    /// </summary>
    public partial class ChooseGenresAndAuthrosWindow : Window, INotifyPropertyChanged
    {
        public string BookName { get; set; }

        #region PropertyChanged Realization

        public event PropertyChangedEventHandler PropertyChanged;
        private void InvokePropChanged([CallerMemberName] string prop = default)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
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

        DataStorage Storage = new DataStorage();
        public ChooseGenresAndAuthrosWindow()
        {
            InitializeComponent();
            DataContext = this;
            Authors = Storage.GetAuthors();
            Genres = Storage.GetGenres();
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            var _ListOfSelectedAuthors = new ObservableCollection<Author>(ListOfAuthors.SelectedItems.Cast<Author>()).ToList();
            var _ListOfSelectedGenres = new ObservableCollection<Genre>(ListOfGenres.SelectedItems.Cast<Genre>()).ToList();

            if (_ListOfSelectedAuthors.Count != 0 && _ListOfSelectedGenres.Count !=0)
            {

                foreach (Author item in _ListOfSelectedAuthors)
                {
                    Storage.AddAuthorsToNewBook(BookName, item.Id);
                }

                MessageBox.Show("Authors Has been added");

                foreach (Genre item in _ListOfSelectedGenres)
                {

                    
                    Storage.AddGenresToNewBook(BookName, item.Id);
                }
                MessageBox.Show("Genres Has been added");

                this.Close();

            }

            else { MessageBox.Show("No selections"); }
        }
    }
}
