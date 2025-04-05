using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ZodiacSignsCalendar.Models;

namespace ZodiacSignsCalendar.ViewModels
{
    internal class ZodiacSignCalcViewModel : INotifyPropertyChanged
    {
        // Input fields from the UI
        private string _firstName = "";
        private string _lastName = "";
        private string _email = "";
        private DateTime? _selectedDate;

        // Output fields for messages
        private string _firstNameMessage = "";
        private string _lastNameMessage = "";
        private string _emailMessage = "";
        private string _birthDateMessage = "";
        private string _adultMessage = "";
        private string _sunSignMessage = "";
        private string _chineseSignMessage = "";
        private string _birthdayMessage = "";

        // Indicator for processing
        private bool _isProcessing;

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region Input Properties
        public string FirstName
        {
            get => _firstName;
            set
            {
                if (_firstName != value)
                {
                    _firstName = value;
                    OnPropertyChanged(nameof(FirstName));
                    UpdateCanProceed();
                }
            }
        }

        public string LastName
        {
            get => _lastName;
            set
            {
                if (_lastName != value)
                {
                    _lastName = value;
                    OnPropertyChanged(nameof(LastName));
                    UpdateCanProceed();
                }
            }
        }

        public string Email
        {
            get => _email;
            set
            {
                if (_email != value)
                {
                    _email = value;
                    OnPropertyChanged(nameof(Email));
                }
            }
        }

        /// <summary>
        /// This property is bound to a DatePicker in the view.
        /// </summary>
        public DateTime? SelectedDate
        {
            get => _selectedDate;
            set
            {
                if (_selectedDate != value)
                {
                    _selectedDate = value;
                    OnPropertyChanged(nameof(SelectedDate));
                    UpdateCanProceed();
                }
            }
        }

        #endregion

        #region Output Properties

        public string FirstNameMessage
        {
            get => _firstNameMessage;
            set
            {
                if (_firstNameMessage != value)
                {
                    _firstNameMessage = value;
                    OnPropertyChanged(nameof(FirstNameMessage));
                }
            }
        }

        public string LastNameMessage
        {
            get => _lastNameMessage;
            set
            {
                if (_lastNameMessage != value)
                {
                    _lastNameMessage = value;
                    OnPropertyChanged(nameof(LastNameMessage));
                }
            }
        }

        public string EmailMessage
        {
            get => _emailMessage;
            set
            {
                if (_emailMessage != value)
                {
                    _emailMessage = value;
                    OnPropertyChanged(nameof(EmailMessage));
                }
            }
        }

        public string BirthDateMessage
        {
            get => _birthDateMessage;
            set
            {
                if (_birthDateMessage != value)
                {
                    _birthDateMessage = value;
                    OnPropertyChanged(nameof(BirthDateMessage));
                }
            }
        }

        public string AdultMessage
        {
            get => _adultMessage;
            set
            {
                if (_adultMessage != value)
                {
                    _adultMessage = value;
                    OnPropertyChanged(nameof(AdultMessage));
                }
            }
        }

        public string SunSignMessage
        {
            get => _sunSignMessage;
            set
            {
                if (_sunSignMessage != value)
                {
                    _sunSignMessage = value;
                    OnPropertyChanged(nameof(SunSignMessage));
                }
            }
        }

        public string ChineseSignMessage
        {
            get => _chineseSignMessage;
            set
            {
                if (_chineseSignMessage != value)
                {
                    _chineseSignMessage = value;
                    OnPropertyChanged(nameof(ChineseSignMessage));
                }
            }
        }

        public string BirthdayMessage
        {
            get => _birthdayMessage;
            set
            {
                if (_birthdayMessage != value)
                {
                    _birthdayMessage = value;
                    OnPropertyChanged(nameof(BirthdayMessage));
                }
            }
        }

        #endregion

        #region Processing Properties
        public bool IsProcessing
        {
            get => _isProcessing;
            set
            {
                _isProcessing = value;
                OnPropertyChanged(nameof(IsProcessing));
                OnPropertyChanged(nameof(IsNotProcessing));
            }
        }

        public bool IsNotProcessing => !IsProcessing;
        #endregion

        public IAsyncRelayCommand ProceedCommand { get; }

        public ZodiacSignCalcViewModel()
        {
            ProceedCommand ??= new AsyncRelayCommand(OnProceedAsync, CanProceed);
        }
        
        private async Task OnProceedAsync()
        {
            try
            {
                IsProcessing = true;
                // Clear previous messages
                ClearMessages();

                // Generate messages
                await Task.Run(() =>
                {
                    DateOnly birthDate = DateOnly.FromDateTime(SelectedDate.Value);
                    Person person = new Person(FirstName, LastName, Email, birthDate);


                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        FirstNameMessage = $"Your first name is {person.FirstName}";
                        LastNameMessage = $"Your last name is {person.LastName}";
                        EmailMessage = string.IsNullOrWhiteSpace(person.Email) ? $"You didn't specify your email" : $"Your email is {person.Email}";
                        BirthDateMessage = $"Your date of birth is {person.BirthDate}";

                        AdultMessage = person.IsAdult.Value ? "You are an adult" : "You are not an adult";
                        SunSignMessage = $"Your Sun Sign is: {person.SunSign}";
                        ChineseSignMessage = $"Your Chinese Zodiac sign is: {person.ChineseSign}";
                        BirthdayMessage = person.IsBirthday.Value ? "Happy Birthday!" : "";
                    });
                });
                
            } 
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                IsProcessing = false;
            }
            
        }

        private void ClearMessages()
        {
            FirstNameMessage = "";
            LastNameMessage = "";
            EmailMessage = "";
            BirthDateMessage = "";

            AdultMessage = "";
            SunSignMessage = "";
            ChineseSignMessage = "";
            BirthdayMessage = "";
        }

        private bool CanProceed()
        {
            return SelectedDate.HasValue &&
                   !string.IsNullOrWhiteSpace(FirstName) &&
                   !string.IsNullOrWhiteSpace(LastName);
        }

        private void UpdateCanProceed()
        {
            ProceedCommand.NotifyCanExecuteChanged();
        }
    }
}
