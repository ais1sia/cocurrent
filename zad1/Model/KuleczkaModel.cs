using Logika;

namespace Model
{
    public class KuleczkaModel
    {
        private readonly Kuleczka kula;

        public KuleczkaModel(Kuleczka _kula)
        {
            this.kula = _kula;
        }
        
        public int Srednica => kula.Srednica;
        public Vector2 Pozycja => kula.Pozycja;
        public Vector2 Szybkosc => kula.Szybkosc;
    }
}