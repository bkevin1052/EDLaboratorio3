using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibreriaDeClases.Interfaces;

namespace LibreriaDeClases.Clases
{
    public class ArbolAVL<T> : IArbolAVL<T>
    {
        private Nodo<T> raiz;


        public ArbolAVL()
        {
            raiz = null;
        }

        //Buscar Nodo

        public Nodo<T> Buscar(T dato, Nodo<T> nodo)
        {
            if (raiz == null)
            {
                return null;
            }
            else if (nodo.CompareTo(dato) > 0)// en duda
            {
                return nodo;
            }
            else if (nodo.CompareTo(dato) < 0)//en duda
            {
                return Buscar(dato, nodo.derecho);
            }
            else
            {
                return Buscar(dato, nodo.izquierdo);
            }
        }


        public int ObtenerFactorEquilibrio(Nodo<T> nodo)
        {
            if (nodo == null)
            {
                return -1;
            }
            else
            {
                return nodo.factorEquilibrio;
            }
        }


        public Nodo<T> RotacionIzquierda(Nodo<T> nodo)
        {
            Nodo<T> aux = nodo.izquierdo;
            nodo.izquierdo = aux.derecho;
            nodo.factorEquilibrio = Math.Max(ObtenerFactorEquilibrio(nodo.izquierdo), ObtenerFactorEquilibrio(nodo.derecho)) + 1;
            aux.factorEquilibrio = Math.Max(ObtenerFactorEquilibrio(aux.izquierdo), ObtenerFactorEquilibrio(aux.derecho)) + 1;

            return aux;
        }


        public Nodo<T> RotacionDerecha(Nodo<T> nodo)
        {
            Nodo<T> aux = nodo.derecho;
            nodo.derecho = aux.izquierdo;
            nodo.factorEquilibrio = Math.Max(ObtenerFactorEquilibrio(nodo.derecho), ObtenerFactorEquilibrio(nodo.izquierdo)) + 1;
            aux.factorEquilibrio = Math.Max(ObtenerFactorEquilibrio(aux.derecho), ObtenerFactorEquilibrio(aux.izquierdo)) + 1;

            return aux;
        }


        public Nodo<T> RotacionDobleIzquierda(Nodo<T> nodo)
        {
            Nodo<T> temp;
            nodo.izquierdo = RotacionDerecha(nodo.izquierdo);
            temp = RotacionIzquierda(nodo);

            return temp;
        }


        public Nodo<T> RotacionDobleDerecha(Nodo<T> nodo)
        {
            Nodo<T> temp;
            nodo.derecho = RotacionDerecha(nodo.derecho);
            temp = RotacionDerecha(nodo);

            return temp;
        }


        public Nodo<T> InsertarAVL(Nodo<T> nuevo, Nodo<T> subArbol)
        {
            Nodo<T> nuevoPadre = subArbol;
            if (nuevo.CompareTo(subArbol.dato) < 0)//en duda
            {
                if (subArbol.izquierdo == null)
                {
                    subArbol.izquierdo = nuevo;
                }
                else
                {
                    subArbol.izquierdo = InsertarAVL(nuevo, subArbol.izquierdo);
                    if ((ObtenerFactorEquilibrio(subArbol.izquierdo) - (ObtenerFactorEquilibrio(subArbol.derecho)) == 2))
                    {
                        if (nuevo.CompareTo(subArbol.izquierdo.dato) < 0) // en duda
                        {
                            nuevoPadre = RotacionIzquierda(subArbol);
                        }
                        else
                        {
                            nuevoPadre = RotacionDobleIzquierda(subArbol);

                        }

                    }
                }

            } else if (nuevo.CompareTo(subArbol.derecho.dato) > 0)// en duda
            {
                if (subArbol.derecho == null)
                {
                    subArbol.derecho = nuevo;
                }
                else
                {
                    subArbol.derecho = InsertarAVL(nuevo, subArbol.derecho);
                    if ((ObtenerFactorEquilibrio(subArbol.derecho) - (ObtenerFactorEquilibrio(subArbol.izquierdo)) == 2))
                    {
                        if (nuevo.CompareTo(subArbol.derecho.dato) > 0) // en duda
                        {
                            nuevoPadre = RotacionDerecha(subArbol);
                        }
                        else
                        {
                            nuevoPadre = RotacionDobleDerecha(subArbol);
                        }
                    }
                }
            }
            else
            {
                // Nodo duplicado
            }
            //Actualizar altura
            if ((subArbol.izquierdo == null) && (subArbol.derecho != null))
            {
                subArbol.factorEquilibrio = subArbol.derecho.factorEquilibrio + 1;
            }
            else if ((subArbol.izquierdo == null) && (subArbol.izquierdo != null))
            {
                subArbol.factorEquilibrio = subArbol.izquierdo.factorEquilibrio + 1;
            }
            else
            {
                subArbol.factorEquilibrio = Math.Max(ObtenerFactorEquilibrio(subArbol.izquierdo), ObtenerFactorEquilibrio(subArbol.derecho)) +1;
            }
            return nuevoPadre;
        }


        public void Insertar(T dato)
        {
            Nodo<T> nuevo = new Nodo<T>(dato);
            if(raiz == null)
            {
                raiz = nuevo;
            }
            else
            {
                raiz = InsertarAVL(nuevo, raiz);
            }
        }
    }
}
