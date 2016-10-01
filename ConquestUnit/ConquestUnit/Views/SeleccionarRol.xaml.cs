using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class SeleccionarRol : Page
    {
        bool perfilCreado = false;
        public SeleccionarRol()
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
                    perfilCreado = true;
                }
            }
        }

        private void imgJugador_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(GuardarDatosJugador), typeof(SeleccionarRol));
        }

        private void imgElegirMesa_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MesaEnEspera));
        }

        private void imgElegirJugador_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (perfilCreado)
            {
                this.Frame.Navigate(typeof(ElegirMesa));
            }
            else
            {
                this.Frame.Navigate(typeof(GuardarDatosJugador), typeof(ElegirMesa));
            }
        }

        private void btnRegresar_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MenuPrincipal));
        }
    }
}
