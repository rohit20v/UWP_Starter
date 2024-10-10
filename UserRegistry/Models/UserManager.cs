using System.Collections.ObjectModel;

namespace UserRegistry.Models
{
    internal class UserManager
    {
        public static ObservableCollection<User> UserList = [];

        public static ObservableCollection<User> GetUsers()
        {
            return UserList;
        }

        public static void AddUsers(User user)
        {
            UserList.Add(user);
        }
    }
}