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

        public void Insertar(Nodo<T> nuevo)
        {
            if (raiz == null)
            {
                raiz = nuevo;
            }
            else
            {
                raiz = InsertarAVL(nuevo, raiz);
            }
        }

        public void Eliminar(Nodo<T> dato)
        {
            throw new NotImplementedException();
        }

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
            aux.derecho = nodo;
            nodo.factorEquilibrio = Math.Max(ObtenerFactorEquilibrio(nodo.izquierdo), ObtenerFactorEquilibrio(nodo.derecho)) + 1;
            aux.factorEquilibrio = Math.Max(ObtenerFactorEquilibrio(aux.izquierdo), ObtenerFactorEquilibrio(aux.derecho)) + 1;

            return aux;
        }


        public Nodo<T> RotacionDerecha(Nodo<T> nodo)
        {
            Nodo<T> aux = nodo.derecho;
            nodo.derecho = aux.izquierdo;
            aux.izquierdo = nodo;
            nodo.factorEquilibrio = Math.Max(ObtenerFactorEquilibrio(nodo.izquierdo), ObtenerFactorEquilibrio(nodo.derecho)) + 1;
            aux.factorEquilibrio = Math.Max(ObtenerFactorEquilibrio(aux.izquierdo), ObtenerFactorEquilibrio(aux.derecho)) + 1;

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
            nodo.derecho = RotacionIzquierda(nodo.derecho);
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

            } else if (nuevo.CompareTo(subArbol.dato) > 0)// en duda
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
                throw new Exception("Nodo duplicado");
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

        public void InOrden(Nodo<T> nodo)
        {
            if(nodo != null)
            {

                InOrden(nodo.izquierdo);
                //obtener dato aqui
                InOrden(nodo.derecho);
            }
        }
        public void PreOrden(Nodo<T> nodo)
        {
            if (nodo != null)
            {
                //obtener dato aqui
                InOrden(nodo.izquierdo);
                InOrden(nodo.derecho);
            }
        }
        public void PostOrden(Nodo<T> nodo)
        {
            if (nodo != null)
            {
                InOrden(nodo.izquierdo);
                InOrden(nodo.derecho);
                //obtener dato aqui
            }
        }

        
    }
}
