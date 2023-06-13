//using Dane;

namespace Logika
{
    public class Plansza
    {
        public int Szerokosc { get; init; }
        public int Wysokosc { get; init; }

        public Vector2 GranicaX => new Vector2(0, Szerokosc);
        public Vector2 GranicaY => new Vector2(0, Wysokosc);

        public Plansza(int szerokosc, int wysokosc)
        {
            Szerokosc = szerokosc;
            Wysokosc = wysokosc;
        }
    }
}