using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GoogleSpeechForWord
{
    public partial class InterfaceForm : Form
    {
        private HandlerAddIn handler;
        SpeechRecognition recognitionEng;

        protected override CreateParams CreateParams
        {
            get
            {
                const int CS_NOCLOSE = 0x200;
                CreateParams cp = base.CreateParams;
                cp.ClassStyle = cp.ClassStyle | CS_NOCLOSE;
                return cp;
            }
        }

        public InterfaceForm(HandlerAddIn handler)
        {
            InitializeComponent();
            this.handler = handler;
            recognitionEng = new SpeechRecognition(handler);

        }

        private void ButtonCreateDoc_Click(object sender, EventArgs e)
        {
            handler.Application.Documents.Add();
        }

        private void ButtonRecording_ClickAsync(object sender, EventArgs e)
        {
            recognitionEng.StartListening(10);
        }
    }
}
