using Model;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;

namespace ViewModel
{
    public class ViewModelSim : ViewModelBase, IObserver<IEnumerable<KulkaModel>>
    {
        private IDisposable? unsubscriber;

        private ObservableCollection<KulkaModel> kulki;
        private readonly ApiModel logic;
        private readonly InterfaceValidator<int> validator;
        private int liczbaKulek = 5;
        private bool flag = false;

        public int LiczbaKulek
        {
            get => liczbaKulek;
            set
            {
                if (validator.IsValid(value)) SetField(ref liczbaKulek, value);
                else liczbaKulek = 1;
            }
        }
        public bool getSetFlag
        {
            get => flag;
            private set => SetField(ref flag, value);
        }
        public IEnumerable<KulkaModel> Kulki => kulki;
        public ICommand SimStartCommand { get; init; }
        public ICommand SimStopCommand { get; init; }

        public ViewModelSim(ApiModel? model = default, InterfaceValidator<int>? validatorKulek = default)
            : base()
        {
            logic = model ?? ApiModel.StworzModelApi();
            validator = validatorKulek ?? new ValidatorKulek();
            kulki = new ObservableCollection<KulkaModel>();

            SimStartCommand = new SimStartCommand(this);
            SimStopCommand = new SimStopCommand(this);
            Subscribe(logic);
        }

        public void Subscribe(IObservable<IEnumerable<KulkaModel>> provider)//generated
        {
            unsubscriber = provider.Subscribe(this);
        }

        public void Unsubscribe()
        {
            unsubscriber?.Dispose();//generated
        }


        public void SimStart()
        {
            getSetFlag = true;
            logic.GenerowanieKulek(LiczbaKulek);
            logic.Start();
        }

        public void SimStop()
        {
            getSetFlag = false;
            logic.Stop();
        }

        //wymagane przez visual
        public void OnCompleted()
        {
            Unsubscribe();
        }

        public void OnError(Exception error)
        {
            throw error;
        }
        public void OnNext(IEnumerable<KulkaModel> kulki)
        {
           if(kulki == null)
           {
               kulki = new List<KulkaModel>();
           }
           this.kulki = new ObservableCollection<KulkaModel>(kulki);
           OnPropertyChanged(nameof(Kulki));
        }
    }
}