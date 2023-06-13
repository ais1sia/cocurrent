using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class ViewModelMain : ViewModelBase
    {
        public ViewModelBase thisViewModel { get; }

        public ViewModelMain() : base()
        {
            thisViewModel = new ViewModelSim(validatorKulek: new ValidatorKulek(1, 20));
        }
    }
}