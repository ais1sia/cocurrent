using Logika;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ValidatorKulek : InterfaceValidator<int>
    {
        private readonly int min;
        private readonly int max;

        public ValidatorKulek(int min, int max)
        {
            this.min = min;
            this.max = max;
        }

        public ValidatorKulek() : this(Int32.MinValue) { }

        public ValidatorKulek(int min) : this(min, Int32.MaxValue) { }

        public bool IsValid(int val)
        {
            return val.IsBetween(min, max);     //ch1
        }

        public bool IsInvalid(int val)
        {
            return !IsValid(val);
        }
    }
}
