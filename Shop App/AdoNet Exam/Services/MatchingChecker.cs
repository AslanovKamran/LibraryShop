using AdoNet_Exam.Storage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoNet_Exam.Services
{
    public class MatchingChecker
    {
        public int IsUserExists(ObservableCollection<User> users, string login, string password) 
        {
            bool _IsExists = false;
            int _userId = 0;
            foreach (var item in users)
            {
                if (item.Login == login)
                {
                    if (item.Password == password)
                    {
                        _IsExists = true;
                        _userId = item.Id;
                        break;
                    }
                }
            }

            if (_IsExists) { return _userId; }
            else { return -1; }

        }
    }
}
