using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace EDLaboratorio3.Controllers
{
    public class HomeController : Controller
    {
        public static string ruta = @"C:\Users\" + Environment.UserName + @"\Desktop\logs.txt";

        public ActionResult Index()
        {
            logWriter("VISITO INICIO", ruta, false);
            return View();
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
    }
}