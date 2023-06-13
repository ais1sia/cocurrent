using Logika;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    internal class ApiModel : ModelAbstractApi
    {
        private readonly LogikaAbstractApi _logika;
        private readonly ISet<IObserver<InterfejsKulkaModel>> _observers;
        private readonly IDictionary<InterfejsKulka, InterfejsKulkaModel> _kulkaDoModelu;

        private IDisposable? _unsubscriber;

        public ApiModel(LogikaAbstractApi? logika = default)
        {
            _logika = logika ?? LogikaAbstractApi.StworzLogikaApi();
            _observers = new HashSet<IObserver<InterfejsKulkaModel>>();
            _kulkaDoModelu = new Dictionary<InterfejsKulka, InterfejsKulkaModel>();
        }

        public override void Start(int liczbaKulek)
        {
            Follow(_logika);
            _logika.StworzKulki(liczbaKulek);
        }

        public override void Stop()
        {
            _logika.Dispose();
        }

        public void Follow(IObservable<InterfejsKulka> provider)
        {
            _unsubscriber = provider.Subscribe(this);
        }

        public override void OnCompleted()
        {
            _unsubscriber?.Dispose();
            KoniecTransmisji();
        }

        public override void OnNext(InterfejsKulka kulka)
        {
            _kulkaDoModelu.TryGetValue(kulka, out var kulkaModel);
            if (kulkaModel == null)
            {
                kulkaModel = new KulkaModel(kulka);
                _kulkaDoModelu.Add(kulka, kulkaModel);
            }
            SledzKule(kulkaModel);
        }

        public override IDisposable Subscribe(IObserver<InterfejsKulkaModel> observer)
        {
            _observers.Add(observer);
            return new Unsubscriber(_observers, observer);
        }

        private void SledzKule(InterfejsKulkaModel kulka)
        {
            foreach (var observer in _observers)
            {
                observer.OnNext(kulka);
            }
        }

        private void KoniecTransmisji()
        {
            foreach (var observer in _observers)
            {
                observer.OnCompleted();
            }
            _observers.Clear();
        }

        private class Unsubscriber : IDisposable
        {
            private readonly ISet<IObserver<InterfejsKulkaModel>> _observers;
            private readonly IObserver<InterfejsKulkaModel> _observer;

            public Unsubscriber(ISet<IObserver<InterfejsKulkaModel>> observers, IObserver<InterfejsKulkaModel> observer)
            {
                _observers = observers;
                _observer = observer;
            }

            public void Dispose()
            {
                _observers.Remove(_observer);
            }
        }
    }
}
