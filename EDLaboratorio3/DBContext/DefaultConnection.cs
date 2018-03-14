﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LibreriaDeClases.Clases;
using EDLaboratorio3.Models;

namespace EDLaboratorio3.DBContext
{
    public class DefaultConnection
    {
        private static volatile DefaultConnection Instance;
        private static object syncRoot = new Object();

        public ArbolAVL<Partido> miAVLFechas = new ArbolAVL<Partido>();
        public ArbolAVL<Partido> miAVLNoPartidos = new ArbolAVL<Partido>();


        public int IDActual { get; set; }

        private DefaultConnection()
        {
            IDActual = 0;
        }

        public static DefaultConnection getInstance
        {
            get
            {
                if (Instance == null)
                {
                    lock (syncRoot)
                    {
                        if (Instance == null)
                        {
                            Instance = new DefaultConnection();
                        }
                    }
                }
                return Instance;
            }
        }
    }
}