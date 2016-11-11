using ConquestUnit.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Util;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Foundation.Metadata;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Core;
using SynapseSDK;
using DataModel;
using Windows.UI.Xaml.Media.Imaging;

namespace ConquestUnit
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public static ConquestUnitContext context { get; set; }
        public static Jugador objJugador { get; set; }
        public static CoreDispatcher UIDispatcher = null;
        public static MainCore objSDK { get; set; }
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
            //Inicializando
            if (DetectPlatform() == Platform.Windows)
            {
                context = new ConquestUnitContext();
                context.InitDatabase(Windows.Storage.ApplicationData.Current.LocalFolder.Path);
            }
            Task.Run(async () => { objJugador = await LocalStorage.ObtenerDatosJugador(); }).Wait();
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached)
            {
                this.DebugSettings.EnableFrameRateCounter = true;
            }
#endif
            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                #region Set Frame.Background property in the App.xaml.cs
                //http://www.reflectionit.nl/blog/2012/windows-8-xaml-tips-app-background-image
                //http://stackoverflow.com/questions/12743355/screen-flashes-between-splash-and-extended-splash-in-windows-8-app
                // Set the application background Image
                string urlBackgroundImage = "ms-appx:///Assets/Pantallas/PC/Fondo.png";
                if (DetectPlatform()==Platform.WindowsPhone)
                {
                    urlBackgroundImage = "ms-appx:///Assets/Pantallas/Phone/Fondo.jpg";
                }
                rootFrame.Background = new ImageBrush
                {
                    Stretch = Windows.UI.Xaml.Media.Stretch.UniformToFill,
                    ImageSource = new BitmapImage { UriSource = new Uri(urlBackgroundImage) }
                };
                //Associate the frame with a SuspensionManager key
                //SuspensionManager.RegisterFrame(rootFrame, "AppFrame");
                #endregion

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (e.PrelaunchActivated == false)
            {
                if (rootFrame.Content == null)
                {
                    // When the navigation stack isn't restored navigate to the first page,
                    // configuring the new page by passing required information as a navigation
                    // parameter
                    rootFrame.Navigate(typeof(Views.SplashScreen), e.Arguments);
                }
                // Ensure the current window is active
                Window.Current.Activate();
            }
        }

        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }

        public static Platform DetectPlatform()
        {
            bool isHardwareButtonsAPIPresent = ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons");
            return isHardwareButtonsAPIPresent ? Platform.WindowsPhone : Platform.Windows;
        }

        public enum TipoRol
        {
            Ninguno,
            Sala,
            Jugador
        }
    }
}
