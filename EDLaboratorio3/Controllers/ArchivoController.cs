using EDLaboratorio3.Models;
using LibreriaDeClases;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EDLaboratorio3.Controllers
{
    public class ArchivoController : Controller
    {
        // GET: Archivo
        public ActionResult Index()
        {
            return View();
        }

        // GET: Archivo/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Archivo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Archivo/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Archivo/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Archivo/Edit/5
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

        // GET: Archivo/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Archivo/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult CargaArchivoFecha()
        {
            return View();
        }

        //Post SubirArchivoPaises
        [HttpPost]
        public ActionResult CargaArchivoFecha(HttpPostedFileBase file)
        {
            string filePath = string.Empty;
            Archivo modelo = new Archivo();
            if (file != null)
            {
                string ruta = Server.MapPath("~/Temp/");

                if (!Directory.Exists(ruta))
                {
                    Directory.CreateDirectory(ruta);
                }

                filePath = ruta + Path.GetFileName(file.FileName);

                string extension = Path.GetExtension(file.FileName);

                file.SaveAs(filePath);

                using (StreamReader r = new StreamReader(filePath))
                {
                    string json = r.ReadToEnd();
                    dynamic array = JsonConvert.DeserializeObject(json);
                    RecorridoPreOrdenInternoPartidosPorFecha(array);

                }


                modelo.SubirArchivo(ruta, file);

            }
            ViewBag.Error = modelo.error;
            ViewBag.Correcto = modelo.Confirmacion;

            return View(DBContext.DefaultConnection.miAVLFechas.EnOrden());
        }

        private void RecorridoPreOrdenInternoPartidosPorFecha(dynamic actual)
        {

            if (actual != null)
            {
                var item = actual.valor;
                Partido temp = new Partido();
                temp.NoPartido = item.NoPartido;
                temp.FechaPartido = item.FechaPartido;
                temp.Grupo = item.Grupo;
                temp.Pais1 = item.Pais1;
                temp.Pais2 = item.Pais2;
                temp.Estadio = item.Estadio;
                Nodo<Partido> n = new Nodo<Partido>(temp, CompararFechas);
                DBContext.DefaultConnection.miAVLFechas.Insertar(n);
                RecorridoPreOrdenInternoPartidosPorFecha(JsonConvert.DeserializeObject(Convert.ToString(actual.izquierdo)));
                RecorridoPreOrdenInternoPartidosPorFecha(JsonConvert.DeserializeObject(Convert.ToString(actual.derecho)));
            }



        }

        public static int CompararFechas(Partido actual, Partido nuevo)
        {
            if (DateTime.Compare(actual.FechaPartido, nuevo.FechaPartido) == 1)
                return 1;
            else if (DateTime.Compare(actual.FechaPartido, nuevo.FechaPartido) == -1)
                return -1;
            else
                return 0;
        }

        public ActionResult SubirArchivoPartidosPorNoPartido()
        {
            return View();
        }

        //Post SubirArchivoPaises
        [HttpPost]
        public ActionResult SubirArchivoPartidosPorNoPartido(HttpPostedFileBase file)
        {
            string filePath = string.Empty;
            Archivo modelo = new Archivo();
            if (file != null)
            {
                string ruta = Server.MapPath("~/Temp/");

                if (!Directory.Exists(ruta))
                {
                    Directory.CreateDirectory(ruta);
                }

                filePath = ruta + Path.GetFileName(file.FileName);

                string extension = Path.GetExtension(file.FileName);

                file.SaveAs(filePath);

                using (StreamReader r = new StreamReader(filePath))
                {
                    string json = r.ReadToEnd();
                    dynamic array = JsonConvert.DeserializeObject(json);
                    RecorridoPreOrdenInternoPartidosPorNoPartido(array);

                }


                modelo.SubirArchivo(ruta, file);

            }
            ViewBag.Error = modelo.error;
            ViewBag.Correcto = modelo.Confirmacion;

            return View(DBContext.DefaultConnection.miAVLNoPartidos.EnOrden());
        }

        private void RecorridoPreOrdenInternoPartidosPorNoPartido(dynamic actual)
        {

            if (actual != null)
            {
                var item = actual.valor;
                Partido temp = new Partido();
                temp.NoPartido = item.NoPartido;
                temp.FechaPartido = item.FechaPartido;
                temp.Grupo = item.Grupo;
                temp.Pais1 = item.Pais1;
                temp.Pais2 = item.Pais2;
                temp.Estadio = item.Estadio;
                Nodo<Partido> n = new Nodo<Partido>(temp, CompararNoPartido);
                DBContext.DefaultConnection.miAVLNoPartidos.Insertar(n);
                RecorridoPreOrdenInternoPartidosPorNoPartido(JsonConvert.DeserializeObject(Convert.ToString(actual.izquierdo)));
                RecorridoPreOrdenInternoPartidosPorNoPartido(JsonConvert.DeserializeObject(Convert.ToString(actual.derecho)));
            }



        }

        public static int CompararNoPartido(Partido actual, Partido nuevo)
        {
            if (actual.NoPartido > nuevo.NoPartido)
                return 1;
            else if (actual.NoPartido < nuevo.NoPartido)
                return -1;
            else
                return 0;
        }
    }
}
