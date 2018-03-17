using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EDLaboratorio3.DBContext;
using LibreriaDeClases;
using EDLaboratorio3.Models;


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
            HomeController.logWriter("VISITO INICIO DE FECHA", HomeController.ruta, true);

            return View(DBContext.DefaultConnection.miAVLFechas.EnOrden());
        }
        public ActionResult IndexNoPartido()
        {
            HomeController.logWriter("VISITO INICIO DE NUMERO DE PARTIDO", HomeController.ruta, true);

            return View(DBContext.DefaultConnection.miAVLNoPartidos.EnOrden());
        }
        // GET: Partido/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Partido/Create
        public ActionResult Create()
        {  
            return View();
        }

        // POST: Partido/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                HomeController.logWriter("VISITO CREAR", HomeController.ruta, true);


                return RedirectToAction("Index");
            }
            catch
            {
                return View();
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
        public ActionResult Delete(int id)
        {
            HomeController.logWriter("VISITO ELIMINAR", HomeController.ruta, true);

            return View();
        }

        // POST: Partido/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
