using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dane
{
    public abstract class DaneAbstractApi
    {
        public abstract int WysokoscPlanszy { get; }
        public abstract int SzerokoscPlanszy { get; }
        public abstract int minSrednicaKuli { get; }
        public abstract int maxSrednicaKuli { get; }
        public abstract float predkosc { get; }


        public static DaneAbstractApi StworzDaneApi()
        {
            return new DaneApi();
        }
    }
}
