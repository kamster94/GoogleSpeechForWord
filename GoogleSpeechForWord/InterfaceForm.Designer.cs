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
            this.buttonCreateDoc = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonRecording
            // 
            this.buttonRecording.Location = new System.Drawing.Point(12, 126);
            this.buttonRecording.Name = "buttonRecording";
            this.buttonRecording.Size = new System.Drawing.Size(126, 23);
            this.buttonRecording.TabIndex = 0;
            this.buttonRecording.Text = "Start recording";
            this.buttonRecording.UseVisualStyleBackColor = true;
            this.buttonRecording.Click += new System.EventHandler(this.ButtonRecording_ClickAsync);
            // 
            // buttonCreateDoc
            // 
            this.buttonCreateDoc.Location = new System.Drawing.Point(12, 13);
            this.buttonCreateDoc.Name = "buttonCreateDoc";
            this.buttonCreateDoc.Size = new System.Drawing.Size(126, 23);
            this.buttonCreateDoc.TabIndex = 1;
            this.buttonCreateDoc.Text = "Create new document";
            this.buttonCreateDoc.UseVisualStyleBackColor = true;
            this.buttonCreateDoc.Click += new System.EventHandler(this.ButtonCreateDoc_Click);
            // 
            // InterfaceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(444, 161);
            this.Controls.Add(this.buttonCreateDoc);
            this.Controls.Add(this.buttonRecording);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "InterfaceForm";
            this.ShowIcon = false;
            this.Text = "Google Speech Recognition Solution for Microsoft Word";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonRecording;
        private System.Windows.Forms.Button buttonCreateDoc;
    }
}