namespace GoogleSpeechForWord
{
    partial class InterfaceForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonRecording = new System.Windows.Forms.Button();
            this.buttonInfo = new System.Windows.Forms.Button();
            this.iconBox = new System.Windows.Forms.PictureBox();
            this.label = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.iconBox)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonRecording
            // 
            this.buttonRecording.FlatAppearance.BorderSize = 0;
            this.buttonRecording.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.buttonRecording.Location = new System.Drawing.Point(12, 97);
            this.buttonRecording.Name = "buttonRecording";
            this.buttonRecording.Size = new System.Drawing.Size(155, 23);
            this.buttonRecording.TabIndex = 0;
            this.buttonRecording.Text = "Start recording";
            this.buttonRecording.UseVisualStyleBackColor = true;
            this.buttonRecording.Click += new System.EventHandler(this.ButtonRecording_ClickAsync);
            // 
            // buttonInfo
            // 
            this.buttonInfo.Location = new System.Drawing.Point(12, 126);
            this.buttonInfo.Name = "buttonInfo";
            this.buttonInfo.Size = new System.Drawing.Size(155, 23);
            this.buttonInfo.TabIndex = 1;
            this.buttonInfo.Text = "Info";
            this.buttonInfo.UseVisualStyleBackColor = true;
            this.buttonInfo.Click += new System.EventHandler(this.ButtonInfo_Click);
            // 
            // iconBox
            // 
            this.iconBox.Image = global::GoogleSpeechForWord.Properties.Resources.baseline_keyboard_voice_black_18dp;
            this.iconBox.Location = new System.Drawing.Point(12, 50);
            this.iconBox.Name = "iconBox";
            this.iconBox.Size = new System.Drawing.Size(155, 41);
            this.iconBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.iconBox.TabIndex = 2;
            this.iconBox.TabStop = false;
            // 
            // label
            // 
            this.label.Location = new System.Drawing.Point(12, 9);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(155, 38);
            this.label.TabIndex = 3;
            this.label.Text = "Google Speech Recognition \r\nSolution for Microsoft Word";
            this.label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // InterfaceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(179, 161);
            this.Controls.Add(this.label);
            this.Controls.Add(this.iconBox);
            this.Controls.Add(this.buttonInfo);
            this.Controls.Add(this.buttonRecording);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "InterfaceForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.iconBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonRecording;
        private System.Windows.Forms.Button buttonInfo;
        private System.Windows.Forms.PictureBox iconBox;
        private System.Windows.Forms.Label label;
    }
}