using System;
using System.Drawing;
using System.Windows.Forms;

namespace Assignment2
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
        }

        private void AboutForm_Load(object sender, EventArgs e)
        {
            // Make the background color of the picture box transparent
            notepadPictureBox.BackColor = Color.Transparent;
        }

        // Close the application when the OK button is clicked
        private void OKButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
