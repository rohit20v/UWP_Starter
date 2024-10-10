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

        public User()
        {
            _commonIdCounter++;
            UserId = _commonIdCounter;
        }
    }
}