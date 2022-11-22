using AdoNet_Exam.Services;
using AdoNet_Exam.Storage;
using AdoNet_Exam.Windows;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AdoNet_Exam
{

    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        #region PropertyChanged Realization

        public event PropertyChangedEventHandler PropertyChanged;
        private void InvokePropChanged([CallerMemberName] string prop = default)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        #endregion

        #region UsersList
        private ObservableCollection<User> _users = new ObservableCollection<User>();
        public ObservableCollection<User> Users
        {
            get { return _users; }
            set { _users = value; InvokePropChanged(); }
        }
        #endregion

        #region Supporting Classes (Storage and Checkers)
        DataStorage Storage = new DataStorage();
        InputChecker InputChecker = new InputChecker();
        MatchingChecker MatchingChecker = new MatchingChecker();
        #endregion

        private void ClearTextBoxes()
        {
            LoginTextBox.Clear();
            PasswordTextBox.Clear();
        }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        #region Enter KeyDown Login
        private void EnterPressed(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                LogInButton_Click(LogInButton, null);
            }
        }
        #endregion

        private void LogInButton_Click(object sender, RoutedEventArgs e)
        {
            Users = Storage.GetUsers();
            var login = LoginTextBox.Text;
            var password = PasswordTextBox.Password;
            int id = default;
            if (InputChecker.IsCorrect(login, password))
            {

                id = MatchingChecker.IsUserExists(Users, login, password);
                if (id > 0)
                {
                    var _user = Users.Where(x => x.Id == id).FirstOrDefault();
                    MessageBox.Show($"Welcome { _user.FirstName} {_user.LastName} " ); ClearTextBoxes();
                    if (_user.IsAdmin)
                    {
                        var admin_window = new AdminWindow();
                        this.Hide();
                        admin_window.ShowDialog();
                        this.Close();
                    }
                    else
                    {
                        var user_window = new UsersWindow();
                        user_window.UserId = _user.Id;
                        user_window.UserLogin = _user.Login;
                        this.Hide();
                        user_window.ShowDialog();
                        this.Close();

                       
                    }
                }
                else { MessageBox.Show("Incorrect Data", "Error", MessageBoxButton.OK, MessageBoxImage.Error); ClearTextBoxes(); };

            }
            else
            {
                MessageBox.Show("Input Correct Data", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                ClearTextBoxes();
            }

        }


        private void SignUpButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            var _singUpWindow = new SignUpWindow();
            _singUpWindow.ShowDialog();
            this.Show();
        }

        #region WindowCommands

        private void CloseForm_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        #endregion
    }
}
