using System;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using DataModel;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ConquestUnit
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        //will be fired -invoked- when you're leaving the page .. and before navigating from it ..
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
        }

        //Will be fired -invoked- when you arrive to the page 
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
        }

        private void Retrieve_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var pregunta = App.context.Obtener<Pregunta>(Convert.ToInt32(txtId.Text));
                var opciones = App.context.Listar<Opcion>().Where(x => x.PreguntaId == pregunta.PreguntaId).ToList();
                txtResultado.Text = pregunta.TextoPregunta + "\n";
                for (int i = 0; i < opciones.Count; i++)
                {
                    txtResultado.Text += opciones[i].TextoOpcion + "\n";
                }
            }
            catch (Exception ex)
            {
                txtResultado.Text = ex.Message;
            }
        }
    }
}
