using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Assignment2
{
    public partial class TextEditor : Form
    {
        // Store track the name of the currently opened file
        private string currentlyOpenedFile = "";

        // Store the dialog result from a save file dialog
        private string saveDialogResult = "";

        // Store whether the user has done a logout action
        private bool logout = false;

        // Store the currently logged-in user
        private User currentUser = new User();

        // Store the list of users with existing account
        private UserList users = new UserList();
        public TextEditor(User currentUser, string previousScreen = "")
        {
            InitializeComponent();

            // Load the current user
            this.currentUser = currentUser;

            // Load all users
            users.LoadAllUsers();

            // If the user is a new user (i.e.: comes from creating an account),
            // Add the user into our list
            if (previousScreen == "NewUser")
            {
                users.AddUser(currentUser);
            }

            // Add text editor closing method 
            this.FormClosing += TextEditor_Closing;
        }
        private void TextEditor_Load(object sender, EventArgs e)
        {
            // Add the size numbers to the combo box to resize texts
            resizeToolStripComboBox.Items.AddRange(Enumerable.Range(8, 20 - 7).Cast<object>().ToArray());

            // Change the username label with the current user's username
            usernameToolStripLabel.Text = "Username: " + currentUser.Username;

            // Disable the text editor if the user is of type "View"
            if (currentUser.UserType == "View")
            {
                richTextBox1.ReadOnly = true;
            }
        }

        private void TextEditor_Shown(object sender, EventArgs e)
        {
            // Open a message box as welcome message
            string welcomeMessage = "Welcome to Helena's Text Editor! ";
            string userSpecificMessage = "";
            if (currentUser.UserType == "View")
            {
                userSpecificMessage = "Your account only allows to view texts. To start, click on the open file button and pick a file to read.";
            }
            else if (currentUser.UserType == "Edit")
            {
                userSpecificMessage = "Your account allows you to create, edit and view texts";
            }
            MessageBox.Show(welcomeMessage + userSpecificMessage, welcomeMessage);
        }

        private void TextEditor_Closing(object sender, FormClosingEventArgs e)
        {
            saveDialogResult = "";
            // If the file has been modified but not saved, and the text editor is not closing from logout
            // action, we display a message box to ask whether the user wants to save before closing the app
            if (((currentlyOpenedFile == "" && richTextBox1.TextLength != 0) || !IsFileUpToDate()) && !logout)
            {
                string messageBox = HandleUnsavedTexts(sender, e);
                if (messageBox == "cancel" || saveDialogResult == "cancel")
                {
                    e.Cancel = true;
                }
            }

            // Save back all users in the login.txt file
            users.StoreAllUsers();
        }

        // Method to handle when the text editor has been edited (i.e.: user has typed a text)
        // but the file has never been saved
        private string HandleUnsavedTexts(object sender, EventArgs e)
        {
            DialogResult savingDr = MessageBox.Show("Your file is not saved! Would you " +
                    "like to save it?", "Saving", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            if (savingDr == DialogResult.Yes)
            {
                Save_Click(sender, e);
            }
            else if (savingDr == DialogResult.Cancel)
            {
                return "cancel";
            }
            return "";
        }

        // Method to check whether the currentlyOpenedFile is at the latest version
        // I.e.: what is saved is the same as what is in the text editor
        private bool IsFileUpToDate()
        {
            // If there is no currently opened file, return true
            if (currentlyOpenedFile == "")
            {
                return true;
            }

            // Get the texts from the file using a rich text box
            RichTextBox fileTextBox = new RichTextBox();
            string fileContent = File.ReadAllText(currentlyOpenedFile);

            // Populate  fileTextBox with the file content
            fileTextBox.Rtf = fileContent;

            // If the file and text editor's texts are of same length, then the file is up to date
            if (fileTextBox.TextLength == richTextBox1.TextLength)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void Cut_Click(object sender, EventArgs e)
        {
            richTextBox1.Cut();
        }

        private void Copy_Click(object sender, EventArgs e)
        {
            richTextBox1.Copy();
        }

        private void Paste_Click(object sender, EventArgs e)
        {
            richTextBox1.Paste();
        }

        private void New_Click(object sender, EventArgs e)
        {
            string messageBox = "";
            saveDialogResult = "";

            // If the texts in the text editor has not been saved yet, handle the unsaved texts
            if ((currentlyOpenedFile == "" && richTextBox1.TextLength != 0) || !IsFileUpToDate())
            {
                messageBox = HandleUnsavedTexts(sender, e);
            }
            
            // If the user has not clicked "cancel" for both the message box and the save file dialog
            // Then we can proceed (i.e.: clear the rich text box)
            if (messageBox != "cancel" && saveDialogResult != "cancel")
            {
                richTextBox1.Clear();

                // Clear the currently stored filePath
                currentlyOpenedFile = "";
            }
        }

        private void BoldToolStripButton_Click(object sender, EventArgs e)
        {
            // Only allow to make the texts bold if the user is of type "Edit"
            if (currentUser.UserType == "Edit")
            {
                Font newFont, oldFont = richTextBox1.SelectionFont;

                if (oldFont.Bold)
                {
                    newFont = new Font(oldFont, oldFont.Style & ~FontStyle.Bold);
                }
                else
                {
                    newFont = new Font(oldFont, oldFont.Style | FontStyle.Bold);
                }

                richTextBox1.SelectionFont = newFont;
                richTextBox1.Focus();
            }
        }

        private void ItalicToolStripButton_Click(object sender, EventArgs e)
        {
            // Only allow to make the texts italic if the user is of type "Edit"
            if (currentUser.UserType == "Edit")
            {
                Font newFont, oldFont = richTextBox1.SelectionFont;

                if (oldFont.Italic)
                {
                    newFont = new Font(oldFont, oldFont.Style & ~FontStyle.Italic);
                }
                else
                {
                    newFont = new Font(oldFont, oldFont.Style | FontStyle.Italic);
                }

                richTextBox1.SelectionFont = newFont;
                richTextBox1.Focus();
            }
        }

        private void UnderlineToolStripButton_Click(object sender, EventArgs e)
        {
            // Only allow to underline texts if the user is of type "Edit"
            if (currentUser.UserType == "Edit")
            {
                Font newFont, oldFont = richTextBox1.SelectionFont;

                if (oldFont.Underline)
                {
                    newFont = new Font(oldFont, oldFont.Style & ~FontStyle.Underline);
                }
                else
                {
                    newFont = new Font(oldFont, oldFont.Style | FontStyle.Underline);
                }

                richTextBox1.SelectionFont = newFont;
                richTextBox1.Focus();
            }
        }

        private void ResizeToolStripComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Only allow to resize the texts if the user is of type "Edit"
            if (currentUser.UserType == "Edit")
            {
                Font newFont, oldFont = richTextBox1.SelectionFont;
                int newSize = Convert.ToInt32(resizeToolStripComboBox.SelectedItem);
                newFont = new Font(oldFont.FontFamily, newSize, oldFont.Style);
                richTextBox1.SelectionFont = newFont;
                richTextBox1.Focus();
            }
        }

        private void Save_Click(object sender, EventArgs e)
        {
            // If the file has previously been saved (i.e.: currentlyOpenedFile not empty)
            // We save the texts from the text editor to the corresponding file
            if (currentlyOpenedFile != "")
            {
                richTextBox1.SaveFile(currentlyOpenedFile);
            }

            // If the file has not been previously saved (i.e.: currentlyOpenedFile is empty)
            // Then we call the save as method
            else
            {
                SaveAs_Click(sender, e);
            }
        }

        // Source: https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.richtextbox.savefile?view=windowsdesktop-5.0#System_Windows_Forms_RichTextBox_SaveFile_System_String_
        private void SaveAs_Click(object sender, EventArgs e)
        {
            // Create a SaveFileDialog to request a path and file name to save to.
            SaveFileDialog saveFile1 = new SaveFileDialog();

            // Initialize the SaveFileDialog to specify the RTF extention for the file.
            saveFile1.DefaultExt = "*.rtf";
            saveFile1.Filter = "RTF Files|*.rtf";

            // Determine whether the user selected a file name from the saveFileDialog
            if (saveFile1.ShowDialog() == DialogResult.OK && saveFile1.FileName.Length > 0)
            {
                // Save the contents of the RichTextBox into the file.
                richTextBox1.SaveFile(saveFile1.FileName);
                currentlyOpenedFile = saveFile1.FileName;
            }
            else 
            {
                saveDialogResult = "cancel";
            }
        }

        // Source: https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.richtextbox.loadfile?view=windowsdesktop-5.0
        private void OpenFile_Click(object sender, EventArgs e)
        {
            // Reset Save dialog result
            saveDialogResult = "";

            // Ask user whether they want to save their file if they have not done it yet
            string messageBox = "";
            if ((currentlyOpenedFile == "" && richTextBox1.TextLength != 0) || !IsFileUpToDate())
            {
                messageBox = HandleUnsavedTexts(sender, e);
            }

            // If the user has not clicked cancel for both the message box and the save file dialog
            // Then we can proceed and open a file
            if (messageBox != "cancel" && saveDialogResult != "cancel")
            {
                // Create an OpenFileDialog to request a file to open.
                OpenFileDialog openFile1 = new OpenFileDialog();

                // Initialize the OpenFileDialog to look for RTF files.
                openFile1.DefaultExt = "*.rtf";
                openFile1.Filter = "RTF Files|*.rtf";

                // Determine whether the user selected a file from the OpenFileDialog.
                if (openFile1.ShowDialog() == DialogResult.OK && openFile1.FileName.Length > 0)
                {
                    // Load the contents of the file into the RichTextBox.
                    richTextBox1.LoadFile(openFile1.FileName);

                    // Store the file path of the currently opened file
                    currentlyOpenedFile = openFile1.FileName;
                }
            }
        }

        private void About_Click(object sender, EventArgs e)
        {
            var aboutForm = new AboutForm();
            aboutForm.Show();
        }

        private void LogoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // If the file has not been saved and the text box has been edited (i.e.: user has
            // typed a text), then handle the unsaved texts
            string messageBox = "";
            saveDialogResult = "";
            if ((currentlyOpenedFile == "" && richTextBox1.TextLength != 0) || !IsFileUpToDate())
            {
                messageBox = HandleUnsavedTexts(sender, e);
            }

            // If the user has not clicked cancel for both the message box and the save file dialog,
            // then we can proceed and logout
            if (messageBox != "cancel" && saveDialogResult != "cancel")
            {
                // Save back all users in the login.txt file
                users.StoreAllUsers();

                // Hide the text editor window and show the login window
                this.Hide();
                var loginScreen = new LoginScreen();
                loginScreen.Closed += (s, args) => this.Close();
                loginScreen.Show();

                // Make logout true to show that the user has done a logout action
                logout = true;
            }
        }
    }
}
