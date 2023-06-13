using Model;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;

namespace ViewModel
{
    public class ViewModelSim : ViewModelBase, IObserver<InterfejsKulkaModel>
    {
        private readonly ModelAbstractApi _model;
        private readonly InterfaceValidator<int> validator;

        private IDisposable? unsubscriber;

        private int liczbaKulek = 5;
        private bool flag = false;

        public int LiczbaKulek
        {
            get => liczbaKulek;
            set => SetField(ref liczbaKulek, value, validator, 1);
        }

        public bool getSetFlag
        {
            get => flag;
            private set => SetField(ref flag, value);
        }

        public ObservableCollection<InterfejsKulkaModel> Kulki { get; } = new();
        public ICommand SimStartCommand { get; init; }
        public ICommand SimStopCommand { get; init; }

        public ViewModelSim(ModelAbstractApi? model = default, InterfaceValidator<int>? validatorKulek = default)
            : base()
        {
            _model = model ?? ModelAbstractApi.StworzModelApi();
            validator = validatorKulek ?? new ValidatorKulek();

            SimStartCommand = new SimStartCommand(this);
            SimStopCommand = new SimStopCommand(this);
        }

        public void SimStart()
        {
            getSetFlag = true;
            Follow(_model);
            _model.Start(liczbaKulek);
        }

        public void SimStop()
        {
            getSetFlag = false;
            Kulki.Clear();
            _model.Stop();
        }
        
        public void OnCompleted()
        {
            unsubscriber?.Dispose();
        }

        public void OnError(Exception error)
        {
            throw error;
        }

        public void OnNext(InterfejsKulkaModel kulka)
        {
            Kulki.Add(kulka);
        }

        public void Follow(IObservable<InterfejsKulkaModel> provider)
        {
            unsubscriber = provider.Subscribe(this);
        }
    }
}