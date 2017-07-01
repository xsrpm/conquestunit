using SynapseSDK;
using System;
using System.Threading;
using Util;
using Windows.Foundation.Metadata;
using Windows.Graphics.Display;
using Windows.Networking;
using Windows.Phone.Devices.Notification;
using Windows.Storage.Streams;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
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
        private bool mandoActivo;
        private Timer timerMantenerConexion;

        public ConquestUnitMando()
        {
            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            DisplayInformation.AutoRotationPreferences = DisplayOrientations.Landscape;
            mandoActivo = false;
            //El color del jugador
            if (e != null)
            {
                var color = e.Parameter.ToString();
                TonalidadMando.Stroke = Convertidor.GetSolidColorBrush(color);
            }
            if (App.objJugador != null)
            {
                if (App.objJugador.Nombre != null)
                {
                    lblBienvenido.Text = App.objJugador.Nombre;
                }
                if (App.objJugador.Imagen != null)
                {
                    BitmapImage bimgBitmapImage = new BitmapImage();
                    IRandomAccessStream fileStream = await Convertidor.ConvertImageToStream(App.objJugador.Imagen);
                    bimgBitmapImage.SetSource(fileStream);
                    imgJugador.Source = bimgBitmapImage;
                }
            }
            IniciarSDK();

            Window.Current.CoreWindow.KeyDown += CoreWindow_KeyDown;
            
            timerMantenerConexion = new Timer(timerMantenerConexionCallback, null, TimeSpan.FromSeconds(3), TimeSpan.FromSeconds(Constantes.KeepAlive));
        }

        private async void timerMantenerConexionCallback(object state)
        {
            await App.UIDispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                IniciarSDK();
            });
        }

        private void MiMetodoReceptorJugadorJuegoHelper(string strIp, string strMensaje)
        {
            try
            {
                if (!string.IsNullOrEmpty(strIp) && !string.IsNullOrEmpty(strMensaje))
                {
                    var mensaje = strMensaje.Split(new string[] { Constantes.SEPARADOR }, StringSplitOptions.None);
                    #region La mesa indica habilitar los controles
                    if (mensaje[0] == Constantes.MesaConumicaHABILITARControles)
                    {
                        HabilitarControles();
                    }
                    #endregion
                    #region La mesa indica deshabilitar los controles
                    else if (mensaje[0] == Constantes.MesaConumicaDESHABILITARControles)
                    {
                        DeshabilitarControles();
                    }
                    #endregion
                    #region Victoria
                    else if (mensaje[0] == Constantes.MesaConumicaVICTORIAFinDelJuego)
                    {
                        Victoria();
                    }
                    #endregion
                    #region Derrota
                    else if (mensaje[0] == Constantes.MesaConumicaDERROTAFinDelJuego)
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
                    App.objSDK.ConnectStreamSocket(new HostName(App.objJugador.IpMesaConectada));
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
            pageMando.IsEnabled = false;
            pageMando.IsEnabled = true;

            if (mandoActivo)
            {
                await App.objSDK.StreamPing(Constantes.JugadorPresionaBoton + Constantes.SEPARADOR +
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

        private void imgJugador_Tapped(object sender, TappedRoutedEventArgs e)
        {
            IniciarSDK();
        }

        private void CoreWindow_KeyDown(CoreWindow sender, KeyEventArgs args)
        {
            switch (args.VirtualKey)
            {
                //Direccionales
                case Windows.System.VirtualKey.A:
                    EnviarBotonPresionadoMesa(Constantes.Controles.IZQUIERDA);
                    break;
                case Windows.System.VirtualKey.W:
                    EnviarBotonPresionadoMesa(Constantes.Controles.ARRIBA);
                    break;
                case Windows.System.VirtualKey.D:
                    EnviarBotonPresionadoMesa(Constantes.Controles.DERECHA);
                    break;
                case Windows.System.VirtualKey.X:
                    EnviarBotonPresionadoMesa(Constantes.Controles.ABAJO);
                    break;
                case Windows.System.VirtualKey.Q:
                    EnviarBotonPresionadoMesa(Constantes.Controles.ARRIBAIZQUIERDA);
                    break;
                case Windows.System.VirtualKey.E:
                    EnviarBotonPresionadoMesa(Constantes.Controles.ARRIBADERECHA);
                    break;
                case Windows.System.VirtualKey.Z:
                    EnviarBotonPresionadoMesa(Constantes.Controles.ABAJOIZQUIERDA);
                    break;
                case Windows.System.VirtualKey.C:
                    EnviarBotonPresionadoMesa(Constantes.Controles.ABAJODERECHA);
                    break;
                //Botones accion
                case Windows.System.VirtualKey.J:
                    EnviarBotonPresionadoMesa(Constantes.Controles.CUADRADO);
                    break;
                case Windows.System.VirtualKey.I:
                    EnviarBotonPresionadoMesa(Constantes.Controles.TRIANGULO);
                    break;
                case Windows.System.VirtualKey.L:
                    EnviarBotonPresionadoMesa(Constantes.Controles.CIRCULO);
                    break;
                case Windows.System.VirtualKey.K:
                    EnviarBotonPresionadoMesa(Constantes.Controles.EQUIS);
                    break;
                //Acciones
                case Windows.System.VirtualKey.Enter:
                    EnviarBotonPresionadoMesa(Constantes.Controles.EQUIS);
                    break;
                case Windows.System.VirtualKey.Space:
                    EnviarBotonPresionadoMesa(Constantes.Controles.EQUIS);
                    break;
            }
        }

    }
}
