using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dane
{
    public class DaneZmianaKulka : EventArgs
    {
        public InterfejsDaneKulka Kulka { get; set; }

        public DaneZmianaKulka(InterfejsDaneKulka kulka)
        {
            Kulka = kulka;
        }
    }
}
