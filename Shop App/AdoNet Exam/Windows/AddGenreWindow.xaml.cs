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
    /// Логика взаимодействия для AddGenreWindow.xaml
    /// </summary>
    public partial class AddGenreWindow : Window, INotifyPropertyChanged
    {
        #region PropertyChanged Realization

        public event PropertyChangedEventHandler PropertyChanged;
        private void InvokePropChanged([CallerMemberName] string prop = default)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
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

        #region Update Genres

        public void UpdateGenres()
        {
            Genres = new ObservableCollection<Genre>(Storage.GetGenres());
        }
        #endregion

        DataStorage Storage = new DataStorage();
        public AddGenreWindow()
        {
            InitializeComponent();
            DataContext = this;
            Genres = Storage.GetGenres();
        }

        private void AddGenreButton_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(GenreType.Text))
            {
                var newGenre = new Genre();
                newGenre.GenreType = GenreType.Text;
                Storage.AddGenre(newGenre);
                UpdateGenres();
                MessageBox.Show("Genre has been added");
                GenreType.Clear();
            }
            else 
            {
                MessageBox.Show("Input Correct Data");
                GenreType.Clear();
            }
        }
    }
}
