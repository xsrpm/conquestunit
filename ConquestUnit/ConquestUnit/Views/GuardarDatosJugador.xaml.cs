using DataModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Util;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Foundation.Metadata;
using Windows.Graphics.Display;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.System;
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
    public sealed partial class GuardarDatosJugador : Page
    {
        public static BitmapImage bimgBitmapImage = new BitmapImage();
        byte[] fotoBytes;
        Type paginaRedirect;

        public GuardarDatosJugador()
        {
            this.InitializeComponent();
            paginaRedirect = typeof(MenuPrincipal);
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            DisplayInformation.AutoRotationPreferences = DisplayOrientations.Portrait;
            //Pagina a redireccionar
            if (e.Parameter != null)
                paginaRedirect = (Type)e.Parameter;
            if (App.objJugador != null)
            {
                if (App.objJugador.Nombre != null)
                {
                    lblNombreJugador.Text = "Bienvenido " + App.objJugador.Nombre;
                    txtNombre.Text = App.objJugador.Nombre;
                }
                if (App.objJugador.Imagen != null)
                {
                    fotoBytes = App.objJugador.Imagen;
                    bimgBitmapImage = new BitmapImage();
                    IRandomAccessStream fileStream = await Convertidor.ConvertImageToStream(fotoBytes);
                    bimgBitmapImage.SetSource(fileStream);
                    imgJugador.Source = bimgBitmapImage;
                    imgFoto.Source = bimgBitmapImage;
                }
                else
                {
                    Uri uri = new Uri("ms-appx:///Assets/Images/Icons/logo128x128.png");
                    BitmapImage logoImage = new BitmapImage(uri);
                    imgJugador.Source = logoImage;
                }
            }
            else
            {
                if (App.DetectPlatform() == Platform.WindowsPhone)
                {
                    Uri uri = new Uri("ms-appx:///Assets/Images/Icons/logo128x128.png");
                    BitmapImage logoImage = new BitmapImage(uri);
                    imgJugador.Source = logoImage;
                    btnAtras.Visibility = Visibility.Collapsed;
                }
                if (paginaRedirect == typeof(ElegirMesa))
                {
                    btnAtras.Visibility = Visibility.Collapsed;
                }
            }
        }

        private async void imgFoto_Tapped(object sender, TappedRoutedEventArgs e)
        {
            try
            {
                IRandomAccessStream fileStream;
                FileOpenPicker openPicker = new FileOpenPicker();
                openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
                openPicker.ViewMode = PickerViewMode.Thumbnail;
                openPicker.FileTypeFilter.Clear();
                openPicker.FileTypeFilter.Add(".png");
                openPicker.FileTypeFilter.Add(".jpeg");
                openPicker.FileTypeFilter.Add(".jpg");

                StorageFile file = await openPicker.PickSingleFileAsync();

                if (file != null)
                {
                    fileStream = await file.OpenAsync(FileAccessMode.Read);
                    fotoBytes = await Convertidor.ConvertImageToBytes(fileStream);
                    bimgBitmapImage.SetSource(fileStream);
                    imgFoto.Source = bimgBitmapImage;
                }
                btnGuardar.Focus(FocusState.Programmatic);
            }
            catch (Exception ex)
            {
                Helper.MensajeOk(ex.Message);
            }
        }

        private async void btnGuardar_Tapped(object sender, TappedRoutedEventArgs e)
        {
            string strMensaje;
            try
            {
                if (string.IsNullOrEmpty(txtNombre.Text.Trim()))
                {
                    strMensaje = "No ha ingresado su nombre";
                    Helper.MensajeOk(strMensaje);
                    return;
                }

                var objJugador = new Jugador();
                objJugador.Nombre = txtNombre.Text.TrimStart().TrimEnd();
                objJugador.Imagen = fotoBytes;
                await LocalStorage.GuardarDatosJugador(objJugador);
                App.objJugador = objJugador;

                this.Frame.Navigate(paginaRedirect);
            }
            catch (Exception ex)
            {
                Helper.MensajeOk(ex.Message);
            }
        }

        private void btnAtras_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(paginaRedirect);
        }

        private void txtNombre_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Enter)
            {
                txtNombre.IsEnabled = false;
                txtNombre.IsEnabled = true;
                btnGuardar_Tapped(null, null);
            }
        }
    }
}
