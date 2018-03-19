using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibreriaDeClases.Interfaces;

namespace LibreriaDeClases.Clases
{
    public class ArbolAVL<T> : IArbolAVL<T>
    {
        private Nodo<T> raiz;
        List<T> lista;
        string rutaArchivo;
        bool sobreescribir;

        public ArbolAVL()
        {
            raiz = null;
        }
        public void logWriterAsignacion(string rutaArchivo, bool sobrescribir = true)
        {
            this.rutaArchivo = rutaArchivo;
            this.sobreescribir = sobrescribir;
        }

        public void logWriter(string contenido)
        {
            StreamWriter logReporter = new StreamWriter(rutaArchivo, sobreescribir);
            logReporter.WriteLine(contenido + "; " + DateTime.Now);
            logReporter.Close();
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
        public void Eliminar(T dato)
        {
            if (raiz != null)
            {
                EliminarInterno(dato);
            }
        }
        public void EliminarInterno(T dato)
        {
            Nodo<T> aux = raiz;
            Nodo<T> nodo = raiz;
            Nodo<T> padre = BuscarPadre(aux, dato);
            if(padre == null && aux.CompareTo(dato) == 0)
            {
                if (raiz.izquierdo == null)
                {
                    if (raiz.derecho == null)
                        raiz = null;
                    else
                    {
                        aux = raiz;
                        raiz = raiz.derecho;
                        aux = null;
                    }

                }
                else if (raiz.derecho == null)
                {
                    aux = raiz;
                    raiz = raiz.izquierdo;
                    aux = null;
                }
                else
                {
                    aux = EncontrarMasDerechadeIzquierdo(raiz.izquierdo);
                    nodo = BuscarPadre(raiz, aux.dato);
                    if (nodo.CompareTo(raiz.dato) != 0)
                    {
                        nodo.derecho = null;
                        aux.izquierdo = raiz.izquierdo;
                        aux.derecho = raiz.derecho;
                        raiz = null;
                        raiz = aux;
                    }
                    else
                    {
                        aux.derecho = raiz.derecho;
                        raiz = null;
                        raiz = aux;
                    }
                }
            }
            else if(padre !=null)
            {
                if(padre.derecho != null && padre.derecho.CompareTo(dato) == 0)
                {
                    if (padre.derecho.izquierdo == null)
                    {
                        if (padre.derecho.derecho == null)
                            padre.derecho = null;
                        else
                        {
                            aux = padre.derecho;
                            padre.derecho = padre.derecho.derecho;
                            aux = null;
                        }

                    }
                    else if (padre.derecho.derecho == null)
                    {
                        aux = padre.derecho;
                        padre.derecho = padre.derecho.izquierdo;
                        aux = null;
                    }
                    else
                    {
                        aux = EncontrarMasDerechadeIzquierdo(padre.derecho.izquierdo);
                        nodo = BuscarPadre(raiz, aux.dato);
                        if (nodo.CompareTo(padre.derecho.dato) != 0)
                        {
                            nodo.derecho = null;
                            aux.izquierdo = padre.derecho.izquierdo;
                            aux.derecho = padre.derecho.derecho;
                            padre.derecho = null;
                            padre.derecho = aux;
                        }
                        else
                        {
                            aux.derecho = padre.derecho.derecho;
                            padre.derecho = null;
                            padre.derecho = aux;
                        }
                    }
                }
                else if(padre.izquierdo != null && padre.izquierdo.CompareTo(dato) == 0)
                {
                    if (padre.izquierdo.izquierdo == null)
                    {
                        if (padre.izquierdo.derecho == null)
                        {
                            padre.izquierdo = null;
                        }
                        else
                        {
                            aux = padre.izquierdo;
                            padre.izquierdo = padre.izquierdo.derecho;
                            aux = null;
                        }

                    }
                    else if (padre.izquierdo.derecho == null)
                    {
                        aux = padre.izquierdo;
                        padre.izquierdo = padre.izquierdo.izquierdo;
                        aux = null;
                    }
                    else
                    {
                        aux = EncontrarMasDerechadeIzquierdo(padre.izquierdo.izquierdo);
                        nodo = BuscarPadre(raiz, aux.dato);
                        if (nodo.CompareTo(padre.izquierdo.dato) != 0)
                        {
                            nodo.derecho = null;
                            aux.izquierdo = padre.izquierdo.izquierdo;
                            aux.derecho = padre.izquierdo.derecho;
                            padre.izquierdo = null;
                            padre.izquierdo = aux;
                        }
                        else
                        {
                            aux.derecho = padre.izquierdo.derecho;
                            padre.izquierdo = null;
                            padre.izquierdo = aux;
                        }
                    }

                }
            }
            ActualizarFactoresEquilibrio(raiz, 0, 0, 0);
            Balancear(raiz);

        }

        public void Balancear(Nodo<T> actual)
        {
            if(!(actual.izquierdo == null && actual.derecho == null))
            {
                if(actual.izquierdo != null)
                {
                    Balancear(actual.izquierdo);
                }
                if(actual.derecho != null)
                {
                    Balancear(actual.derecho);
                }
                if(Math.Abs(ObtenerFactorEquilibrio(actual.derecho) - ObtenerFactorEquilibrio(actual.izquierdo)) == 2)
                {
                    if (ObtenerFactorEquilibrio(actual.derecho) - ObtenerFactorEquilibrio(actual.izquierdo) == -2)
                    {
                        if (!((ObtenerFactorEquilibrio(actual.izquierdo.derecho)- ObtenerFactorEquilibrio(actual.izquierdo.izquierdo)) == 1))
                        {
                            Nodo<T> Padre = BuscarPadre(raiz, actual.dato);
                            if (Padre == null)
                            {
                                raiz = RotacionalaDerecha(actual);
                            }

                            else
                            {
                                if (Padre.derecho.CompareTo(actual.dato) == 0)
                                {
                                    Padre.derecho = RotacionalaDerecha(actual);
                                }
                                else
                                {
                                    Padre.izquierdo = RotacionalaDerecha(actual);
                                }

                            }
                        }
                        else
                        {
                            Nodo<T> Padre = BuscarPadre(raiz, actual.dato);
                            if (Padre == null)
                            {
                                raiz = RotacionDoblealaDerecha(actual);
                            }

                            else
                            {
                                if (Padre.derecho.CompareTo(actual.dato) == 0)
                                {
                                    Padre.derecho = RotacionDoblealaDerecha(actual);
                                }
                                else
                                {
                                    Padre.izquierdo = RotacionDoblealaDerecha(actual);
                                }

                            }
                        }
                    }
                    if (ObtenerFactorEquilibrio(actual.derecho) - ObtenerFactorEquilibrio(actual.izquierdo) == 2)
                    {
                        if (!((ObtenerFactorEquilibrio(actual.derecho.derecho) - ObtenerFactorEquilibrio(actual.derecho.izquierdo)) == -1))
                        {
                            Nodo<T> Padre = BuscarPadre(raiz, actual.dato);
                            if (Padre == null)
                            {
                                raiz = RotacionalaIzquierda(actual);
                            }
                            else
                            {
                                if (Padre.derecho.CompareTo(actual.dato) == 0)
                                {
                                    Padre.derecho = RotacionalaIzquierda(actual);
                                }
                                else
                                {
                                    Padre.izquierdo = RotacionalaIzquierda(actual);
                                }

                            }
                        }
                        else
                        {
                            Nodo<T> Padre = BuscarPadre(raiz, actual.dato);
                            if (Padre == null)
                            {
                                raiz = RotacionDoblealaIzquierda(actual);
                            }
                            else
                            {
                                if (Padre.derecho.CompareTo(actual.dato) == 0)
                                {
                                    Padre.derecho = RotacionDoblealaIzquierda(actual);
                                }
                                else
                                {
                                    Padre.izquierdo = RotacionDoblealaIzquierda(actual);
                                }

                            }
                        }
                    }
                }
                ActualizarFactoresEquilibrio(raiz, 0, 0, 0);
            }
        }

        public Nodo<T> EncontrarMasDerechadeIzquierdo(Nodo<T> subarbol)
        {
            if(subarbol.derecho == null)
            {
                return subarbol;
            }
            else
            {
                return EncontrarMasDerechadeIzquierdo(subarbol.derecho);
            }
        }
        public int ActualizarFactoresEquilibrio(Nodo<T> actual, int contador, int altura, int aux)
        {
            aux = contador;
            if(!(actual.izquierdo == null && actual.derecho == null))
            {
                if(actual. izquierdo != null)
                {
                    contador = ActualizarFactoresEquilibrio(actual.izquierdo, (contador+1), (altura+1), 0);
                }
                if(actual.derecho != null)
                {
                    if(contador<ActualizarFactoresEquilibrio(actual.derecho, (aux+1), (altura + 1), 0))
                    {
                        contador = ActualizarFactoresEquilibrio(actual.derecho, (aux+1), (altura + 1), 0);
                    }
                }
                actual.factorEquilibrio = contador - altura;
                return contador;
                    }
            else
            {
                actual.factorEquilibrio = contador - altura;
                return contador;
            }
        }

        public Nodo<T> BuscarPadre(Nodo<T> actual, T dato)
        {
            Nodo<T> padre = actual;
            if (actual.CompareTo(dato) > 0)
            {
                if (actual.izquierdo != null)
                {
                    if (actual.izquierdo.CompareTo(dato) != 0)
                       padre = BuscarPadre(actual.izquierdo, dato);
                }
                else
                {
                    padre = null;
                }
            }
            else if (actual.CompareTo(dato) < 0)
            {
                if (actual.derecho != null)
                {
                    if (actual.derecho.CompareTo(dato) != 0)
                        padre = BuscarPadre(actual.derecho, dato);
                }
                else
                {
                    padre = null;
                }
            }
            else
                padre = null;
            return padre;
        }

        public Nodo<T> Buscar(T dato, Nodo<T> raiz)
        {
            if (this.raiz == null)
            {
                return null;
            }
            else if (raiz.CompareTo(dato) == 0)
            {
                return raiz;
            }
            else if (raiz.CompareTo(dato) < 0)
            {
                return Buscar(dato, raiz.derecho);
            }
            else
            {
                return Buscar(dato, raiz.izquierdo);
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


        public Nodo<T> RotacionalaDerecha(Nodo<T> nodo)
        {
            Nodo<T> aux = nodo.izquierdo;
            nodo.izquierdo = aux.derecho;
            aux.derecho = nodo;
            nodo.factorEquilibrio = Math.Max(ObtenerFactorEquilibrio(nodo.izquierdo), ObtenerFactorEquilibrio(nodo.derecho)) + 1;
            aux.factorEquilibrio = Math.Max(ObtenerFactorEquilibrio(aux.izquierdo), ObtenerFactorEquilibrio(aux.derecho)) + 1;
            logWriter("SE BALANCEÓ EL ÁRBOL CON UNA ROTACIÓN A LA DERECHA");
            return aux;
        }


        public Nodo<T> RotacionalaIzquierda(Nodo<T> nodo)
        {
            Nodo<T> aux = nodo.derecho;
            nodo.derecho = aux.izquierdo;
            aux.izquierdo = nodo;
            nodo.factorEquilibrio = Math.Max(ObtenerFactorEquilibrio(nodo.izquierdo), ObtenerFactorEquilibrio(nodo.derecho)) + 1;
            aux.factorEquilibrio = Math.Max(ObtenerFactorEquilibrio(aux.izquierdo), ObtenerFactorEquilibrio(aux.derecho)) + 1;
            logWriter("SE BALANCEÓ EL ÁRBOL CON UNA ROTACIÓN A LA IZQUIERDA");
            return aux;
        }


        public Nodo<T> RotacionDoblealaDerecha(Nodo<T> nodo)
        {
            logWriter("SE BALANCEÓ EL ÁRBOL CON UNA ROTACIÓN DOBLE A LA DERECHA");
            Nodo<T> temp;
            nodo.izquierdo = RotacionalaIzquierda(nodo.izquierdo);
            temp = RotacionalaDerecha(nodo);
            return temp;
        }


        public Nodo<T> RotacionDoblealaIzquierda(Nodo<T> nodo)
        {
            logWriter("SE BALANCEÓ EL ÁRBOL CON UNA ROTACIÓN DOBLE A LA IZQUIERDA");
            Nodo<T> temp;
            nodo.derecho = RotacionalaDerecha(nodo.derecho);
            temp = RotacionalaIzquierda(nodo);
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
                            nuevoPadre = RotacionalaDerecha(subArbol);
                        }
                        else
                        {
                            nuevoPadre = RotacionDoblealaDerecha(subArbol);

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
                            nuevoPadre = RotacionalaIzquierda(subArbol);
                        }
                        else
                        {
                            nuevoPadre = RotacionDoblealaIzquierda(subArbol);
                        }
                    }
                }
            }
            else
            {
                return subArbol;
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

        /// <summary>
        /// Metodo recorrer InOrden
        /// </summary>
        /// <param name="recorrido"></param>
        public List<T> EnOrden()
        {
            lista = new List<T>();
            RecorridoEnOrdenInterno(raiz);
            return lista;
        }

        /// <summary>
        /// Metodo recorrer PreOrden
        /// </summary>
        /// <param name="recorrido"></param>
        public List<T> PreOrden()
        {
            lista = new List<T>();
            RecorridoPreOrdenInterno(raiz);
            return lista;
        }

        /// <summary>
        /// Metodo recorrer PostOrden
        /// </summary>
        /// <param name="recorrido"></param>
        public List<T> PostOrden()
        {
            lista = new List<T>();
            RecorridoPostOrdenInterno(raiz);
            return lista;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="recorrido"></param>
        /// <param name="actual"></param>
        private void RecorridoEnOrdenInterno(Nodo<T> actual)
        {
            if (actual != null)
            {
                RecorridoEnOrdenInterno(actual.izquierdo);
                lista.Add(actual.dato);
                RecorridoEnOrdenInterno(actual.derecho);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="recorrido"></param>
        /// <param name="actual"></param>
        private void RecorridoPostOrdenInterno(Nodo<T> actual)
        {
            if (actual != null)
            {
                RecorridoPostOrdenInterno(actual.izquierdo);
                RecorridoPostOrdenInterno(actual.derecho);
                lista.Add(actual.dato);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="recorrido"></param>
        /// <param name="actual"></param>
        private void RecorridoPreOrdenInterno(Nodo<T> actual)
        {
            if (actual != null)
            {
                lista.Add(actual.dato);
                RecorridoPreOrdenInterno(actual.izquierdo);
                RecorridoPreOrdenInterno(actual.derecho);
            }
        }

        public Nodo<T> ObtenerRaiz()
        {
            return raiz;
        }
    }
}
