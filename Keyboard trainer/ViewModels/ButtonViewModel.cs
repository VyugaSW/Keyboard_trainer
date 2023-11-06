using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Diagnostics;



using Keyboard_trainer.Models;

namespace Keyboard_trainer.ViewModels
{
    public class ButtonViewModel
    {
        private ResourceManager _lowChar; // Resource file with lower register
        private ResourceManager _upChar; // Resource file with upper register
        public Dictionary<string, ButtonModel> Buttons { get; set; }

        public ButtonViewModel(Grid grid)
        {
            Buttons = new Dictionary<string, ButtonModel>();
            _lowChar = new ResourceManager("Keyboard_trainer.Resources.UpChar", Assembly.GetExecutingAssembly());
            _upChar = new ResourceManager("Keyboard_trainer.Resources.LowChar", Assembly.GetExecutingAssembly());

            AddButtons(grid);
            ButtonContentInit();
        }

        private void AddButtons(Grid grid)
        {
            foreach (Grid subGrid in grid.Children)
            {
                foreach (Button button in subGrid.Children)
                {
                    Buttons.Add(button.Name, new ButtonModel() { Button = button });
                }
            }
        }

        public void ButtonContentInit(bool isUpper = false)
        {
            ResourceManager resource = isUpper ? _lowChar : _upChar;

            for(int i = 0; i < Buttons.Count; i++)
                Buttons[Buttons.Keys.ElementAt(i)].Button.Content = resource.GetString(Buttons[Buttons.Keys.ElementAt(i)].Button.Name);
        }

        public string ButtonPressDown(Key key)
        {
            return Buttons[key.ToString()].Button.Content.ToString();
        }

        public bool IsCapitalOrShift(Key key) => (key == Key.Capital || key == Key.LeftShift || key == Key.RightShift) ? true : false;

        public string GiveKeyCharSpaceSymbol(Key key)
        {
            if (IsCapitalOrShift(key))
                return string.Empty;

            if (key == Key.Space)
                return " ";

            else if (key == Key.Back)
                return "BACKSPACE";

            else if (key != Key.LeftAlt || key != Key.RightAlt || key != Key.Return || 
                    key != Key.RWin || key != Key.LWin || key != Key.Tab || key != Key.LeftCtrl || key != Key.RightCtrl)
                return ButtonPressDown(key);

            return null;
        }
    }
}
