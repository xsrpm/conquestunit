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
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
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

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (App.objJugador != null)
            {
                if (App.objJugador.Nombre != null)
                {
                    lblNombreJugador.Text = lblNombreJugador.Text + " " + App.objJugador.Nombre;
                }
            }
            IniciarSDK();
        }

        private void btnRegresar_Tapped(object sender, TappedRoutedEventArgs e)
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
                        //await App.objSDK.UnicastPing(new HostName(objDevice.IP),
                        //    Constantes.UnirseEnviameConfirmacion + Constantes.SEPARADOR +
                        //    mesaSeleccionada + Constantes.SEPARADOR +
                        //    App.objJugador.Ip + Constantes.SEPARADOR +
                        //    App.objJugador.Nombre + Constantes.SEPARADOR +
                        //    strBytes);
                    }
                }
            }
            catch (Exception ex)
            {
                Helper.MensajeOk(ex.Message);
            }
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
                        if (mensaje.Length != 3)
                            return;

                        //Reenviar a la pantalla de Jugador esperando el inicio del juego
                        Mesa objMesa = new Mesa();
                        objMesa.Ip = mensaje[1];
                        objMesa.MesaID = mensaje[2];

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
    }
}
