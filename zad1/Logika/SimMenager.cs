using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logika
{
    public class SimMenager
    {
        private const float maxSzybkosc = 10;//tutaj zmieniamy predkosc

        private readonly Plansza Board; 
        private readonly int SrednicaKuli;
        private readonly Random rand;

        public SimMenager(Plansza board, int srednicaKuli)
        {
            Board = board;
            SrednicaKuli = srednicaKuli;
            rand = new Random();
            Kulki = new List<Kuleczka>();
        }

        public IList<Kuleczka> Kulki { get; private set; }

        public void PushK()
        {
            foreach (var k in Kulki)
            {
                k.Poruszanie(Board.GranicaX, Board.GranicaY);
            }
        }

        private Vector2 GetRandSzybkosc()
        {
            double x = rand.NextDouble() * (maxSzybkosc / 2f);
            double y = rand.NextDouble() * (maxSzybkosc / 2f);
            return new Vector2((float)x, (float)y);
        }

        private Vector2 GetRandPozycja()
        {
            int x = rand.Next((SrednicaKuli / 2), Board.Szerokosc - (SrednicaKuli / 2))-15;
            int y = rand.Next((SrednicaKuli / 2), Board.Wysokosc - (SrednicaKuli / 2))-10;
            return new Vector2 (x, y);
        }

        public IList<Kuleczka> RandGenKulek(int liczba_kulek)
        {
            Kulki = new List<Kuleczka>(liczba_kulek);

            for (int i = 0; i < liczba_kulek; i++){
                Vector2 pozycja = GetRandPozycja();
                Vector2 speed = GetRandSzybkosc();
                Kulki.Add(new Kuleczka(SrednicaKuli, speed, pozycja));
            }
            return Kulki;
        }
    }
}
