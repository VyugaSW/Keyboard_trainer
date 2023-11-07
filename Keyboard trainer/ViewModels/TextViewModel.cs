using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Keyboard_trainer.Models;

namespace Keyboard_trainer.ViewModels
{
    public class TextViewModel
    {
        static readonly int LowTextFirstCode = 97;
        static readonly int LowTextLastCode = 123;
        static readonly int UpTextFirstCode = 65;
        static readonly int UpTextLastCode = 91;

        private int _curIndex;
        public TextModel TextModel { get; set; }
        public DifficultModel DifficultModel { get; set; }

        public TextViewModel()
        {
            _curIndex = -1;
            DifficultModel = new DifficultModel();
            TextModel = new TextModel();
        }

        public bool IsCorrect(string letter)
        {
            _curIndex++;

            try
            {
                if(TextModel.OutputText[_curIndex] == char.Parse(letter))
                    return true;
            }
            catch(FormatException ex) { throw ex; }

            return false;
        }

        public bool IsEnd() => TextModel.InputText.Length == TextModel.OutputText.Length;

        public void GenerateText()
        {
            Random random = new Random();

            int wordLenght;
            int DoubleSpaceChance;
            int changeRegisterChance;

            TextModel.OutputText = string.Empty;
            TextModel.InputText = string.Empty;

            for(int word = 0; word < DifficultModel.WordCount; word++)
            {
                wordLenght = random.Next(1,DifficultModel.MaxLenght);
                DoubleSpaceChance = random.Next(1, DifficultModel.FrequencyDoubleSpaces) / 2; // FrequencySpace maybe only: either 1 or 2 or 3

                for (int letter = 0; letter < wordLenght; letter++)
                {
                    
                    changeRegisterChance = random.Next(1, DifficultModel.FrequencyChangeRegister) / 2; // FrequencyChangeRegister maybe only: either 1 or 2 or 3

                    if (changeRegisterChance >= 1)
                        TextModel.OutputText += (char)random.Next(UpTextFirstCode, UpTextLastCode);
                    else
                        TextModel.OutputText += (char)random.Next(LowTextFirstCode, LowTextLastCode);
                }

                TextModel.OutputText += " ";

                if (DoubleSpaceChance >= 1)
                    TextModel.OutputText += " ";
            }


        }

        public void ChangeInputText(string key)
        {
            if (key == "BACKSPACE")
            {
                try
                {
                    _curIndex -= 2;
                    TextModel.InputText = TextModel.InputText.Remove(TextModel.InputText.Length - 1);
                }
                catch (Exception) { }
            }
            else
                TextModel.InputText += key;
        }
    }
}
