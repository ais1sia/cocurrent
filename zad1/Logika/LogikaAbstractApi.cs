using Dane;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logika
{
    public abstract class LogikaAbstractApi : IObservable<InterfejsKulka>
    {
        public static LogikaAbstractApi StworzLogikaApi(DaneAbstractApi? dane = default)
        {
            return new LogikaApi(dane ?? DaneAbstractApi.StworzDaneApi());
        }
        //wygenerowane przez visuala
        public abstract IDisposable Subscribe(IObserver<InterfejsKulka> observer);

        public abstract void Dispose();

        public abstract IEnumerable<InterfejsKulka> StworzKulki(int count);
    }
}
