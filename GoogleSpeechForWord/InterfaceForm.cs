using System;
using System.Resources;
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

        public InterfaceForm(HandlerAddIn handler, ResourceManager resourceManager)
        {
            InitializeComponent();
            this.handler = handler;
            recognitionEng = new SpeechRecognition(handler, resourceManager);
        }
        private void ButtonInfo_Click(object sender, EventArgs e)
        {
            InfoForm info = new InfoForm();
            info.ShowDialog();
        }

        private void ButtonRecording_ClickAsync(object sender, EventArgs e)
        {
            timer.Interval = 10000;
            timer.Tick += timer_Tick;
            timer.Start();
            buttonRecording.Enabled = false;
            iconBox.Image = Properties.Resources.baseline_keyboard_voice_black_18dp_on;
            recognitionEng.StartListening(10);

        }

        void timer_Tick(object sender, System.EventArgs e)
        {
            buttonRecording.Enabled = true;
            iconBox.Image = Properties.Resources.baseline_keyboard_voice_black_18dp;
            timer.Stop();
        }
    }
}
