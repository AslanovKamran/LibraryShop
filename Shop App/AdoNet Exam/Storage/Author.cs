using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AdoNet_Exam.Storage
{
    public class Author : INotifyPropertyChanged
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
    }
}
