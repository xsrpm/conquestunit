using ConquestUnit.Model;
using DataAccess.Model;
using SynapseSDK;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Util;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Foundation.Metadata;
using Windows.Networking;
using Windows.Storage.Streams;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace ConquestUnit.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MesaEnEspera : Page
    {
        //MainCore objSDK;
        Mesa objMesa;

        public MesaEnEspera()
        {
            this.InitializeComponent();
            btnJugar.IsEnabled = false;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            IniciarSDK();
        }

        private void btnJugar_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Helper.MensajeOk("El juego se iniciará...!!!");
        }

        private async void btnRegresar_Tapped(object sender, TappedRoutedEventArgs e)
        {
            //Notificar a los dispositivos que se está cerrando la mesa
            foreach (var item in objMesa.JugadoresConectados)
                await App.objSDK.UnicastPing(new HostName(item.Ip),
                            Constantes.MesaIndicaSeCierra + Constantes.SEPARADOR);

            this.Frame.Navigate(typeof(SeleccionarRol));
        }

        private async void MiMetodoReceptorMesaEsperaHelper(string strIp, string strMensaje)
        {
            try
            {
                if (!string.IsNullOrEmpty(strIp) && !string.IsNullOrEmpty(strMensaje))
                {
                    #region Jugador se une
                    if (strMensaje.Trim().Contains(Constantes.UnirseEnviameConfirmacion))
                    {
                        // Dispositivo se está agregando a la sala
                        var mensaje = strMensaje.Split(new string[] { Constantes.SEPARADOR }, StringSplitOptions.None);
                        //mensaje[0] => Acción (UnirseEnviameConfirmacion)
                        //mensaje[1] => mesaSeleccionada
                        //mensaje[2] => objJugador.Ip
                        //mensaje[3] => objJugador.Nombre
                        //mensaje[4] => strBytes (imagen del jugador)
                        if (mensaje.Length != 5)
                            return;

                        //Verificar que es la mesa seleccionada
                        if (mensaje[1] != objMesa.MesaID)
                            return;

                        //Verificar que la mesa no esté llena
                        if (objMesa.JugadoresConectados.Count >= 4)
                            return;

                        //Notificar al nuevo jugador que se ha unido a la mesa
                        await App.objSDK.UnicastPing(new HostName(mensaje[2]),
                            Constantes.ConfirmacionUnirseMesa + Constantes.SEPARADOR +
                            objMesa.Ip + Constantes.SEPARADOR +
                            objMesa.MesaID);

                        //Agregar al jugador a la mesa
                        var jugador = new Jugador();
                        jugador.Ip = mensaje[2];
                        jugador.Nombre = mensaje[3];
                        if (!mensaje[4].Equals(Constantes.SIN_IMAGEN))
                            jugador.Imagen = Convert.FromBase64String(mensaje[4]);

                        //Validar jugador no se ha unido aún
                        if (!ValidaJugadorExistexIP(jugador.Ip))
                            objMesa.JugadoresConectados.Add(jugador);

                        //Mostrar datos de los jugadores en pantalla
                        MostrarDatosJugadoresEnPantalla();
                    }
                    #endregion
                    #region Jugador se retira
                    else if (strMensaje.Trim().Contains(Constantes.JugadorSaleMesa))
                    {
                        // Jugador se retira de la mesa estando en la pantalla de espera de inicio de juego
                        var mensaje = strMensaje.Split(new string[] { Constantes.SEPARADOR }, StringSplitOptions.None);
                        //mensaje[0] => Acción (JugadorSaleMesa)
                        //mensaje[1] => objJugador.Ip
                        if (mensaje.Length != 2)
                            return;

                        //Eliminar al jugador de la mesa
                        var ipJugadorRetirado = mensaje[1];
                        for (int i = 0; i < objMesa.JugadoresConectados.Count; i++)
                        {
                            if (objMesa.JugadoresConectados[i].Ip.Equals(ipJugadorRetirado))
                            {
                                objMesa.JugadoresConectados.RemoveAt(i);
                                break;
                            }
                        }
                        
                        //Mostrar datos de los jugadores en pantalla
                        MostrarDatosJugadoresEnPantalla();
                    }
                    #endregion
                }
                //Habilitar o deshabilitar el boton de Jugar
                btnJugar.IsEnabled = (objMesa.JugadoresConectados.Count >= 2 ? true : false);
            }
            catch (Exception ex)
            {
                Helper.MensajeOk(ex.Message);
            }
        }

        private async void MostrarDatosJugadoresEnPantalla()
        {
            Uri uri = new Uri("ms-appx:///Assets/Images/user128x128.png");
            BitmapImage defaultImage = new BitmapImage(uri);
            Jugador jugador;
            if (objMesa.JugadoresConectados.Count >= 1)
            {
                jugador = objMesa.JugadoresConectados[0];
                if (!lblIpJugador1.Text.Equals(jugador.Ip))
                {
                    lblIpJugador1.Text = jugador.Ip;
                    if (jugador.Imagen != null)
                    {
                        BitmapImage bimgBitmapImage = new BitmapImage();
                        IRandomAccessStream fileStream = await Convertidor.ConvertImageToStream(jugador.Imagen);
                        bimgBitmapImage.SetSource(fileStream);
                        imgJugador1.Source = bimgBitmapImage;
                    }
                    lblJugador1.Text = jugador.Nombre;
                }
            }
            else
            {
                lblIpJugador1.Text = "0.0.0.0";
                imgJugador1.Source = defaultImage;
                lblJugador1.Text = "Esperando...";
            }
            if (objMesa.JugadoresConectados.Count >= 2)
            {
                jugador = objMesa.JugadoresConectados[1];
                if (!lblIpJugador2.Text.Equals(jugador.Ip))
                {
                    lblIpJugador2.Text = jugador.Ip;
                    if (jugador.Imagen != null)
                    {
                        BitmapImage bimgBitmapImage = new BitmapImage();
                        IRandomAccessStream fileStream = await Convertidor.ConvertImageToStream(jugador.Imagen);
                        bimgBitmapImage.SetSource(fileStream);
                        imgJugador2.Source = bimgBitmapImage;
                    }
                    lblJugador2.Text = jugador.Nombre;
                }
            }
            else
            {
                lblIpJugador2.Text = "0.0.0.0";
                imgJugador2.Source = defaultImage;
                lblJugador2.Text = "Esperando...";
            }
            if (objMesa.JugadoresConectados.Count >= 3)
            {
                jugador = objMesa.JugadoresConectados[2];
                if (!lblIpJugador3.Text.Equals(jugador.Ip))
                {
                    lblIpJugador3.Text = jugador.Ip;
                    if (jugador.Imagen != null)
                    {
                        BitmapImage bimgBitmapImage = new BitmapImage();
                        IRandomAccessStream fileStream = await Convertidor.ConvertImageToStream(jugador.Imagen);
                        bimgBitmapImage.SetSource(fileStream);
                        imgJugador3.Source = bimgBitmapImage;
                    }
                    lblJugador3.Text = jugador.Nombre;
                }
            }
            else
            {
                lblIpJugador3.Text = "0.0.0.0";
                imgJugador3.Source = defaultImage;
                lblJugador3.Text = "Esperando...";
            }
            if (objMesa.JugadoresConectados.Count >= 4)
            {
                jugador = objMesa.JugadoresConectados[3];
                if (!lblIpJugador4.Text.Equals(jugador.Ip))
                {
                    lblIpJugador4.Text = jugador.Ip;
                    if (jugador.Imagen != null)
                    {
                        BitmapImage bimgBitmapImage = new BitmapImage();
                        IRandomAccessStream fileStream = await Convertidor.ConvertImageToStream(jugador.Imagen);
                        bimgBitmapImage.SetSource(fileStream);
                        imgJugador4.Source = bimgBitmapImage;
                    }
                    lblJugador4.Text = jugador.Nombre;
                }
            }
            else
            {
                lblIpJugador4.Text = "0.0.0.0";
                imgJugador4.Source = defaultImage;
                lblJugador4.Text = "Esperando...";
            }
        }

        private bool ValidaJugadorExistexIP(string strIp)
        {
            try
            {
                foreach (var objJugador in objMesa.JugadoresConectados)
                {
                    if (objJugador.Ip.Equals(strIp))
                        return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Helper.MensajeOk(ex.Message);
                return false;
            }
        }

        #region Conexion SynapseSDK
        private void IniciarSDK()
        {
            try
            {
                App.UIDispatcher = this.Dispatcher;
                App.objSDK = MainCore.getInstance(Constantes.MULTICAST_ADDRESS, Constantes.MULTICAST_SERVICE_PORT, Constantes.UNICAST_SERVICE_PORT, Constantes.STREAM_SERVICE_PORT, MiMetodoReceptorMesaEspera, Constantes.DELAY);
                int cont = 0;
                while (!App.objSDK.SocketIsConnected && cont < 3)
                {
                    App.objSDK.TearDownSockets();
                    App.objSDK.InitializeSockets();
                    cont++;
                }

                if (App.objSDK.SocketIsConnected)
                {
                    objMesa = new Mesa();
                    objMesa.Ip = App.objSDK.MyIP.ToString();
                    //GENERAR NUMERO UNICO DE LA MESA
                    var numeroMesa = App.objSDK.MyIP.ToString().Split('.');
                    if (numeroMesa.Length == 4)
                    {
                        objMesa.MesaID = numeroMesa[2] + numeroMesa[3];
                        lblCodigoMesa.Text = objMesa.MesaID;
                    }
                    App.objSDK.setObjMetodoReceptorString = MiMetodoReceptorMesaEspera;
                }
                else
                {
                    //No hay conexión
                    string strMensaje = "Lo sentimos, no se pudo establecer la conexión vía Wi-Fi. Intente nuevamente.";
                    Helper.MensajeOk(strMensaje);
                    this.Frame.Navigate(typeof(SeleccionarRol));
                    return;
                }
            }
            catch (Exception ex)
            {
                Helper.MensajeOk(ex.Message);
            }
        }

        public async void MiMetodoReceptorMesaEspera(string strIp, string strMessage)
        {
            try
            {
                await App.UIDispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    MiMetodoReceptorMesaEsperaHelper(strIp, strMessage);
                });
            }
            catch (Exception ex)
            {
                Helper.MensajeOk(ex.Message);
            }
        }
        #endregion
    }
}
