using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keyboard_trainer.Models
{
    public class StatisticModel : INotifyPropertyChanged
    {

        private int _speed;
        private int _fails; 

        public int Speed
        {
            get => _speed;
            set
            {
                _speed = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Speed)));
            }
        }
        public int Fails
        {
            get => _fails;
            set
            {
                _fails = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Fails)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

    }
}
