using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ZodiacSignsCalendar.Models
{
    internal class UserBirthInfo
    {
        public DateTime? BirthDate { get; set; }
        public int Age { get; set; }
        public bool IsBirthday { get; set; }
        public string WesternZodiac { get; set; }
        public string ChineseZodiac { get; set; }

        public void CalculateZodiacSign()
        {
            if (!BirthDate.HasValue)
            {
                throw new NullReferenceException("Birth Date was not selected");
            }

            DateTime today = DateTime.Today;
            DateTime birthdate = BirthDate.Value;


            if (birthdate > today)
            {
                throw new ArgumentException("Your Date of Birth cannot be in the future");
            }

            Age = CalculateAge(birthdate);
            if (Age > 135)
            {
                throw new ArgumentException("You cannot be older than 135 years old");
            }

            IsBirthday = CheckBirthday(birthdate);
            WesternZodiac = CalculateWesternZodiac(birthdate);
            ChineseZodiac = CalculateChineseZodiac(birthdate);
        }

        private int CalculateAge(DateTime birthdate)
        {
            // Save today's date
            DateTime today = DateTime.Today;

            // Calculate the age
            int age = today.Year - birthdate.Year;

            // Subtract one year if the person was not born yet in the current year
            if (today < birthdate.AddYears(age))
            {
                age--;
            }

            return age;
        }

        private bool CheckBirthday(DateTime birthdate)
        {
            return DateTime.Today.ToString("MMdd") == birthdate.ToString("MMdd");
        }

        private string CalculateWesternZodiac(DateTime birthdate)
        {

            int day = birthdate.Day;
            int month = birthdate.Month;

            if ((month == 3 && day >= 21) || (month == 4 && day <= 19)) return "Aries";
            else if ((month == 4 && day >= 20) || (month == 5 && day <= 20)) return "Taurus";
            else if ((month == 5 && day >= 21) || (month == 6 && day <= 20)) return "Gemini";
            else if ((month == 6 && day >= 21) || (month == 7 && day <= 22)) return "Cancer";
            else if ((month == 7 && day >= 23) || (month == 8 && day <= 22)) return "Leo";
            else if ((month == 8 && day >= 23) || (month == 9 && day <= 22)) return "Virgo";
            else if ((month == 9 && day >= 23) || (month == 10 && day <= 22)) return "Libra";
            else if ((month == 10 && day >= 23) || (month == 11 && day <= 21)) return "Scorpio";
            else if ((month == 11 && day >= 22) || (month == 12 && day <= 21)) return "Sagittarius";
            else if ((month == 12 && day >= 22) || (month == 1 && day <= 19)) return "Capricorn";
            else if ((month == 1 && day >= 20) || (month == 2 && day <= 18)) return "Aquarius";
            else if ((month == 2 && day >= 19) || (month == 3 && day <= 20)) return "Pisces";

            return "Unknown";
        }

        private string CalculateChineseZodiac(DateTime birthdate)
        {

            int year = birthdate.Year;
            string[] zodiacSigns =
            [
                "Monkey",   // 0
                "Rooster",  // 1
                "Dog",      // 2
                "Pig",      // 3
                "Rat",      // 4
                "Ox",       // 5
                "Tiger",    // 6
                "Rabbit",   // 7
                "Dragon",   // 8
                "Snake",    // 9
                "Horse",    // 10
                "Goat"      // 11
            ];

            int zodiacIndex = year % 12;
            return zodiacSigns[zodiacIndex];
        }

    }
}
