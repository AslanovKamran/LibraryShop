using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AdoNet_Exam.Storage
{
    public class User : INotifyPropertyChanged
    {
        #region PropertyChanged Realization

        public event PropertyChangedEventHandler PropertyChanged;
        private void InvokePropChanged([CallerMemberName] string prop = default)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        #endregion

        private int _id;
        public int Id
        {
            get { return _id; }
            set { _id = value; InvokePropChanged(); }
        }


        private string _firstName;
        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; InvokePropChanged(); }
        }


        private string _lastName;
        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; InvokePropChanged(); }
        }


        private string _login;
        public string Login
        {
            get { return _login; }
            set { _login = value; InvokePropChanged(); }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set { _password = value; InvokePropChanged(); }
        }

        private bool _isAdmin;

        public bool IsAdmin
        {
            get { return _isAdmin; }
            set { _isAdmin = value; InvokePropChanged(); }
        }

    }
}
