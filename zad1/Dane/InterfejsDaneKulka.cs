using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dane
{
    public interface InterfejsDaneKulka : IObservable<InterfejsDaneKulka>
    {
        int Srednica { get; init; }

        float PosX { get; }
        float PosY { get; }

        float SpeedX { get; }
        float SpeedY { get; }

        Task SetPredkosc(float predkoscX, float predkoscY); 
        Task Przesuwanie(float przesuniecieX, float przesuniecieY);
    }
}
