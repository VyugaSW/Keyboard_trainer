using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Keyboard_trainer.Models;

namespace Keyboard_trainer.ViewModels
{
    public class StatisticViewModel
    {
        public StatisticModel StatisticModel { get; set; }

        public StatisticViewModel()
        {
            StatisticModel = new StatisticModel();
        }
    }
}
