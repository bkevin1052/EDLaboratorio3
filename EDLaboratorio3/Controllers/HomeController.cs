using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using EDLaboratorio3.DBContext;
using EDLaboratorio3.Models;
using LibreriaDeClases;

namespace EDLaboratorio3.Controllers
{
    public class HomeController : Controller
    {
        DefaultConnection db = DefaultConnection.getInstance;
        public static string ruta = @"C:\Users\" + Environment.UserName + @"\Desktop\logs.txt";

        public ActionResult Index()
        {
            logWriter("VISITO INICIO", ruta, false);

            //PRUEBA DE INSERCIÔN
            Partido P2 = new Partido();
            Partido P3 = new Partido();

            P2.Pais1 = "Estados Unidos";
            P2.Pais2 = "Italia";
            P2.Estadio = "CAMP NOU";
            P2.NoPartido = 60;
            P2.FechaPartido = Convert.ToDateTime("3 / 14 / 2018 3:49:11 PM");
            P2.Grupo = "C";

            P3.Pais1 = "Mexico";
            P3.Pais2 = "Italia";
            P3.Estadio = "bERNABEO";
            P3.NoPartido = 70;
            P3.FechaPartido = Convert.ToDateTime("3 / 14 / 2018 3:49:11 PM");
            P3.Grupo = "B";

            //cREACION DE NODOS
            Nodo<Partido> n5 = new Nodo<Partido>(P2, CompararFecha);
            Nodo<Partido> n6 = new Nodo<Partido>(P3, CompararFecha);

            DefaultConnection.miAVLFechas.Insertar(n5);
            DefaultConnection.miAVLFechas.Insertar(n6);


            return View();
        }
        public static int CompararFecha(Partido actual, Partido nuevo)
        {
            if (actual.FechaPartido > nuevo.FechaPartido)
                return 1;
            else if (actual.FechaPartido < nuevo.FechaPartido)
                return -1;
            else
                return 0;
        }

        public static int CompararNumero(Partido actual, Partido nuevo)
        {
            if(actual.NoPartido > nuevo.NoPartido)
                return 1;
            else if(actual.NoPartido < nuevo.NoPartido)
                return -1;
            else
                return 0;
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            logWriter("VISITO ACERCA DE", ruta, false);

            return View();
        }

        public ActionResult Contact()
        {
            logWriter("VISITO CONTACTO", ruta, false);

            ViewBag.Message = "Your contact page.";

            return View();
        }

        public static void logWriter(string contenido, string rutaArchivo, bool sobrescribir = true)
        {
            StreamWriter logReporter = new StreamWriter(rutaArchivo, !sobrescribir);
            logReporter.WriteLine(contenido + "; " + DateTime.Now);
            logReporter.Close();
        }

        public ActionResult CheckRadio (FormCollection frm)
        {
            string llave = frm["Llave"].ToString();
            if(llave == "fecha")
            {
                return RedirectToAction("CargaArchivoFecha", "Archivo");
            }
            else
            {
                return RedirectToAction("CargaArchivoNoPartido", "Archivo");
            }
        }
    }
}