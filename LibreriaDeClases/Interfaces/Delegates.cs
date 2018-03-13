using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaDeClases.Interfaces
{

    public delegate int ComparadorNodosDelegate<T>(T actual, T nuevo);

    public delegate void RecorridoDelegate<T>(Nodo<T> actual);
}
