using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AdoNet_Exam.Storage
{
    public class Stock : INotifyPropertyChanged
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

        private string _bookName;

        public string BookName
        {
            get { return _bookName; }
            set { _bookName = value; InvokePropChanged(); }
        }

        private decimal _primeCost;

        public decimal PrimeCost
        {
            get { return _primeCost; }
            set { _primeCost = value; InvokePropChanged(); }
        }

        private int _discountPercent;

        public int DiscountPercent
        {
            get { return _discountPercent; }
            set { _discountPercent = value; InvokePropChanged(); }
        }

        private decimal _price;

        public decimal Price
        {
            get { return _price; }
            set { _price = value; InvokePropChanged(); }
        }





    }
}
