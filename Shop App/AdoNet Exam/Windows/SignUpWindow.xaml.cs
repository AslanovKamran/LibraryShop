using AdoNet_Exam.Services;
using AdoNet_Exam.Storage;
using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для SignUpWindow.xaml
    /// </summary>
    public partial class SignUpWindow : Window, INotifyPropertyChanged
    {
        #region PropertyChanged Realization

        public event PropertyChangedEventHandler PropertyChanged;
        private void InvokePropChanged([CallerMemberName] string prop = default)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        #endregion

        #region SupportingClasses
        InputChecker InputChecker = new InputChecker();
        DataStorage Storage = new DataStorage();
        #endregion
        private void ClearTextBoxes()
        {
            FirstNameTextBox.Clear();
            LastNameTextBox.Clear();
            LoginTextBox.Clear();

            PasswordTextBox.Clear();
            RepeatedPasswordTextBox.Clear();
        }
        public SignUpWindow()
        {
            InitializeComponent();
            DataContext = this;
        }


        private void SignUpButton_Click(object sender, RoutedEventArgs e)
        {
            var first_name = FirstNameTextBox.Text;
            var last_name = LastNameTextBox.Text;
            var login = LoginTextBox.Text;

            var password = PasswordTextBox.Password;
            var repeated_password = RepeatedPasswordTextBox.Password;

            bool isChked = InputChecker.SignUpChecker(first_name, last_name, login, password, repeated_password);
            if (isChked)
            {
                var new_User = new User();

                new_User.FirstName = first_name;
                new_User.LastName = last_name;
                new_User.Login = login;
                new_User.Password = password;

                Storage.InsertNewUser(new_User);
                ClearTextBoxes();
                this.Close();
            }
            else
            {
                MessageBox.Show("Not all gaps are filled or passwords don't match", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                ClearTextBoxes();
            }


        }


        #region WindowCommands

        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void HelpButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Fill the following blanks", "Helper", MessageBoxButton.OK, MessageBoxImage.Question);
        }

        #endregion

        private void EnterPressed(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SignUpButton_Click(SignUpButton, null);
            }
        }
    }
}
