using Dane;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logika
{
    internal class SimKontroler : LogikaAbstractApi
    {
        public override IEnumerable<Kuleczka> Kulki => simMenager.Kulki;

        private readonly ISet<IObserver<IEnumerable<Kuleczka>>> obs;
        private readonly DaneAbstractApi dane;      //checklist1
        private readonly SimMenager simMenager;

        private bool flag = false;

        public SimKontroler(DaneAbstractApi? dane = default)
        {
            this.dane = dane ?? DaneAbstractApi.StworzDaneApi();
            simMenager = new SimMenager(new Plansza(this.dane.SzerokoscPlanszy, this.dane.WysokoscPlanszy),
                this.dane.SrednicaKuli);
            obs = new HashSet<IObserver<IEnumerable<Kuleczka>>>();
        }

        public override void GenerowanieKuleczek(int liczba_kulek)
        {
            simMenager.RandGenKulek(liczba_kulek);
        }

        public override void Sim()
        {
            while(flag)
            {
                simMenager.PushK();
                SledzKulki(Kulki);
                Thread.Sleep(10);
            }
        }

        public override void StartSim()
        {
            if (!flag)
            {
                flag = true;
                Task.Run(Sim);
            }
        }

        public override void StopSim()
        {
            if (flag)
            {
                flag = false;
            }
        }

        public override IDisposable Subscribe(IObserver<IEnumerable<Kuleczka>> _observer)
        {
            obs.Add(_observer);
            return new Unsubcriber(obs, _observer);
        }

        private class Unsubcriber : IDisposable//mechanizm zrzucenie nieprzydzielonych zasobów
        {
            private readonly ISet<IObserver<IEnumerable<Kuleczka>>> observers;
            private readonly IObserver<IEnumerable<Kuleczka>> observer;

            public Unsubcriber(ISet<IObserver<IEnumerable<Kuleczka>>> observers, IObserver<IEnumerable<Kuleczka>> observer)
            {
                this.observers = observers;
                this.observer = observer;
            }
            public void Dispose()//wymog do Disposable, zasugerowane do napisania przez visual
            {
                if(observer is not null){
                    observers.Remove(observer);
                }
            }
        }

        public void SledzKulki(IEnumerable<Kuleczka> kulki)
        {
            foreach(var observer in obs)
            {
                if(kulki is null)
                {
                    observer.OnError(new NullReferenceException("Obiekt Kula jest null"));
                }
                else
                {
                    observer.OnNext(kulki);
                }
            }
        }
    }
}
