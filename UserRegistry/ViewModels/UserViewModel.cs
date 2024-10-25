using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using UserRegistry.Models;

namespace UserRegistry.ViewModels
{
    internal class UserViewModel : INotifyPropertyChanged
    {
        private User _user = new();
        private User _selectedUser = new();
        public static ObservableCollection<User> Users { get; set; } = [];
        public event PropertyChangedEventHandler PropertyChanged;

        public User GetUser
        {
            get => _user;
            set
            {
                _user = value;
                OnPropertyChanged(nameof(Name));
                OnPropertyChanged(nameof(Surname));
                OnPropertyChanged(nameof(DateOfBirth));
                OnPropertyChanged(nameof(Address));
                OnPropertyChanged(nameof(City));
                OnPropertyChanged(nameof(Cap));
                OnPropertyChanged(nameof(PhoneNumber));
            }
        }

        public User SelectedUser
        {
            get => _selectedUser;
            set
            {
                _selectedUser = value;
                if (_selectedUser != null)
                {
                    GetUser = new User()
                    {
                        Name = SelectedUser.Name,
                        Surname = SelectedUser.Surname,
                        DateOfBirth = SelectedUser.DateOfBirth,
                        Address = SelectedUser.Address,
                        City = SelectedUser.City,
                        Cap = SelectedUser.Cap,
                        PhoneNumber = SelectedUser.PhoneNumber,
                    };
                }

                OnPropertyChanged(nameof(_selectedUser));
            }
        }

        public string Name
        {
            get => _user.Name;
            set
            {
                _user.Name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public string Surname
        {
            get => _user.Surname;
            set
            {
                _user.Surname = value;
                OnPropertyChanged(nameof(Surname));
            }
        }

        public DateTimeOffset DateOfBirth
        {
            get => _user.DateOfBirth;
            set
            {
                _user.DateOfBirth = value;
                OnPropertyChanged(nameof(DateOfBirth));
            }
        }


        public string Address
        {
            get => _user.Address;
            set
            {
                _user.Address = value;
                OnPropertyChanged(nameof(Address));
            }
        }

        public string City
        {
            get => _user.City;
            set
            {
                _user.City = value;
                OnPropertyChanged(nameof(City));
            }
        }

        public int Cap
        {
            get => _user.Cap;
            set
            {
                _user.Cap = value;
                OnPropertyChanged(nameof(Cap));
            }
        }

        public string PhoneNumber
        {
            get => _user.PhoneNumber;
            set
            {
                _user.PhoneNumber = value;
                OnPropertyChanged(nameof(PhoneNumber));
            }
        }

        public void UpdateSelectedUser()
        {
            if (_selectedUser != null)
            {
                int index = Users.IndexOf(_selectedUser);
                if (index >= 0)
                {
                    var userId = Users[index].UserId;
                    GetUser.UserId = userId;
                    Users[index] = GetUser;
                }
            }
        }

        public void DeleteSelectedUser()
        {
            if (_selectedUser != null)
            {
                int index = Users.IndexOf(_selectedUser);
                if (index >= 0)
                {
                    Users.RemoveAt(index);
                }
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}