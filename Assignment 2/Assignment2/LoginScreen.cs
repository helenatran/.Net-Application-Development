using System;
using System.Windows.Forms;

namespace Assignment2
{
    public partial class LoginScreen : Form
    {
        private UserList users = new UserList();

        public LoginScreen()
        {
            InitializeComponent();
        }

        private void LoginScreen_Load(object sender, EventArgs e)
        {
            // Mask password with asteriks 
            passwordTextBox.PasswordChar = '*';

            // Load all users
            users.LoadAllUsers();
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            // Check whether the user's credentials are correct
            // If they are, then close the login window and open the text editor window
            if (users.VerifyUserCredentials(usernameTextBox.Text, passwordTextBox.Text))
            {
                this.Hide();
                var textEditor = new TextEditor(users.CurrentUser);
                textEditor.Closed += (s, args) => this.Close();
                textEditor.Show();
            }
            else
            {
                // Clear the username and password text boxes
                usernameTextBox.Text = "";
                passwordTextBox.Text = "";
                
                // Display an error message box
                MessageBox.Show("Invalid credentials. Try again!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void NewUserButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            var newUserForm = new NewUserForm();
            newUserForm.Closed += (s, args) => this.Close();
            newUserForm.Show();
        }
    }
}
