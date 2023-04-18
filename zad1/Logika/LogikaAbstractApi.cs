using Dane;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logika
{
    public abstract class LogikaAbstractApi : IObservable<IEnumerable<Kulka>>
    {
        public abstract IEnumerable<Kulka> Kulki { get; }

        public abstract void GenerowanieKulek(int liczba_kulek);
        public abstract void Sim();
        public abstract void StartSim();
        public abstract void StopSim();

        //wygenerowane przez visuala
        public abstract IDisposable Subscribe(IObserver<IEnumerable<Kulka>> observer);

        public static LogikaAbstractApi StworzLogikaApi(DaneAbstractApi? dane = default) 
        {
            return new SimKontroler(dane ?? DaneAbstractApi.StworzDaneApi());
        }
    }
}
