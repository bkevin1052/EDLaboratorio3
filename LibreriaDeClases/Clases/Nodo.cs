using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibreriaDeClases.Interfaces;

namespace LibreriaDeClases
{
    public class Nodo<T> : IComparable<T>
    {
        public T dato { get; set; }
        public int factorEquilibrio { get; set; }

        public Nodo<T> izquierdo { get; set; }
        public Nodo<T> derecho { get; set; }

        public ComparadorNodosDelegate<T> comparador;

        public Nodo(T dato)
        {
            this.dato = dato;
            this.factorEquilibrio = 0;
            this.derecho = null;
            this.izquierdo = null;
        }

        public int CompareTo(T other)
        {
            return comparador(this.dato, other);
        }
    }
}
