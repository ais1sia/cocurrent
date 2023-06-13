using Logika;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class PlanszaModel
    {
        private readonly Plansza board;

        public PlanszaModel(Plansza _board)
        {
            this.board = _board;
        }

        public int Wysokosc => board.Wysokosc;
        public int Szerokosc => board.Szerokosc;
    }
}