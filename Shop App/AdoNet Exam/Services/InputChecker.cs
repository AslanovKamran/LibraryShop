using AdoNet_Exam.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace AdoNet_Exam.Services
{
    public class InputChecker
    {
        public bool IsCorrect(string login, string password)
        {
            if ((!String.IsNullOrEmpty(login)) && (!String.IsNullOrEmpty(password)) && (!login.Contains(" ")) && (!password.Contains(" ")))
            {
                return true;
            }
            else return false;
        }

        public bool SignUpChecker(string firtName, string lastName, string login, string password, string repeated_password)
        {
            

            if
                (

                    (!String.IsNullOrEmpty(firtName)) && (!String.IsNullOrEmpty(lastName)) && (!firtName.Contains(" ")) && (!lastName.Contains(" ")) &&
                    (!String.IsNullOrEmpty(login)) && (!String.IsNullOrEmpty(password)) && (!login.Contains(" ")) && (!password.Contains(" ")) &&
                    (!String.IsNullOrEmpty(repeated_password)) && (!repeated_password.Contains(" ")) && (repeated_password == password)
                )
                    { return true; }

            else return false;
        }

        public bool AddBookChecker( string _bookName, string _amountOfPages, string _primeCost, string _discount, string _amountOfBooks) 
        {
            if
              (

                  (!String.IsNullOrEmpty(_bookName)) &&
                  (int.TryParse(_amountOfPages, out _)) &&  (Convert.ToInt32(_amountOfPages) > 0) &&
                  (decimal.TryParse(_primeCost, out _)) &&  (Convert.ToDecimal(_primeCost) > 0) &&
                  (int.TryParse(_discount, out _))      &&  (Convert.ToInt32(_discount) >= 0) && (Convert.ToInt32(_discount) < 100) &&
                  (int.TryParse(_amountOfBooks, out _)) && (Convert.ToInt32(_amountOfBooks) > 0)


              ) { return true; }

            else return false;
        }

        public bool AddOrderChecker(Book _selectedBook) 
        {
            if (_selectedBook != null) { return true; }
            else return false;
        }
    }
}
