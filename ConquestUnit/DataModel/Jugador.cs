namespace DataModel
{
    public class Jugador
    {
        public string Nombre { get; set; }
        public byte[] Imagen { get; set; }
        public string Ip { get; set; }
        public string ImagenUnidad { get; set; }
        public string ImagenUnidadAgrupadora { get; set; }
        public string Color { get; set; }
        public string ColorPastel { get; set; }
        //Para saber si el jugador está conectado
        public bool Activo { get; set; }
        public string IpMesaConectada { get; set; }
        public Jugador()
        {
            Activo = true;
        }
    }
}
