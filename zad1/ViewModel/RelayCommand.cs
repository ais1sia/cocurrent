using System;
using System.Windows.Input;

public class RelayCommand : ICommand {
    private readonly Action handler;
    private bool isEnabled;

    public RelayCommand(Action handler) {
        this.handler = handler;
        IsEnabled = true;
    }

    public bool IsEnabled {
        get { return isEnabled; }
        set {
            if (value != isEnabled) {
                isEnabled = value;
                CanExecuteChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public bool CanExecute(object parameter) {
        return IsEnabled;
    }

    public event EventHandler? CanExecuteChanged;

    public void Execute(object parameter) {
        handler();
    }
}
