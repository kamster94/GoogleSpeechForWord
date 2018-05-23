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
using System.Resources;

namespace GoogleSpeechForWord
{
    public partial class HandlerAddIn
    {
        private InterfaceForm interfaceForm;

        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        ResourceManager resourceManager;

        private void HandlerAddIn_Startup(object sender, System.EventArgs e)
        {
            System.Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", "../../google-speech-pwr.json");
            resourceManager = new ResourceManager("GoogleSpeechForWord.Resources.Polish", Assembly.GetExecutingAssembly());
            interfaceForm = new InterfaceForm(this, resourceManager);
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
                if (word.ToLower() == resourceManager.GetString("slash") | word.ToLower() == "\\") InsertText("\\");
                if (word.ToLower() == resourceManager.GetString("comma") | word.ToLower() == resourceManager.GetString("commaAlt") | word.ToLower() == ",") InsertText(",");
                if (word.ToLower() == resourceManager.GetString("dot") | word.ToLower() == ".") InsertText(".");
                if (word.ToLower() == resourceManager.GetString("bracket")) InsertText("()");
            }
            
        }

        public void IssueCommand(string command)
        {
            log.Debug(command);
            if (command.ToLower().StartsWith(resourceManager.GetString("undo")))
            {
                log.Debug("Undoing changes");
                int number = SillyNumberParser(command.Split(' ')[2]);
                if (number == 0) Int32.TryParse(command.Split(' ')[2], out number);
                log.Debug(number + " times");
                this.Application.ActiveDocument.Undo(number);
            }
            else if (command.ToLower().StartsWith(resourceManager.GetString("moveUp")))
            {
                Word.Selection currentSelection = Application.Selection;
                log.Debug("Moving up");
                int number = SillyNumberParser(command.Split(' ')[3]);
                if (number == 0) Int32.TryParse(command.Split(' ')[3], out number);
                log.Debug(number + " times");
                currentSelection.MoveUp(WdUnits.wdLine, number);
            }
            else if (command.ToLower().StartsWith(resourceManager.GetString("moveLeft")))
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
            if (textNumber == resourceManager.GetString("one")) return 1;
            else if (textNumber == resourceManager.GetString("two") | textNumber == resourceManager.GetString("twoAlt")) return 2;
            else if (textNumber == resourceManager.GetString("three")) return 3;
            else if (textNumber == resourceManager.GetString("four")) return 4;
            else if (textNumber == resourceManager.GetString("five")) return 5;
            else return 0;
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
