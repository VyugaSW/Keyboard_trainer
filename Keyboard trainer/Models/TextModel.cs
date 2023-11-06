using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Keyboard_trainer.Models
{
    public class TextModel : INotifyPropertyChanged
    {
        private string _inputText;
        private string _outputText;

        public string InputText
        {
            get => _inputText;
            set
            {
                _inputText = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(InputText)));
            }
        }
        public string OutputText
        {
            get => _outputText;
            set
            {
                _outputText = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(OutputText)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
