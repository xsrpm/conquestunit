using SynapseSDK;
using System;
using Util;
using Windows.Foundation.Metadata;
using Windows.Graphics.Display;
using Windows.Networking;
using Windows.Phone.Devices.Notification;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace ConquestUnit.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ConquestUnitMando : Page
    {
        private bool mandoActivo;
        public ConquestUnitMando()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            DisplayInformation.AutoRotationPreferences = DisplayOrientations.Landscape;
            mandoActivo = false;
            //El color del jugador
            if (e != null)
            {
                var color = e.Parameter.ToString();
                TonalidadMando.Stroke = Convertidor.GetSolidColorBrush(color);
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
                    #region Victoria
                    else if (strMensaje.Trim().Contains(Constantes.MesaConumicaVICTORIAFinDelJuego))
                    {
                        Victoria();
                    }
                    #endregion
                    #region Derrota
                    else if (strMensaje.Trim().Contains(Constantes.MesaConumicaDERROTAFinDelJuego))
                    {
                        Derrota();
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

        public void Victoria()
        {
            DeshabilitarControles();
            panelDireccional.Visibility = Visibility.Collapsed;
            panelComandos.Visibility = Visibility.Collapsed;
            panelEncendido.Visibility = Visibility.Collapsed;
            btnMensaje.Content = "GANASTE";
            GridMensajeFinJuego.Visibility = Visibility.Visible;
        }
        public void Derrota()
        {
            DeshabilitarControles();
            panelDireccional.Visibility = Visibility.Collapsed;
            panelComandos.Visibility = Visibility.Collapsed;
            panelEncendido.Visibility = Visibility.Collapsed;
            btnMensaje.Content = "DERROTA";
            GridMensajeFinJuego.Visibility = Visibility.Visible;
        }

        public async void EnviarBotonPresionadoMesa(int botonPresionado)
        {
            if (mandoActivo)
            {
                await App.objSDK.UnicastPing(new HostName(App.objJugador.IpMesaConectada),
                    Constantes.JugadorPresionaBoton + Constantes.SEPARADOR +
                    App.objJugador.Ip + Constantes.SEPARADOR +
                    botonPresionado);
            }
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
            mandoActivo = true;
            lblTurno.Text = "En Turno";
            panelEncendido.Background = Convertidor.GetSolidColorBrush(Constantes.COLORMANDOACTIVO);
            if (App.DetectPlatform() == Platform.WindowsPhone)
            {
                var vibration = VibrationDevice.GetDefault();
                vibration.Vibrate(TimeSpan.FromMilliseconds(500));
            }
        }

        public void DeshabilitarControles()
        {
            mandoActivo = false;
            lblTurno.Text = "Esperando Turno...";
            panelEncendido.Background = Convertidor.GetSolidColorBrush(Constantes.COLORMANDOINACTIVO);
        }

        private void btnArribaIzquierda_Tapped(object sender, TappedRoutedEventArgs e)
        {
            EnviarBotonPresionadoMesa(Constantes.Controles.ARRIBAIZQUIERDA);
        }

        private void btnArribaDerecha_Tapped(object sender, TappedRoutedEventArgs e)
        {
            EnviarBotonPresionadoMesa(Constantes.Controles.ARRIBADERECHA);
        }

        private void btnAbajoIzquierda_Tapped(object sender, TappedRoutedEventArgs e)
        {
            EnviarBotonPresionadoMesa(Constantes.Controles.ABAJOIZQUIERDA);
        }

        private void btnAbajoDerecha_Tapped(object sender, TappedRoutedEventArgs e)
        {
            EnviarBotonPresionadoMesa(Constantes.Controles.ABAJODERECHA);
        }

        private void btnRegresar_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(ElegirMesa));
        }

        private void btnMenuPrincipal_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(MenuPrincipal));
        }
    }
}
