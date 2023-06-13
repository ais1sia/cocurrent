 using Dane;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Diagnostics;

namespace Logika
{
    internal class LogikaApi : LogikaAbstractApi
    {
        private readonly IList<InterfejsKulka> _kulki;
        private readonly ISet<IObserver<InterfejsKulka>> _observers;
        private readonly DaneAbstractApi _dane;
        private readonly Plansza _plansza;
        private readonly Random _random = new();

        public LogikaApi(DaneAbstractApi? dane = default)
        {
            _dane = dane ?? DaneAbstractApi.StworzDaneApi();
            _observers = new HashSet<IObserver<InterfejsKulka>>();

            _plansza = new Plansza( _dane.SzerokoscPlanszy, _dane.WysokoscPlanszy);
            _kulki = new List<InterfejsKulka>();
        }

        public override IEnumerable<InterfejsKulka> StworzKulki(int count)
        {
           for(int i = 0; i < count; i++)
            {
                int srednica = GetRandomSrednica();
                Vector2 szybkosc = GetRandomSzybkosc();
                Vector2 poz = GetRandomPos(srednica);
                Kulka nowaKulka = new(srednica, szybkosc, poz, _plansza);

                _kulki.Add(nowaKulka);
                SledzKule(nowaKulka);
            }
            ThreadManager.SetValidator(ObslugaKolizji);
            ThreadManager.Start();

            return _kulki;
        }
        
        private void ObslugaKolizji()
        {
            var kolizje = Kolizje.Get(_kulki);
            if (kolizje.Count > 0)
            {
                
                
                foreach (var kolizja in kolizje)
                {
                    var (kulka1, kulka2) = kolizja;
                    (kulka1.Szybkosc, kulka2.Szybkosc) = Kolizje.ObliczSzybkosc(kulka1, kulka2);
                }
            }
            Thread.Sleep(1);   
        }
        
        private Vector2 GetRandomPos(int srednica)
        {
            int promien = (srednica / 2);
            int x = _random.Next((srednica / 2), _plansza.Szerokosc - (srednica / 2));
            int y = _random.Next((srednica / 2), _plansza.Wysokosc - (srednica / 2));
            return new Vector2(x, y);
        }

        private Vector2 GetRandomSzybkosc()
        {
            double x = (_random.NextDouble() * 2.0 - 1.0) * _dane.predkosc;
            double y = (_random.NextDouble() * 2.0 - 1.0) * _dane.predkosc;
            return new Vector2((float)x, (float)y);
        }

        private int GetRandomSrednica()
        {
            return _random.Next(_dane.minSrednicaKuli, _dane.maxSrednicaKuli + 1);
        }

        public override IDisposable Subscribe(IObserver<InterfejsKulka> observer)
        {
            _observers.Add(observer);
            return new Unsubscriber(_observers, observer);
        }

        private void SledzKule(InterfejsKulka kulka)
        {
            foreach(var observer in _observers)
            {
                observer.OnNext(kulka);
            }
        }

        private void KoniecTransmisji()
        {
            foreach(var observer in _observers)
            {
                observer.OnCompleted();
            }
            _observers.Clear();
        }

        private class Unsubscriber : IDisposable
        {
            private readonly ISet<IObserver<InterfejsKulka>> _observers;
            private readonly IObserver<InterfejsKulka> _observer;

            public Unsubscriber(ISet<IObserver<InterfejsKulka>> observers, IObserver<InterfejsKulka> observer)
            {
                _observers = observers;
                _observer = observer;
            }

            public void Dispose()
            {
                _observers.Remove(_observer);
            }
        }

        public override void Dispose()
        {
            KoniecTransmisji();
            ThreadManager.Stop();

            Trace.WriteLine($"Srednia delta = {ThreadManager.AverageDeltaTime}");
            Trace.WriteLine($"Srednie Fps = {ThreadManager.AverageFps}");
            Trace.WriteLine($"Klatki = {ThreadManager.FrameCount}");

            foreach (var kulka in _kulki)
            {
                kulka.Dispose();
            }
            _kulki.Clear();
        }
    }
}
