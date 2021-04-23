using System;
using System.Collections.Generic;
using API.Extensions;

namespace API.Entities
{
    public class AppUser
    {
        public int Id { get; set; }
        public string UserName { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }
        public DateTime DateOfBirth { get; set; } //Ask for Date of birth instead of age, because age changes every year
        public string KnownAs { get; set; } //something to use instead of a username
        public DateTime Created { get; set; } = DateTime.Now; //When this account was created
        public DateTime LastActive { get; set; } = DateTime.Now; // when was the last time this user was active
        public string Gender { get; set; }
        public string Introduction { get; set; }
        public string LookingFor { get; set; }
        public string Interests { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public ICollection<Photo> Photos { get; set; }

        //public int GetAge(){
            //return DateOfBirth.CalculateAge(); //Return someone's age based on the date of birth
       // }
    }
}