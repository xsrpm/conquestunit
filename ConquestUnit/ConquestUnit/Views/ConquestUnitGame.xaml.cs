using DataModel;
using System;
using System.Linq;
using Util;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace ConquestUnit.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ConquestUnitGame : Page
    {
        Windows.UI.Xaml.Shapes.Path[,] Territorio;
        Windows.UI.Xaml.Shapes.Path TerrSelec;

        Juego objJuego;

        Windows.UI.Xaml.Visibility[,] BlancoGridVisibilidad;

        public ConquestUnitGame()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            objJuego = new Juego(Constantes.MAPA_CHINA);
            // INCIALIZACION DE PRUEBA --- el objeto juego viene de parametro "e"
            var jugador1 = new Jugador() { Conectado = true, Ip = "192.168.0.4", Nombre = "Roy" };
            var jugador2 = new Jugador() { Conectado = true, Ip = "192.168.0.6", Nombre = "Cesar" };
            var jugador3 = new Jugador() { Conectado = true, Ip = "192.168.0.8", Nombre = "Joel" };
            var jugador4 = new Jugador() { Conectado = true, Ip = "192.168.0.9", Nombre = "Christian" };
            objJuego.JugadoresConectados.Add(jugador1);
            objJuego.JugadoresConectados.Add(jugador2);
            objJuego.JugadoresConectados.Add(jugador3);
            objJuego.JugadoresConectados.Add(jugador4);
            // Definir los turnos de los jugadores,
            // los cuales será igual a la lista de jugadores de la mesa
            // pero en desorden
            GameLogic.LogicaInicio.InicializarTurnos(objJuego);
            // Repartir los territorios
            GameLogic.LogicaInicio.InicializarTerritorios(objJuego);
            GameLogic.LogicaInicio.RepartirTerritorio(objJuego);
            GameLogic.LogicaInicio.RepartirUnidadesEnTerritorios(objJuego);

            Inicializar();
        }

        public void Inicializar()
        {
            Territorio = new Windows.UI.Xaml.Shapes.Path[24, 4]
            {
                {Uliassutai,Qinghai,Tibet,null },{Huijiang,Sichuan,Yunnan,null },{Gansu,Sichuan,Tibet,Huijiang},
                {Gansu,Hubei,Yunnan,Tibet},{Sichuan,Guizhou,null,null },{Sichuan,Hunan,Guangxi,Yunnan },

                {null,Heilongjiang,Mongolia,Huijiang },{Uliassutai,Jilin,Gansu,Huijiang },{Mongolia,Shaanxi,Sichuan,Qinghai},
                {Mongolia,Shanxi,Sichuan,Gansu },{Mongolia,Zhili,Menan,Shaanxi },{Menan,Anhu,Hunan,Sichuan },

                {null,Jilin,Mongolia,Uliassutai },{Heilongjiang,null,Shengjing,Mongolia },{Jilin,Mongolia,Zhili,Zhili },
                {Mongolia,Shengjing,Shandong,Shanxi },{Zhili,null,Menan,Zhili },{Zhili,Shandong,Hubei,Shaanxi },

                {Guizhou,Hunan,null,Yunnan},{Hubei,Ilangxi,Guangxi,Guizhou },{Anhu,Fcohou,null,Hunan },
                {Zhelang,null,null,Ilangxi },{Anhu,null,Fcohou,Anhu },{Menan,Zhelang,Ilangxi,Hubei }
            };

            //Dibujar unidades y Cantidad de unidades
            for (int i = 0; i < objJuego.Territorios.Count; i++)
            {
                DibujarUnidadesTerritorioEnElMapa(objJuego.Territorios[i]);
            }
            ActualizarNumeroUnidadesInfo();

            objJuego.FaseActual = Constantes.FaseJuego.DESPLIEGUE;
            objJuego.AccionActual = Constantes.AccionJuego.DESPLEGAR;

            //blanco,equis,circulo,arriba,abajo
            BlancoGridVisibilidad = new Windows.UI.Xaml.Visibility[9, 5] {
                {0,0,Visibility.Collapsed,Visibility.Collapsed,Visibility.Collapsed},
                {0,0,Visibility.Collapsed,Visibility.Collapsed,Visibility.Collapsed},
                {0,0,0,Visibility.Collapsed,Visibility.Collapsed},
                {0,0,0,0,0},
                {Visibility.Collapsed,Visibility.Collapsed,Visibility.Collapsed,Visibility.Collapsed,Visibility.Collapsed},
                {0,0,Visibility.Collapsed,0,0},
                {0,0,Visibility.Collapsed,Visibility.Collapsed,Visibility.Collapsed},
                {0,0,0,Visibility.Collapsed,Visibility.Collapsed},
                {0,0,0,0,0}
            };
            Seleccionar_Territorio(Huijiang);
        }

        private void MoverTerritorio(int direccion)
        {
            Windows.UI.Xaml.Shapes.Path Actual = TerrSelec;
            Windows.UI.Xaml.Shapes.Path Nuevo;
            Nuevo = Territorio[Convert.ToInt32(TerrSelec.Tag), direccion];
            if (Nuevo != null)
            {
                Seleccionar_Territorio(Nuevo);
            }
        }

        private void BtnArr_Tapped(object sender, TappedRoutedEventArgs e)
        {
            MoverTerritorio(Constantes.MoverTerritorio.ARRIBA);
        }

        private void BtnIzq_Tapped(object sender, TappedRoutedEventArgs e)
        {
            MoverTerritorio(Constantes.MoverTerritorio.IZQUIERDA);
        }

        private void BtnDer_Tapped(object sender, TappedRoutedEventArgs e)
        {
            MoverTerritorio(Constantes.MoverTerritorio.DERECHA);
        }

        private void BtnAba_Tapped(object sender, TappedRoutedEventArgs e)
        {
            MoverTerritorio(Constantes.MoverTerritorio.ABAJO);
        }

        private void Seleccionar_Territorio(Windows.UI.Xaml.Shapes.Path territorio)
        {
            TerrSelec = territorio;
            Thickness Centro_TerrSelec = new Thickness
            (
                territorio.Margin.Left + territorio.Width / 2 - BlancoGrid.Width / 2,
                territorio.Margin.Top + territorio.Height / 2 - BlancoGrid.Height / 2,
                0,
                0
            );

            BlancoGrid.Margin = Centro_TerrSelec;
            MostrarBlanco();


            //Actual.Opacity = 0.5;
            //TerrSelec.Opacity = 1;
        }

        private void CambiarFase(int fase)
        {

        }

        public void MostrarBlanco()
        {
            Blanco.Visibility = BlancoGridVisibilidad[objJuego.FaseActual, 0];
            XButtonGrid.Visibility = BlancoGridVisibilidad[objJuego.FaseActual, 1];
            OButtonGrid.Visibility = BlancoGridVisibilidad[objJuego.FaseActual, 2];
            DownArrowGrid.Visibility = BlancoGridVisibilidad[objJuego.FaseActual, 3];
            UpArrowGrid.Visibility = BlancoGridVisibilidad[objJuego.FaseActual, 4];
        }

        private void XButtonGrid_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }

        public void DibujarUnidadesTerritorioEnElMapa(Territorio territorio)
        {
            try
            {
                Uri uri = new Uri(territorio.ImagenUnidades);
                BitmapImage imagenUnidad = new BitmapImage(uri);

                Image image = (Image)this.FindName("Unidad" + territorio.NombreTerritorio);
                Ellipse elipse = (Ellipse)this.FindName("UnidadElipse" + territorio.NombreTerritorio);
                TextBlock texto = (TextBlock)this.FindName("UnidadCantidad" + territorio.NombreTerritorio);

                image.Source = imagenUnidad;
                elipse.Fill = Convertidor.GetSolidColorBrush(territorio.ColorUnidades);
                texto.Text = territorio.NUnidadesDeplegadas.ToString();
            }
            catch (Exception ex)
            {
                Helper.MensajeOk(ex.Message);
                throw;
            }
        }

        public void ActualizarNumeroUnidadesInfo()
        {
            for (int i = 0; i < objJuego.JugadoresConectados.Count; i++)
            {
                ((TextBlock)this.FindName("UnidadCantidad" + (i + 1))).Text =
                    objJuego.Territorios.Where(x => x.IpJugadorPropietario.Equals(objJuego.JugadoresConectados[i].Ip)).Sum(x => x.NUnidadesDeplegadas).ToString();
            }
        }

        private void BotonA_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }

        private void BotonB_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }

        private void BotonY_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }

        private void BotonX_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (objJuego.AccionActual == Constantes.AccionJuego.DESPLEGAR)
            {
                objJuego.Territorios[Convert.ToInt32(TerrSelec.Tag)].NUnidadesDeplegadas += 1;
                ((TextBlock)this.FindName("UnidadCantidad" + objJuego.Territorios[Convert.ToInt32(TerrSelec.Tag)].NombreTerritorio)).Text =
                    objJuego.Territorios[Convert.ToInt32(TerrSelec.Tag)].NUnidadesDeplegadas.ToString();
                ActualizarNumeroUnidadesInfo();
            }
        }
    }
}
