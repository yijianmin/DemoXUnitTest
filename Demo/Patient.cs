using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Demo
{
    public class Patient : Person,INotifyPropertyChanged
    {
        public Patient()
        {
            IsNew = true;
            BloodSugar = 4.900003f;

            History = new List<string>();
        }

        //public string FirstName { get; set; }
        //public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public int HeartBeatRate { get; set; }
        public bool IsNew { get; set; }
        public float BloodSugar { get; set; }
        public List<string> History { get; set; }

        public event EventHandler<EventArgs> PatientSlept;
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPatientSleep()
        {
            PatientSlept?.Invoke(sender: this, e: EventArgs.Empty);
        }

        public void Sleep()
        {
            OnPatientSleep();
        }

        public void NotAllowed()
        {
            throw new InvalidOperationException(message: "not able to create");
        }

        public void IncreaseHeartBeatRate()
        {
            HeartBeatRate = CalculateHeartBeatRate() + 2;
            OnPropertyChanged(nameof(HeartBeatRate));
        }

        private int CalculateHeartBeatRate()
        {
            var random = new Random();
            return random.Next(1, 100);
        }
        
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(sender: this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
