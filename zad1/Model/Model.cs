using Logika;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Model : ApiModel
    {
        private readonly ISet<IObserver<IEnumerable<KuleczkaModel>>> observers;
        private IDisposable? unsubscriber;
        private readonly LogikaAbstractApi logika;      //checklist1

        public Model(LogikaAbstractApi? logika = default)
        {
            this.logika = logika ?? LogikaAbstractApi.StworzLogikaApi();
            observers = new HashSet<IObserver<IEnumerable<KuleczkaModel>>>();
            Subscribe(logika);
        }


        public override void GenerowanieKuleczek(int liczba_kulek)
        {
            logika.GenerowanieKuleczek(liczba_kulek);
        }

        public override void Start()
        {
            logika.StartSim();
        }

        public override void Stop()
        {
            logika.StopSim();
        }

        public static IEnumerable<KuleczkaModel> m(IEnumerable<Kuleczka> kulki)
        {
            return kulki.Select(kulka => new KuleczkaModel(kulka));
        }

        public void Subscribe(IObservable<IEnumerable<Kuleczka>> p)
        {
            unsubscriber = p.Subscribe(this);
        }

        public override void OnCompleted()
        {
            Unsubscribe();
            EndTransmission();
        }

        public override void OnError(Exception error)
        {
            throw error;
        }
        public override void OnNext(IEnumerable<Kuleczka> kulki)
        {
            SledzKulki(m(kulki));
        }

        public void Unsubscribe()
        {
            unsubscriber?.Dispose();
        }
        public override IDisposable Subscribe(IObserver<IEnumerable<KuleczkaModel>> obs)
        {
            if(!observers.Contains(obs))
            {
                observers.Add(obs);
            }
            return new Unsubscriber(observers, obs);
        }

        private class Unsubscriber : IDisposable
        {
            private readonly ISet<IObserver<IEnumerable<KuleczkaModel>>> observers;
            private readonly IObserver<IEnumerable<KuleczkaModel>> observer;

            public Unsubscriber(ISet<IObserver<IEnumerable<KuleczkaModel>>> observers, IObserver<IEnumerable<KuleczkaModel>> observer)
            {
                this.observers = observers;
                this.observer = observer;
            }
            //musi byc
            public void Dispose()
            {
                if (observer != null)
                {
                    observers.Remove(observer);
                }
            }

        }
        public void SledzKulki(IEnumerable<KuleczkaModel> kulki)
        {
            foreach(var observer in this.observers) { 
                if(kulki == null)
                {
                    observer.OnError(new NullReferenceException("Obiekt kulka jest null!"));
                }
                else
                {
                    observer.OnNext(kulki);
                }
            }
        }

        public void EndTransmission()
        {
            foreach (var observer in observers)
            {
                observer.OnCompleted();
            }

            observers.Clear();
        }
    }
}
