using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public interface InterfaceValidator<T>
    {
        bool IsValid (T value);
    }
}
