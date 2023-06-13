using Logika;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public abstract class ModelAbstractApi : IObserver<InterfejsKulka>, IObservable<InterfejsKulkaModel>
    {
        public static ModelAbstractApi StworzModelApi(LogikaAbstractApi? logika = default)
        {
            return new ApiModel(logika ?? LogikaAbstractApi.StworzLogikaApi());
        }

        public abstract void Start(int liczba);
        public abstract void Stop();

        public abstract IDisposable Subscribe(IObserver<InterfejsKulkaModel> observer);

        public abstract void OnCompleted();
        public virtual void OnError(Exception error) => throw error;
        public abstract void OnNext(InterfejsKulka value);

    }
}
