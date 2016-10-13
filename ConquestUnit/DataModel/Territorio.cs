using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        //public Windows.UI.Xaml.Shapes.Path[] Fronteras { get; set; }
        public List<TerritorioFrontera> TerritoriosFronteras { get; set; }//Aun no se utiliza esta propiedad

        public string IpJugadorPropietario { get; set; }
        public string NombreJugadorPropietario { get; set; }
    }
}
