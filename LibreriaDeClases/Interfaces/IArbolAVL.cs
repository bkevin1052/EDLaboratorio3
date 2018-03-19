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
        Nodo<T> ObtenerRaiz();
        int ObtenerFactorEquilibrio(Nodo<T> nodo);
        Nodo<T> RotacionalaDerecha(Nodo<T> nodo);
        Nodo<T> RotacionalaIzquierda(Nodo<T> nodo);
        Nodo<T> RotacionDoblealaDerecha(Nodo<T> nodo);
        Nodo<T> RotacionDoblealaIzquierda(Nodo<T> nodo);
        Nodo<T> InsertarAVL(Nodo<T> nuevo, Nodo<T> subArbol);
        void Insertar(Nodo<T> dato);
        void Eliminar(T dato);
        List<T> EnOrden();
        List<T> PreOrden();
        List<T> PostOrden();

    }
}
