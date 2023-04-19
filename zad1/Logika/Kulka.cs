using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Logika
{
    public class Kulka : IEquatable<Kulka>
    {
        public int Srednica { get; init; }

        public Vector2 Szybkosc { get; set; }
        public Vector2 Pozycja { get; set; }

      
        public Kulka(int srednica, Vector2 szybkosc, Vector2 pozycja)
        {
            Srednica = srednica;
            Szybkosc = szybkosc;
            Pozycja = pozycja;
        }

        public void Poruszanie(Vector2 granicaX, Vector2 granicaY) 
        {

            if (Szybkosc.CzyZero())    
            {
                return;
            }

            Pozycja += Szybkosc;// Jestem predkosciom ~ Wojtas 7.4.2023

            var (pozX, pozY) = Pozycja; 

            
            if (!pozX.IsBetween(granicaX.X-25, granicaX.Y, Srednica))
            {
                Szybkosc = new Vector2(-Szybkosc.X, Szybkosc.Y);
            }
            if (!pozY.IsBetween(granicaY.X-25, granicaY.Y, Srednica))
            {
                Szybkosc = new Vector2(Szybkosc.X, -Szybkosc.Y);
            }
            
        }

        //wymagane do interfejsu equatable
        public bool Equals(Kulka? other)
        {
            return other is not null
                && Srednica == other.Srednica
                && Pozycja == other.Pozycja
                && Szybkosc == other.Szybkosc;
        }
        
    }
}
