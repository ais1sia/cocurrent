using Logika;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{   
    public abstract class ApiModel : IObserver<IEnumerable<Kuleczka>>, IObservable<IEnumerable<KuleczkaModel>>
    {
        public abstract void GenerowanieKuleczek(int liczba_kulek);
        public abstract void Start();
        public abstract void Stop();

            
        public abstract void OnCompleted();
        public abstract void OnError(Exception error);
        public abstract void OnNext(IEnumerable<Kuleczka> val);
        public abstract IDisposable Subscribe(IObserver<IEnumerable<KuleczkaModel>> obs);
        
        public static ApiModel StworzModelApi(LogikaAbstractApi? logika = default)
        {
            return new Model(logika ?? LogikaAbstractApi.StworzLogikaApi());
        }
    }
}
