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
using Microsoft.Office.Interop.Word;

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
            log.Debug(command);
            if (command.StartsWith(" back"))
            {
                log.Debug("Undoing changes");
                int number = SillyNumberParser(command.Substring(6));
                if (number == 0) Int32.TryParse(command.Substring(6, 1), out number);
                log.Debug(number + " times");
                this.Application.ActiveDocument.Undo(number);
            }
            else if (command.StartsWith(" move up"))
            {
                Word.Selection currentSelection = Application.Selection;
                log.Debug("Moving up");
                int number = SillyNumberParser(command.Split(' ')[3]);
                if (number == 0) Int32.TryParse(command.Split(' ')[3], out number);
                log.Debug(number + " times");
                currentSelection.MoveUp(WdUnits.wdLine, number);
            }
            else if (command.StartsWith(" move left"))
            {
                Word.Selection currentSelection = Application.Selection;
                log.Debug("Moving left");
                int number = SillyNumberParser(command.Split(' ')[3]);
                log.Debug(command.Split(' ')[3]);
                if (number == 0) Int32.TryParse(command.Split(' ')[3], out number);
                log.Debug(number + " times");
                currentSelection.MoveLeft(WdUnits.wdCharacter, number);
            }
        }

        private int SillyNumberParser(string textNumber)
        {
            switch(textNumber)
            {
                case "one":
                    return 1;
                case "two":
                case "to":
                    return 2;
                case "three":
                    return 3;
                case "four":
                    return 4;
                case "five":
                    return 5;
                default:
                    return 0;
            }
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
