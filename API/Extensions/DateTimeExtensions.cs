using System;

namespace API.Extensions
{
    public static class DateTimeExtensions //like every extension we are gonna make it static
    {
        public static int CalculateAge(this DateTime dob){
             var today = DateTime.Today; //returns today's date
             var age = today.Year - dob.Year; //Returns your age, but one more validation is missing.
             if(dob.Date > today.AddYears(-age)) --age; //reduces your age by a year if your birthday haven't passed yet.
             return age;
        }
    }
}