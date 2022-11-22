using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AdoNet_Exam.Storage
{
    public class Book : INotifyPropertyChanged
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


        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; InvokePropChanged(); }
        }


        private int _piblisherId;
        public int PublisherId
        {
            get { return _piblisherId; }
            set { _piblisherId = value; InvokePropChanged(); }
        }


        private int _amountOfPages;
        public int AmountOfPages
        {
            get { return _amountOfPages; }
            set { _amountOfPages = value; InvokePropChanged(); }
        }


        private DateTime _year;
        public DateTime Year
        {
            get { return _year; }
            set { _year = value; InvokePropChanged(); }
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


        private int _amountOfBook;
        public int AmountOfBook
        {
            get { return _amountOfBook; }
            set { _amountOfBook = value; InvokePropChanged(); }
        }


        private bool _isSequel;
        public bool IsSequel
        {
            get { return _isSequel; }
            set { _isSequel = value; }
        }

        private string _bookGenre;
        public string BookGenre
        {
            get { return _bookGenre; }
            set { _bookGenre = value; InvokePropChanged(); }
        }

        private string _bookAuthor;

        public string BookAuthor
        {
            get { return _bookAuthor; }
            set { _bookAuthor = value; }
        }



    }
}
