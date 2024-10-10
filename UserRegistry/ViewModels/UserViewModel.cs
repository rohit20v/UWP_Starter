using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using UserRegistry.Models;

namespace UserRegistry.ViewModels
{
    internal class UserViewModel : INotifyPropertyChanged
    {
        private User _user = new();
        public static ObservableCollection<User> Users { get; set; } = UserManager.GetUsers();
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

        public DateTime DateOfBirth
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

        public int PhoneNumber
        {
            get => _user.PhoneNumber;
            set
            {
                _user.PhoneNumber = value;
                OnPropertyChanged(nameof(PhoneNumber));
            }
        }


        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}