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
  
    public partial class UpdateGenreWindow : Window, INotifyPropertyChanged
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

        public UpdateGenreWindow()
        {
            InitializeComponent();
            DataContext = this;
            Genres = Storage.GetGenres();
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedGenre != null && (!String.IsNullOrWhiteSpace(GenreType.Text)))
            {
                Storage.UpdateGenre(SelectedGenre, GenreType.Text);
                UpdateGenres();
                MessageBox.Show("Genre has been updated");
                GenreType.Clear();
            }
            else { MessageBox.Show("Select Genre and Input valid Type"); GenreType.Clear(); }
        }
    }
}
