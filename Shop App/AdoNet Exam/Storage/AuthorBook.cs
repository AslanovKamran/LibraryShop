using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AdoNet_Exam.Storage
{
    public class AuthorBook : INotifyPropertyChanged
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


        private int _authorId;
        public int AuthorId
        {
            get { return _authorId; }
            set { _authorId = value; InvokePropChanged(); }
        }


    }
}
