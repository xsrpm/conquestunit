using System.Collections.Generic;

namespace DataModel
{
    public class Territorio
    {
        public int TerritorioId { get; set; }
        public string NombreTerritorio { get; set; }
        public int NUnidadesDeplegadas { get; set; }
        public string ColorUnidades { get; set; }
        public string ImagenUnidades { get; set; }
        public int NroRegion { get; set; }

        public string IpJugadorPropietario { get; set; }
        public string NombreJugadorPropietario { get; set; }
    }
}
