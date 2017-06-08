using DataModel;
using SynapseSDK;
using System;
using Util;
using Windows.Foundation.Metadata;
using Windows.Graphics.Display;
using Windows.Networking;
using Windows.Storage.Streams;
using Windows.System;
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
    public sealed partial class ElegirMesa : Page
    {
        public ElegirMesa()
        {
            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            DisplayInformation.AutoRotationPreferences = DisplayOrientations.Portrait;
            if (App.objJugador != null)
            {
                if (App.objJugador.Nombre != null)
                {
                    lblNombreJugador.Text = App.objJugador.Nombre;
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
        }

        private void infoJugador_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(GuardarDatosJugador), typeof(ElegirMesa));
        }

        private void btnAtras_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (App.DetectPlatform() == Platform.WindowsPhone)
            {
                this.Frame.Navigate(typeof(MenuPrincipal));
            }
            else
            {
                this.Frame.Navigate(typeof(SeleccionarRol));
            }
        }

        private async void btnUnirme_Tapped(object sender, TappedRoutedEventArgs e)
        {
            txtMesaId.IsEnabled = false;
            btnUnirme.Visibility = Visibility.Collapsed;
            prConectando.IsActive = true;
            prConectando.Visibility = Visibility.Visible;
            lblConectando.Visibility = Visibility.Visible;
            try
            {
                string mesaSeleccionada = txtMesaId.Text;
                string strBytes = string.Empty;

                App.objSDK.clearDeviceCollection();
                await App.objSDK.MulticastPing();
                var dispositivos = App.objSDK.getDeviceCollection();

                if (App.objJugador.Imagen != null)
                    strBytes = Convert.ToBase64String(App.objJugador.Imagen);
                else
                    strBytes = Constantes.SIN_IMAGEN;

                if (dispositivos != null)
                {
                    foreach (var objDevice in dispositivos)
                    {
                        var hn = new HostName(objDevice.IP);
                        await App.objSDK.ConnectStreamSocket(hn);
                        await App.objSDK.StreamPing(Constantes.UnirseEnviameConfirmacion + Constantes.SEPARADOR +
                            mesaSeleccionada + Constantes.SEPARADOR +
                            App.objJugador.Ip + Constantes.SEPARADOR +
                            App.objJugador.Nombre + Constantes.SEPARADOR +
                            strBytes);
                    }
                }
            }
            catch (Exception)
            {
            }
            prConectando.IsActive = false;
            prConectando.Visibility = Visibility.Collapsed;
            lblConectando.Visibility = Visibility.Collapsed;
            btnUnirme.Visibility = Visibility.Visible;
            txtMesaId.IsEnabled = true;
        }

        public void MiMetodoReceptorSeleccionMesaHelper(string strIp, string strMensaje)
        {
            try
            {
                if (!string.IsNullOrEmpty(strIp) && !string.IsNullOrEmpty(strMensaje))
                {
                    #region Jugador recibe la confirmacion que se ha unido a la mesa
                    if (strMensaje.Trim().Contains(Constantes.ConfirmacionUnirseMesa))
                    {
                        // Se recibe la confirmación de parte de la mesa que se unido satisfactoriamente
                        var mensaje = strMensaje.Split(new string[] { Constantes.SEPARADOR }, StringSplitOptions.None);
                        //mensaje[0] => Acción (ConfirmacionUnirseMesa)
                        //mensaje[1] => objMesa.Ip
                        //mensaje[2] => objMesa.MesaID
                        //mensaje[3] => objMesa.TipoMapa
                        if (mensaje.Length != 4)
                            return;

                        txtMesaId.IsEnabled = false;
                        btnUnirme.Visibility = Visibility.Collapsed;
                        prConectando.IsActive = true;
                        prConectando.Visibility = Visibility.Visible;
                        lblConectando.Visibility = Visibility.Visible;

                        //Reenviar a la pantalla de Jugador esperando el inicio del juego
                        Juego objMesa = new Juego(int.Parse(mensaje[3]));
                        objMesa.Ip = mensaje[1];
                        objMesa.JuegoID = mensaje[2];

                        this.Frame.Navigate(typeof(JugadorEnEspera), objMesa);
                    }
                    #endregion
                }

            }
            catch (Exception ex)
            {
                Helper.MensajeOk(ex.Message);
            }

        }

        #region Conexion SynapseSDK
        private void IniciarSDK()
        {
            try
            {
                App.UIDispatcher = this.Dispatcher;
                App.objSDK = MainCore.getInstance(Constantes.MULTICAST_ADDRESS, Constantes.MULTICAST_SERVICE_PORT, Constantes.UNICAST_SERVICE_PORT, Constantes.STREAM_SERVICE_PORT, MiMetodoReceptorSeleccionMesa, Constantes.DELAY);
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
                    App.objSDK.setObjMetodoReceptorString = MiMetodoReceptorSeleccionMesa;
                }
                else
                {
                    //No hay conexión
                    string strMensaje = "Lo sentimos, no se pudo establecer la conexión vía Wi-Fi. Intente nuevamente.";
                    Helper.MensajeOk(strMensaje);
                    if (App.DetectPlatform() == Platform.WindowsPhone)
                        this.Frame.Navigate(typeof(MenuPrincipal));
                    else
                        this.Frame.Navigate(typeof(SeleccionarRol));
                    return;
                }
            }
            catch (Exception ex)
            {
                Helper.MensajeOk(ex.Message);
            }
        }

        public async void MiMetodoReceptorSeleccionMesa(string strIp, string strMessage)
        {
            try
            {
                await App.UIDispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    MiMetodoReceptorSeleccionMesaHelper(strIp, strMessage);
                });
            }
            catch (Exception ex)
            {
                Helper.MensajeOk(ex.Message);
            }
        }
        #endregion

        private void txtMesaId_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Enter)
            {
                txtMesaId.IsEnabled = false;
                txtMesaId.IsEnabled = true;
                btnUnirme_Tapped(null, null);
            }
        }

        private void txtMesaId_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key.ToString().Equals("Back"))
            {
                e.Handled = false;
                return;
            }
            for (int i = 0; i < 10; i++)
            {
                if (e.Key.ToString() == string.Format("Number{0}", i))
                {
                    e.Handled = false;
                    return;
                }
            }
            e.Handled = true;
        }
    }
}
