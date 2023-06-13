using Logika;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public interface InterfejsKulkaModel : IObserver<InterfejsKulka>, INotifyPropertyChanged
    {
        public int Srednica { get; }
        public Vector2 Szybkosc { get; }
        public Vector2 Pozycja { get; }
    }
}
