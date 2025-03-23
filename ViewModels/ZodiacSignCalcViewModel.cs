using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ZodiacSignsCalendar.Models;

namespace ZodiacSignsCalendar.ViewModels
{
    internal class ZodiacSignCalcViewModel : INotifyPropertyChanged
    {
        private UserBirthInfo _user = new UserBirthInfo();
        private string _ageMessage;
        private string _birthdayMessage;
        private string _westernZodiacMessage;
        private string _chineseZodiacMessage;

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public DateTime? BirthDate
        {
            get => _user.BirthDate;
            set 
            {
                _user.BirthDate = value;
                UpdateCanExecute();
            }
        }

        public string AgeMessage
        {
            get => _ageMessage; 
            set
            {
                _ageMessage = value;
                OnPropertyChanged(nameof(AgeMessage));
            }
        }
        public string BirthdayMessage
        {
            get => _birthdayMessage;
            set
            {
                _birthdayMessage = value;
                OnPropertyChanged(nameof(BirthdayMessage));
            }
        }
        public string WesternZodiacMessage 
        { 
            get => _westernZodiacMessage; 
            set 
            { 
                _westernZodiacMessage = value;
                OnPropertyChanged(nameof(WesternZodiacMessage));
            } 
        }
        public string ChineseZodiacMessage
        {
            get => _chineseZodiacMessage;
            set
            {
                _chineseZodiacMessage = value;
                OnPropertyChanged(nameof(ChineseZodiacMessage));
            }
        }

        public RelayCommand CalculateZodiacSignCommand { get; }

        public ZodiacSignCalcViewModel()
        {
            CalculateZodiacSignCommand ??= new RelayCommand(CalculateZodiacSign, CanExecute);
        }

        private void CalculateZodiacSign()
        {
            try
            {
                ClearFields();
                _user.CalculateZodiacSign();
                AgeMessage = $"You are {_user.Age} years old";
                WesternZodiacMessage = $"Your western zodiac sign is: {_user.WesternZodiac}";
                ChineseZodiacMessage = $"Your chinese zodiac sign is: {_user.ChineseZodiac}";
               
                if (_user.IsBirthday)
                {
                    BirthdayMessage = "Happy Birthday!";
                }
            } 
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            
        }


        private void ClearFields()
        {
            AgeMessage = "";
            BirthdayMessage = "";
            WesternZodiacMessage = "";
            ChineseZodiacMessage = "";
        }

        private bool CanExecute()
        {
            return BirthDate.HasValue;
        }

        private void UpdateCanExecute()
        {
            CalculateZodiacSignCommand.NotifyCanExecuteChanged();
        }
    }
}
