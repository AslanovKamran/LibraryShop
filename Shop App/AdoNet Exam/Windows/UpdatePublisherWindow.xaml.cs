using AdoNet_Exam.Storage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Policy;
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
    /// Логика взаимодействия для UpdatePublisherWindow.xaml
    /// </summary>
    public partial class UpdatePublisherWindow : Window, INotifyPropertyChanged
    {
        #region PropertyChanged Realization

        public event PropertyChangedEventHandler PropertyChanged;
        private void InvokePropChanged([CallerMemberName] string prop = default)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        #endregion

        #region Publishers Binding
        private ObservableCollection<Storage.Publisher> publishers = new();

        public ObservableCollection<Storage.Publisher> Publishers
        {
            get { return publishers; }
            set { publishers = value; InvokePropChanged(); }
        }

        private Storage.Publisher _selectedPublisher = new();
        public Storage.Publisher SelectedPublisher
        {
            get { return _selectedPublisher; }
            set { _selectedPublisher = value; InvokePropChanged(); }
        }

        #endregion

        public void UpdatePublishers()
        {
            Publishers = new ObservableCollection<Storage.Publisher>(Storage.GetPublishers());
        }

        DataStorage Storage = new DataStorage();

        public UpdatePublisherWindow()
        {
            InitializeComponent();
            DataContext = this;
            Publishers = Storage.GetPublishers();
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedPublisher != null && (!String.IsNullOrWhiteSpace(PublisherName.Text)))
            {
                Storage.UpdatePublisher(SelectedPublisher, PublisherName.Text);
                UpdatePublishers();
                MessageBox.Show("Genre has been updated");
                PublisherName.Clear();
            }
            else { MessageBox.Show("Select Genre and Input valid Type"); PublisherName.Clear(); }
        }
    }
}
