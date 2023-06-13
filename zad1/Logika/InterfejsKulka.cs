using Dane;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logika
{
    public interface InterfejsKulka : IObservable<InterfejsKulka>, IObserver<InterfejsDaneKulka>, IDisposable
    {
        public int Srednica { get;}
        public Vector2 Szybkosc { get; set; }
        public Vector2 Pozycja { get; }

        void Poruszanie(float s);
        bool CzyWZasiegu(InterfejsKulka kulka);  
    }
}
