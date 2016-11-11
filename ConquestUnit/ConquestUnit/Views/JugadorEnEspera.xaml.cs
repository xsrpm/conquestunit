using DataModel;
using SynapseSDK;
using System;
using Util;
using Windows.Graphics.Display;
using Windows.Networking;
using Windows.Storage.Streams;
using Windows.UI.Core;
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
    public sealed partial class JugadorEnEspera : Page
    {
        Juego objMesa;
        public JugadorEnEspera()
        {
            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            DisplayInformation.AutoRotationPreferences = DisplayOrientations.Portrait;
            if (e.Parameter != null)
            {
                objMesa = (Juego)e.Parameter;
                lblMesaId.Text = objMesa.JuegoID;
                App.objJugador.IpMesaConectada = objMesa.Ip;
            }

            if (App.objJugador != null)
            {
                if (App.objJugador.Nombre != null)
                {
                    lblNombreJugador.Text = lblNombreJugador.Text + " " + App.objJugador.Nombre;
                    lblNombre.Text = App.objJugador.Nombre;
                }
                if (App.objJugador.Imagen != null)
                {
                    BitmapImage bimgBitmapImage = new BitmapImage();
                    IRandomAccessStream fileStream = await Convertidor.ConvertImageToStream(App.objJugador.Imagen);
                    bimgBitmapImage.SetSource(fileStream);
                    imgJugador.Source = bimgBitmapImage;
                    //imgFoto.Source = bimgBitmapImage;
                }
            }
            IniciarSDK();
        }

        private async void btnAtras_Tapped(object sender, TappedRoutedEventArgs e)
        {
            //Notificar a la mesa que se está saliendo de la espera
            if (objMesa != null)
                await App.objSDK.UnicastPing(new HostName(objMesa.Ip),
                            Constantes.JugadorSaleMesa + Constantes.SEPARADOR +
                            App.objJugador.Ip);
            App.objSDK.setObjMetodoReceptorString = null;

            this.Frame.Navigate(typeof(ElegirMesa));
        }

        private void MiMetodoReceptorJugadorEsperaHelper(string strIp, string strMensaje)
        {
            try
            {
                if (!string.IsNullOrEmpty(strIp) && !string.IsNullOrEmpty(strMensaje))
                {
                    #region La mesa indica se cierra
                    if (strMensaje.Trim().Contains(Constantes.MesaIndicaSeCierra))
                    {
                        Helper.MensajeOk("La mesa se ha cerrado");
                        this.Frame.Navigate(typeof(ElegirMesa));
                    }
                    #endregion
                    #region La mesa que el juega ha iniciado
                    else if (strMensaje.Trim().Contains(Constantes.MesaIndicaJuegoInicia))
                    {
                        // Inicio del juego
                        var mensaje = strMensaje.Split(new string[] { Constantes.SEPARADOR }, StringSplitOptions.None);
                        //mensaje[0] => Acción (MesaIndicaJuegoInicia)
                        //mensaje[1] => objJugador.Ip primer turno
                        if (mensaje.Length != 2)
                            return;

                        //Verificar si es el jugador del primer turno
                        if (mensaje[1].Equals(App.objJugador.Ip))
                            this.Frame.Navigate(typeof(ConquestUnitMando), true);
                        else
                            this.Frame.Navigate(typeof(ConquestUnitMando), false);
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
                App.objSDK = MainCore.getInstance(Constantes.MULTICAST_ADDRESS, Constantes.MULTICAST_SERVICE_PORT, Constantes.UNICAST_SERVICE_PORT, Constantes.STREAM_SERVICE_PORT, MiMetodoReceptorJugadorEspera, Constantes.DELAY);
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
                    App.objSDK.setObjMetodoReceptorString = MiMetodoReceptorJugadorEspera;
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

        public async void MiMetodoReceptorJugadorEspera(string strIp, string strMessage)
        {
            try
            {
                await App.UIDispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    MiMetodoReceptorJugadorEsperaHelper(strIp, strMessage);
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
