using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dane
{
    public class DaneKulka : InterfejsDaneKulka
    {
        public int Srednica { get; init; }

        public float PosX { get; private set; }

        public float PosY { get; private set; }

        public float SpeedX { get; private set; }

        public float SpeedY { get; private set; }

        private ISet<IObserver<InterfejsDaneKulka>> _observers;

        public DaneKulka(int srednica, float posX, float posY, float speedX, float speedY)
        {
            Srednica = srednica;
            PosX = posX;
            PosY = posY;
            SpeedX = speedX;
            SpeedY = speedY;

            _observers = new HashSet<IObserver<InterfejsDaneKulka>>();
        }

        public async Task Przesuwanie(float przesuniecieX, float przesuniecieY)
        {
            PosX += przesuniecieX;
            PosY += przesuniecieY;
            SledzKulki(this);

            await Zapis();
        }
        private async Task Zapis()
        {
            await Task.Delay(1); //czas w milisekundach
        }

        public async Task SetPredkosc(float predkoscX, float predkoscY)
        {
            SpeedX = predkoscX;
            SpeedY = predkoscY;
            SledzKulki(this);

            await Zapis();
        }

        public IDisposable Subscribe(IObserver<InterfejsDaneKulka> observer)
        {
            _observers.Add(observer);
            return new Unsubscriber(_observers, observer);
        }

        private void SledzKulki(InterfejsDaneKulka kulka)
        {   
           foreach (var observer in _observers)
            {
                observer.OnNext(kulka);
            }
        }

        private class Unsubscriber : IDisposable
        {
            private readonly ISet<IObserver<InterfejsDaneKulka>> _observers;
            private readonly IObserver<InterfejsDaneKulka> _observer;
            public Unsubscriber(ISet<IObserver<InterfejsDaneKulka>> observers, IObserver<InterfejsDaneKulka> observer)
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
