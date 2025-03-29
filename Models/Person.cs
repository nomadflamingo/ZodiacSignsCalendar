using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ZodiacSignsCalendar.Models
{
    internal class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Email { get; set; }
        public DateOnly? BirthDate { get; set; }


        private readonly bool _isAdult;
        private readonly string _sunSign;
        private readonly string _chineseSign;
        private readonly bool _isBirthday;
        private readonly int _age;

        public bool? IsAdult => _isAdult;

        public string? SunSign => _sunSign;

        public string? ChineseSign => _chineseSign;

        public bool? IsBirthday => _isBirthday;

        public Person(string firstName, string lastName, string? email, DateOnly? birthDate)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            BirthDate = birthDate;

            if (!birthDate.HasValue)
            {
                throw new NullReferenceException("Birth Date was not selected");
            }

            if (birthDate.Value > DateOnly.FromDateTime(DateTime.Today))
            {
                throw new ArgumentException("Your Date of Birth cannot be in the future");
            }

            int age = CalculateAge(birthDate.Value);
            if (age > 135)
            {
                throw new ArgumentException("You cannot be older than 135 years old");
            }

            _isAdult = age >= 18;
            _sunSign = CalculateWesternZodiac(birthDate.Value);
            _chineseSign = CalculateChineseZodiac(birthDate.Value);
            _isBirthday = CheckBirthday(birthDate.Value);
        }

        public Person(string firstName, string lastName, string email)
        : this(firstName, lastName, email, null)
        {
        }

        public Person(string firstName, string lastName, DateOnly birthDate)
        : this(firstName, lastName, null, birthDate)
        {
        }

        private int CalculateAge(DateOnly birthdate)
        {
            // Save today's date
            DateOnly today = DateOnly.FromDateTime(DateTime.Today);

            // Calculate the age
            int age = today.Year - birthdate.Year;

            // Subtract one year if the person was not born yet in the current year
            if (today < birthdate.AddYears(age))
            {
                age--;
            }

            return age;
        }

        private bool CheckBirthday(DateOnly birthdate)
        {
            return DateTime.Today.ToString("MMdd") == birthdate.ToString("MMdd");
        }

        private string CalculateWesternZodiac(DateOnly birthdate)
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

        private string CalculateChineseZodiac(DateOnly birthdate)
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
