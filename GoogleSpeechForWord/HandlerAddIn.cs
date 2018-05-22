using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Word = Microsoft.Office.Interop.Word;
using Office = Microsoft.Office.Core;
using Microsoft.Office.Tools.Word;

namespace GoogleSpeechForWord
{
    public partial class HandlerAddIn
    {
        private InterfaceForm interfaceForm;

        private void HandlerAddIn_Startup(object sender, System.EventArgs e)
        {
            System.Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", "../../google-speech-pwr.json");
            interfaceForm = new InterfaceForm(this);
            interfaceForm.Show();
        }

        private void HandlerAddIn_Shutdown(object sender, System.EventArgs e)
        {
        }

        #region Kod wygenerowany przez program VSTO

        /// <summary>
        /// Wymagana metoda obsługi projektanta — nie należy modyfikować 
        /// zawartość tej metody z edytorem kodu.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(HandlerAddIn_Startup);
            this.Shutdown += new System.EventHandler(HandlerAddIn_Shutdown);
        }
        
        #endregion
    }
}
