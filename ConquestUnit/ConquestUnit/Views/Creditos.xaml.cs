using Windows.Graphics.Display;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace ConquestUnit.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Creditos : Page
    {
        public Creditos()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            DisplayInformation.AutoRotationPreferences = DisplayOrientations.Portrait;
        }

        private void btnAtras_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(MenuPrincipal));
        }
    }
}
