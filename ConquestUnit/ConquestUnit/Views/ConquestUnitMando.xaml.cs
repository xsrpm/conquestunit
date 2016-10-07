using SynapseSDK;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Util;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.Networking;
using Windows.UI.Core;
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
    public sealed partial class ConquestUnitMando : Page
    {
        public ConquestUnitMando()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            DisplayInformation.AutoRotationPreferences = DisplayOrientations.Landscape;
            //El jugador es primer turno
            if (e.Parameter != null)
            {
                var primerTurno = (bool)e.Parameter;
                if (primerTurno)
                {
                    Uri uri = new Uri("ms-appx:///Assets/Images/active32x32.png");
                    BitmapImage logoImage = new BitmapImage(uri);
                    imgActivo.Source = logoImage;
                    HabilitarControles();
                }
                else
                {
                    DeshabilitarControles();
                }
            }
            IniciarSDK();
        }

        private void MiMetodoReceptorJugadorJuegoHelper(string strIp, string strMensaje)
        {
            try
            {
                if (!string.IsNullOrEmpty(strIp) && !string.IsNullOrEmpty(strMensaje))
                {
                    #region La mesa indica habilitar los controles
                    if (strMensaje.Trim().Contains(Constantes.MesaConumicaHABILITARControles))
                    {
                        HabilitarControles();
                    }
                    #endregion
                    #region La mesa indica deshabilitar los controles
                    else if (strMensaje.Trim().Contains(Constantes.MesaConumicaDESHABILITARControles))
                    {
                        DeshabilitarControles();
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                Helper.MensajeOk(ex.Message);
                return;
            }
        }

        #region Conexion SynapseSDK
        private void IniciarSDK()
        {
            try
            {
                App.UIDispatcher = this.Dispatcher;
                App.objSDK = MainCore.getInstance(Constantes.MULTICAST_ADDRESS, Constantes.MULTICAST_SERVICE_PORT, Constantes.UNICAST_SERVICE_PORT, Constantes.STREAM_SERVICE_PORT, MiMetodoReceptorJugadorJuego, Constantes.DELAY);
                int cont = 0;
                while (!App.objSDK.SocketIsConnected && cont < 3)
                {
                    App.objSDK.TearDownSockets();
                    App.objSDK.InitializeSockets();
                    cont++;
                }

                if (App.objSDK.SocketIsConnected)
                {
                    App.objJugador.Ip = App.objSDK.MyIP.ToString();
                    App.objSDK.setObjMetodoReceptorString = MiMetodoReceptorJugadorJuego;
                }
                else
                {
                    //No hay conexión
                    string strMensaje = "Lo sentimos, no se pudo establecer la conexión vía Wi-Fi. Intente nuevamente.";
                    Helper.MensajeOk(strMensaje);
                    this.Frame.Navigate(typeof(ElegirMesa));
                    return;
                }
            }
            catch (Exception ex)
            {
                Helper.MensajeOk(ex.Message);
            }
        }

        public async void MiMetodoReceptorJugadorJuego(string strIp, string strMessage)
        {
            try
            {
                await App.UIDispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    MiMetodoReceptorJugadorJuegoHelper(strIp, strMessage);
                });
            }
            catch (Exception ex)
            {
                Helper.MensajeOk(ex.Message);
            }
        }
        #endregion

        public async void EnviarBotonPresionadoMesa(int botonPresionado)
        {
            await App.objSDK.UnicastPing(new HostName(App.objJugador.IpMesaConectada),
                Constantes.JugadorPresionaBoton + Constantes.SEPARADOR +
                App.objJugador.Ip + Constantes.SEPARADOR +
                botonPresionado);
        }

        private void btnArriba_Tapped(object sender, TappedRoutedEventArgs e)
        {
            EnviarBotonPresionadoMesa(Constantes.Controles.ARRIBA);
        }

        private void btnIzquierda_Tapped(object sender, TappedRoutedEventArgs e)
        {
            EnviarBotonPresionadoMesa(Constantes.Controles.IZQUIERDA);
        }

        private void btnAbajo_Tapped(object sender, TappedRoutedEventArgs e)
        {
            EnviarBotonPresionadoMesa(Constantes.Controles.ABAJO);
        }

        private void btnDerecha_Tapped(object sender, TappedRoutedEventArgs e)
        {
            EnviarBotonPresionadoMesa(Constantes.Controles.DERECHA);
        }

        private void btnTriangulo_Tapped(object sender, TappedRoutedEventArgs e)
        {
            EnviarBotonPresionadoMesa(Constantes.Controles.TRIANGULO);
        }

        private void btnCuadrado_Tapped(object sender, TappedRoutedEventArgs e)
        {
            EnviarBotonPresionadoMesa(Constantes.Controles.CUADRADO);
        }

        private void btnEquis_Tapped(object sender, TappedRoutedEventArgs e)
        {
            EnviarBotonPresionadoMesa(Constantes.Controles.EQUIS);
        }

        private void btnCirculo_Tapped(object sender, TappedRoutedEventArgs e)
        {
            EnviarBotonPresionadoMesa(Constantes.Controles.CIRCULO);
        }

        public void HabilitarControles()
        {
            Uri uri = new Uri("ms-appx:///Assets/Images/active32x32.png");
            BitmapImage image = new BitmapImage(uri);
            imgActivo.Source = image;

            btnArriba.IsEnabled = true;
            btnIzquierda.IsEnabled = true;
            btnAbajo.IsEnabled = true;
            btnDerecha.IsEnabled = true;

            btnCuadrado.IsEnabled = true;
            btnTriangulo.IsEnabled = true;
            btnCirculo.IsEnabled = true;
            btnEquis.IsEnabled = true;
        }

        public void DeshabilitarControles()
        {
            Uri uri = new Uri("ms-appx:///Assets/Images/inactive32x32.png");
            BitmapImage image = new BitmapImage(uri);
            imgActivo.Source = image;

            btnArriba.IsEnabled = false;
            btnIzquierda.IsEnabled = false;
            btnAbajo.IsEnabled = false;
            btnDerecha.IsEnabled = false;

            btnCuadrado.IsEnabled = false;
            btnTriangulo.IsEnabled = false;
            btnCirculo.IsEnabled = false;
            btnEquis.IsEnabled = false;
        }
    }
}
