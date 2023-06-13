using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logika
{
    public static class Extension
    {
        public static bool IsBetween(this int val, int min, int max)
        {
            return val >= min && val <= max;
        }

        public static bool IsBetween(this float val, float min, float max, float pad = 0f)
        {
            if (pad < 0f)
            {
                throw new ArgumentException("Argument pad musi mieć wartość dodatnią!", nameof(pad));
            }

            return (val - pad >= min) && (val + pad <= max);
        }

        public static float Clamp(this float value, float min, float max)
        {
            if (value > max) return max;
            if (value < min) return min;
            return value;
        }
    }
}
