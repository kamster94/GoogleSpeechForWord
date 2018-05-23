using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Word = Microsoft.Office.Interop.Word;
using Office = Microsoft.Office.Core;
using Microsoft.Office.Tools.Word;
using log4net.Config;
using log4net;
using System.Reflection;
using System.Text.RegularExpressions;

namespace GoogleSpeechForWord
{
    public partial class HandlerAddIn
    {
        private InterfaceForm interfaceForm;

        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private void HandlerAddIn_Startup(object sender, System.EventArgs e)
        {
            System.Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", "../../google-speech-pwr.json");
            interfaceForm = new InterfaceForm(this);
            interfaceForm.MinimizeBox = false;
            interfaceForm.Show();
        }

        private void HandlerAddIn_Shutdown(object sender, System.EventArgs e)
        {
        }

        public void InsertText(string text)
        {
            Word.Selection currentSelection = Application.Selection;

            // Store the user's current Overtype selection
            bool userOvertype = Application.Options.Overtype;

            // Make sure Overtype is turned off.
            if (Application.Options.Overtype)
            {
                Application.Options.Overtype = false;
            }

            // Test to see if selection is an insertion point.
            if (currentSelection.Type == Word.WdSelectionType.wdSelectionIP)
            {
                currentSelection.TypeText(text);
                currentSelection.TypeText(" ");
            }
            else
                if (currentSelection.Type == Word.WdSelectionType.wdSelectionNormal)
            {
                // Move to start of selection.
                if (Application.Options.ReplaceSelection)
                {
                    object direction = Word.WdCollapseDirection.wdCollapseStart;
                    currentSelection.Collapse(ref direction);
                }
                currentSelection.TypeText(text);
                currentSelection.TypeText(" ");
            }
            else
            {
                // Do nothing.
            }

            // Restore the user's Overtype selection
            Application.Options.Overtype = userOvertype;
        }

        public void InsertSign(string text)
        {
            log.Debug("Writing sign " + text);
            foreach (string word in text.Split(' '))
            {
                if (word.ToLower() == "slash" | word.ToLower() == "\\") InsertText("\\");
                if (word.ToLower() == "comma" | word.ToLower() == "coma" | word.ToLower() == ",") InsertText(",");
                if (word.ToLower() == "dot" | word.ToLower() == ".") InsertText(".");
                if (word.ToLower() == "bracket") InsertText("()");
            }
            
        }

        public void IssueCommand(string command)
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
