using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserRegistry.Models
{
    internal class UserManager
    {
        public static ObservableCollection<User> UserList = [];

        public ObservableCollection<User> GetUsers()
        {
            return UserList;
        }

        public void AddUsers(User user)
        {
            UserList.Add(user);
        }
    }
}
