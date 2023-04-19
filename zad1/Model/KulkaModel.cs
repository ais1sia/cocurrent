using Logika;

namespace Model
{
    public class KulkaModel
    {
        private readonly Kulka kula;

        public KulkaModel(Kulka _kula)
        {
            this.kula = _kula;
        }
        
        public int Srednica => kula.Srednica;
        public Vector2 Pozycja => kula.Pozycja;
        public Vector2 Szybkosc => kula.Szybkosc;
    }
}