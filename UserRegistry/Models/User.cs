using System;
using System.ComponentModel.DataAnnotations;

namespace UserRegistry.Models
{
    internal class User
    {
        [Key] public int UserId { get; set; }
        [Required] public string Name { get; set; }
        [Required] public string Surname { get; set; }
        public DateTimeOffset DateOfBirth { get; set; }
        public string DateOfBirthDisplay => DateOfBirth.ToString("dd-MM-yyyy");
        public string Address { get; set; }
        public string City { get; set; }
        public int Cap { get; set; }
        public string PhoneNumber { get; set; }
    }
}