using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EDLaboratorio3.DBContext;
using LibreriaDeClases;
using EDLaboratorio3.Models;
using System.IO;

namespace EDLaboratorio3.Controllers
{
    public class PartidoController : Controller
    {
        // GET: Partido
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult IndexFecha()
        {
            logWriter("VISITO INICIO DE FECHA", HomeController.ruta, true);
            

            return View(DBContext.DefaultConnection.miAVLFechas.EnOrden());
        }
        public ActionResult IndexNoPartido()
        {
            logWriter("VISITO INICIO DE NUMERO DE PARTIDO", HomeController.ruta, true);

            return View(DBContext.DefaultConnection.miAVLNoPartidos.EnOrden());
        }
        
        // GET: Partido/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Partido/CreateFecha
        public ActionResult CreateFecha()
        {  
            return View();
        }        

        // POST: Partido/CreateFecha
        [HttpPost]
        public ActionResult CreateFecha(Partido partido)
        {
            try
            {
                logWriter("VISITO CREAR", HomeController.ruta, true);
                Nodo<Partido> nuevo = new Nodo<Partido>(partido,ArchivoController.CompararFechas);
                DefaultConnection.miAVLFechas.Insertar(nuevo);
                return RedirectToAction("IndexFecha");
            }
            catch
            {
                return View("IndexFecha");
            }
        }

        // GET: Partido/CreateNoPartido
        public ActionResult CreateNoPartido()
        {
            return View();
        }

        // POST: Partido/CreateNoPartido
        [HttpPost]
        public ActionResult CreateNoPartido(Partido partido)
        {
            try
            {
                logWriter("VISITO CREAR", HomeController.ruta, true);
                Nodo<Partido> nuevo = new Nodo<Partido>(partido, ArchivoController.CompararNoPartido);
                DefaultConnection.miAVLNoPartidos.Insertar(nuevo);

                return RedirectToAction("IndexNoPartido");
            }
            catch
            {
                return View("IndexNoPartido");
            }
        }

        // GET: Partido/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Partido/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Partido/Delete/5
        public ActionResult DeleteNoPartido(int id)
        {
            logWriter("VISITO ELIMINAR", HomeController.ruta, true);

            return View(DefaultConnection.miAVLNoPartidos.EnOrden().Where(x => x.NoPartido == id).FirstOrDefault());
        }

        // POST: Partido/Delete/5
        [HttpPost]
        public ActionResult DeleteNoPartido(int id, FormCollection collection)
        {
            try
            {
                Partido partido = DefaultConnection.miAVLNoPartidos.EnOrden().Where(x => x.NoPartido == id).FirstOrDefault();
                DefaultConnection.miAVLNoPartidos.Eliminar(partido);
                return RedirectToAction("IndexNoPartido");
            }
            catch
            {
                return View();
            }
        }

        // GET: Partido/Delete/5
        public ActionResult DeleteFechas(int id)
        {
            logWriter("VISITO ELIMINAR", HomeController.ruta, true);

            return View(DefaultConnection.miAVLFechas.EnOrden().Where(x => x.NoPartido == id).FirstOrDefault());
        }

        // POST: Partido/Delete/5
        [HttpPost]
        public ActionResult DeleteFechas(int id, FormCollection collection)
        {
            try
            {
                Partido partido = DefaultConnection.miAVLFechas.EnOrden().Where(x => x.NoPartido == id).FirstOrDefault();
                DefaultConnection.miAVLFechas.Eliminar(partido);
                return RedirectToAction("IndexFecha");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult BusquedaFecha()
        {
            logWriter("VISITO BUSQUEDA POR FECHA", HomeController.ruta, true);

            return View(DefaultConnection.miBusquedaFecha.ToList());
        }

        public ActionResult BusquedaNoPartido()
        {
            logWriter("VISITO BUSQUEDA NUMERO DE PARTIDO", HomeController.ruta, true);

            return View(DefaultConnection.miBusquedaNoPartidos.ToList());
        }

        [HttpPost]
        public ActionResult BusquedaFecha(string date)
        {
            try
            {
                logWriter("VISITO BUSQUEDA POR FECHA", HomeController.ruta, true);

                Partido partidoBuscadoFecha = DefaultConnection.miAVLFechas.EnOrden().Find(x => x.FechaPartido == DateTime.Parse(date));

                if (partidoBuscadoFecha == null)
                {
                    return HttpNotFound();
                }

                DefaultConnection.miBusquedaFecha.Add(partidoBuscadoFecha);

                return RedirectToAction("BusquedaFecha");
            }
            catch
            {
                return View("BusquedaFecha");
            }
        }

        [HttpPost]
        public ActionResult BusquedaNoPartido(string NoPartido)
        {
            try
            {
                logWriter("VISITO BUSQUEDA NUMERO DE PARTIDO", HomeController.ruta, true);

                Partido partidoBuscadoNoPartido = DefaultConnection.miAVLNoPartidos.EnOrden().Find(x => x.NoPartido == int.Parse(NoPartido));
                if (partidoBuscadoNoPartido == null)

                {

                    return HttpNotFound();

                }
                DefaultConnection.miBusquedaNoPartidos.Add(partidoBuscadoNoPartido);
                return RedirectToAction("BusquedaNoPartido");
            }
            catch
            {
                return View("BusquedaNoPartido");
            }
        }

        public void logWriter(string contenido, string rutaArchivo, bool sobrescribir = true)
        {
            StreamWriter logReporter = new StreamWriter(rutaArchivo, !sobrescribir);
            logReporter.WriteLine(contenido + "; " + DateTime.Now);
            logReporter.Close();
        }


    }
}
