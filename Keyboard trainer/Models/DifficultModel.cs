using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keyboard_trainer.Models
{



    public class DifficultModel : INotifyPropertyChanged
    {
        private int _difficulty;

        public int Difficulty
        {
            get => _difficulty;
            set
            {
                _difficulty = value;
                SetDifficulty(value);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Difficulty)));
            }
        }

        public int MaxLenght { get; private set; }
        public int FrequencyChangeRegister { get; private set; }
        public int FrequencyDoubleSpaces { get; private set; }
        public int WordCount { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public void SetDifficulty(int difficulty)
        {
            MaxLenght = difficulty * 10;
            FrequencyChangeRegister = difficulty;
            FrequencyDoubleSpaces = difficulty;
            WordCount = difficulty * 5;
        }
    }
}
