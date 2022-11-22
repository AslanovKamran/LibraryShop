using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AdoNet_Exam.Storage
{
    public class Order : INotifyPropertyChanged
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


        private int _bookId;
        public int BookId
        {
            get { return _bookId; }
            set { _bookId = value; InvokePropChanged(); }
        }

        private int _userId;
        public int UserId
        {
            get { return _userId; }
            set { _userId = value; InvokePropChanged(); }
        }



        private string _bookName;

        public string BookName
        {
            get { return _bookName; }
            set { _bookName = value; InvokePropChanged(); }
        }

        private string _userLogin;

        public string UserLogin
        {
            get { return _userLogin; }
            set { _userLogin = value; InvokePropChanged(); }
        }



        private DateTime _orderDate;
        public DateTime OrderDate
        {
            get { return _orderDate; }
            set { _orderDate = value; InvokePropChanged(); }
        }


    }
}
