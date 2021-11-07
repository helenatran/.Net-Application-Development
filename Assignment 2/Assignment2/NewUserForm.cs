using System;
using System.Windows.Forms;

namespace Assignment2
{
    public partial class NewUserForm : Form
    {
        private UserList users = new UserList();
        public NewUserForm()
        {
            InitializeComponent();
            users.LoadAllUsers();
        }

        private void NewUserForm_Load(object sender, EventArgs e)
        {
            // Mask password with asteriks 
            passwordTextBox.PasswordChar = '*';
            reEnterPasswordTextBox.PasswordChar = '*';

            // Allow only letters to be entered for first and last name
            firstNameTextBox.KeyPress += Name_KeyPress;
            lastNameTextBox.KeyPress += Name_KeyPress;

            // Allow date of birth to be picked only until today (i.e.: no future date)
            dateOfBirthTimePicker.MaxDate = DateTime.Today;
        }

        // Method to validate the form by ensuring all fields are filled and the 2 password text boxes match
        private string FormValidation()
        {
            if (String.IsNullOrEmpty(usernameTextBox.Text) || String.IsNullOrEmpty(passwordTextBox.Text) ||
                String.IsNullOrEmpty(typeComboBox.Text) || String.IsNullOrEmpty(firstNameTextBox.Text) ||
                String.IsNullOrEmpty(lastNameTextBox.Text) || String.IsNullOrEmpty(reEnterPasswordTextBox.Text))
            {
                return "EmptyFields";
            }
            else if(passwordTextBox.Text != reEnterPasswordTextBox.Text)
            {
                return "DifferentPassword";
            }

            return "";
        }

        private void Name_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow only backspace and letter keys
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back);
        }

        private void SubmitButton_Click(object sender, EventArgs e)
        {
            string formValidation = FormValidation();
            string errorMessage = "";

            // Check if the username is unique
            if (users.UsernameExists(usernameTextBox.Text))
            {
                errorMessage = "This username already exists. Please try again with another username";
            }

            // Check for empty fields
            else if (formValidation == "EmptyFields")
            {
                errorMessage = "Not all fields are filled. Please fill all fields and try again";
            }

            // Check if the password entered in the 2 text boxes match
            else if (formValidation == "DifferentPassword")
            {
                errorMessage = "The passwords entered are different. Please ensure you enter the same password for both fields and try again";
            }
            
            if (errorMessage != "")
            {
                MessageBox.Show(errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string dateOfBirth = dateOfBirthTimePicker.Value.ToString("dd-MM-yyyy");
                User newUser = new User
                (
                    usernameTextBox.Text,
                    passwordTextBox.Text,
                    typeComboBox.Text,
                    firstNameTextBox.Text,
                    lastNameTextBox.Text,
                    dateOfBirth
                );

                // Close the NewUserForm and open a TextEditor with the newUser
                this.Hide();
                var textEditor = new TextEditor(newUser, "NewUser");
                textEditor.Closed += (s, args) => this.Close();
                textEditor.Show();
            }
        }

        // Return back to login screen when the user clicks on the Cancel button
        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            var loginScreen = new LoginScreen();
            loginScreen.Closed += (s, args) => this.Close();
            loginScreen.Show();
        }
    }
}
