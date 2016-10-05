﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class Juego
    {
        public int TurnoActual { get; set; }
        //public string FasedeJuegoActual;
        //public List<int> NFichasParaDesplegar;
        public int FaseActual { get; set; }

        public string JuegoID { get; set; }
        public string Ip { get; set; }
        public List<Jugador> JugadoresConectados { get; set; }
        public List<Territorio> Territorios { get; set; }
        public string TipoMapa { get; set; }

        public Juego(string tipoMapa)
        {
            JugadoresConectados = new List<Jugador>();
            TipoMapa = tipoMapa;
        }
    }
}
