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
    /// Логика взаимодействия для AddAuthorWindow.xaml
    /// </summary>
    public partial class AddAuthorWindow : Window, INotifyPropertyChanged
    {
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

      
        #endregion
        public void UpdateAuthors()
        {
            Authors = new ObservableCollection<Author>(Storage.GetAuthors());
        }

        private void EraseTextBlocks() 
        {
            FirstNameTextBlock.Clear();
            LastNameTextBox.Clear();
        }
        DataStorage Storage = new DataStorage();
        InputChecker Checker = new();
        public AddAuthorWindow()
        {
            InitializeComponent();
            DataContext = this;
            Authors = Storage.GetAuthors();
        }


        private void AddAuthorsButton_Click(object sender, RoutedEventArgs e)
        {
            string first_name = FirstNameTextBlock.Text;
            string last_name = LastNameTextBox.Text;
            if (Checker.IsCorrect(first_name, last_name))
            {
                var new_author = new Author();
                new_author.FirstName = first_name;
                new_author.LastName = last_name;
                Storage.AddAuthor(new_author);
                UpdateAuthors();
                MessageBox.Show("Author has been added");
                EraseTextBlocks();
            }
            else 
            {
                MessageBox.Show("Input correct data");
                EraseTextBlocks();
            }
        }
    }
}
