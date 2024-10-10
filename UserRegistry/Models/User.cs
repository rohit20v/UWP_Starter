using System;
using System.ComponentModel.DataAnnotations;

namespace UserRegistry.Models
{
    internal class User
    {
        private static int _commonIdCounter;

        [Key] public int UserId { get; private set;}
        [Required] public string Name { get; set; }
        [Required] public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public int Cap { get; set; }
        public int PhoneNumber { get; set; }

        public User(string name, string surname, DateTime dateOfBirth = default, string address = null, string city = null, int cap = default, int phoneNumber = default)
        {
            _commonIdCounter++;
            UserId = _commonIdCounter;
            Name = name;
            Surname = surname;
            DateOfBirth = dateOfBirth;
            Address = address;
            City = city;
            Cap = cap;
            PhoneNumber = phoneNumber;
        }
    }
}