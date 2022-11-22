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
    /// Логика взаимодействия для AddPublisherWindow.xaml
    /// </summary>
    public partial class AddPublisherWindow : Window, INotifyPropertyChanged
    {
        #region PropertyChanged Realization

        public event PropertyChangedEventHandler PropertyChanged;
        private void InvokePropChanged([CallerMemberName] string prop = default)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        #endregion

        #region Publishers Binding
        private ObservableCollection<Publisher> publishers = new();

        public ObservableCollection<Publisher> Publishers
        {
            get { return publishers; }
            set { publishers = value; InvokePropChanged(); }
        }



        #endregion

        public void UpdatePublishers()
        {
            Publishers = new ObservableCollection<Publisher>(Storage.GetPublishers());
        }

        DataStorage Storage = new DataStorage();
        public AddPublisherWindow()
        {
            InitializeComponent();
            DataContext = this;
            Publishers = Storage.GetPublishers();
        }
        private void AddPublisher_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(PublisherName.Text))
            {
                var newPublisher = new Publisher();
                newPublisher.Name = PublisherName.Text;
                Storage.AddPublisher(newPublisher);
                UpdatePublishers();
                MessageBox.Show("Publisher has been added");
                PublisherName.Clear();
            }
            else 
            {
                MessageBox.Show("Input coorect data");
            }
        }
    }
}
