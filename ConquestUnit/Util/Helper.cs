using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace Util
{
    public static class Helper
    {
        public static async void MensajeOk(string strMensaje)
        {
            MessageDialog msgDialog = new MessageDialog(strMensaje, Constantes.MessageDialogTitle);
            UICommand okBtn = new UICommand("OK");
            msgDialog.Commands.Add(okBtn);
            await msgDialog.ShowAsync();
        }
    }
}
