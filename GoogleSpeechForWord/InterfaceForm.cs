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
        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();

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
            timer.Interval = 10000; // here time in milliseconds
            timer.Tick += timer_Tick;
            timer.Start();
            buttonRecording.Enabled = false;
            recognitionEng.StartListening(10);

        }

        void timer_Tick(object sender, System.EventArgs e)
        {
            buttonRecording.Enabled = true;
            timer.Stop();
        }
    }
}
