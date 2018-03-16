using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaDeClases.Interfaces
{
    public interface IArbolAVL<T>
    {
        Nodo<T> Buscar(T dato, Nodo<T> nodo);
        int ObtenerFactorEquilibrio(Nodo<T> nodo);
        Nodo<T> RotacionIzquierda(Nodo<T> nodo);
        Nodo<T> RotacionDerecha(Nodo<T> nodo);
        Nodo<T> RotacionDobleIzquierda(Nodo<T> nodo);
        Nodo<T> RotacionDobleDerecha(Nodo<T> nodo);
        Nodo<T> InsertarAVL(Nodo<T> nuevo, Nodo<T> subArbol);
        void Insertar(Nodo<T> dato);
        void Eliminar(Nodo<T> dato);
        List<T> EnOrden();
        List<T> PreOrden();
        List<T> PostOrden();

    }
}
