using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class SimStopCommand : CommandBase
    {
        private readonly ViewModelSim viewModelSim;

        public SimStopCommand(ViewModelSim viewModelSim) : base()
        {
            this.viewModelSim = viewModelSim;

            this.viewModelSim.PropertyChanged += OnSimViewModelPropertyChanged;
        }

        public override bool CanExecute(object? parameter)
        {
            return base.CanExecute(parameter)
                && viewModelSim.getSetFlag;
        }

        public override void Execute(object? parameter)
        {
            viewModelSim.SimStop();
        }

        private void OnSimViewModelPropertyChanged(object? sn, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(viewModelSim.getSetFlag))
            {
                OnCanExecuteChanged();
            }
        }
    }
}
