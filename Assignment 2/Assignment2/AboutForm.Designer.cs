
namespace Assignment2
{
    partial class AboutForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutForm));
            this.notepadPictureBox = new System.Windows.Forms.PictureBox();
            this.titleLabel = new System.Windows.Forms.Label();
            this.okButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.notepadPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // notepadPictureBox
            // 
            this.notepadPictureBox.BackColor = System.Drawing.SystemColors.Control;
            this.notepadPictureBox.ErrorImage = null;
            this.notepadPictureBox.Image = global::Assignment2.Properties.Resources.notepad_png;
            this.notepadPictureBox.InitialImage = global::Assignment2.Properties.Resources.notepad_png;
            this.notepadPictureBox.Location = new System.Drawing.Point(36, 51);
            this.notepadPictureBox.Name = "notepadPictureBox";
            this.notepadPictureBox.Size = new System.Drawing.Size(131, 145);
            this.notepadPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.notepadPictureBox.TabIndex = 0;
            this.notepadPictureBox.TabStop = false;
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.BackColor = System.Drawing.Color.Transparent;
            this.titleLabel.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.titleLabel.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.titleLabel.Location = new System.Drawing.Point(200, 65);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(271, 120);
            this.titleLabel.TabIndex = 6;
            this.titleLabel.Text = "Helena\'s Text Editor (v.1)\r\n2021 by Helena Tran\r\n\r\nHope you enjoy it!";
            // 
            // okButton
            // 
            this.okButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.okButton.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.okButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.okButton.Location = new System.Drawing.Point(200, 240);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(111, 54);
            this.okButton.TabIndex = 7;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = false;
            this.okButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // AboutForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Assignment2.Properties.Resources.marble_rose_gold_background;
            this.ClientSize = new System.Drawing.Size(518, 333);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.titleLabel);
            this.Controls.Add(this.notepadPictureBox);
            this.Icon = Assignment2.Properties.Resources.notepad_512_ico;
            this.Name = "AboutForm";
            this.Text = "About";
            this.Load += new System.EventHandler(this.AboutForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.notepadPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox notepadPictureBox;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Button okButton;
    }
}