using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class Jugador
    {
        public string Nombre { get; set; }
        public byte[] Imagen { get; set; }
        public string Ip { get; set; }
        public string ImagenUnidades { get; set; }
        public string Color { get; set; }
        public string ColorInactivo { get; set; }
        //Para saber si el jugador está conectado
        public bool Conectado { get; set; }
        public string IpMesaConectada { get; set; }
        public Jugador()
        {
            Conectado = true;
        }
    }
}
