namespace Assignment2
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string UserType{ get; set; }
        public string FirstName{ get; set; }
        public string LastName{ get; set; }
        public string DateOfBirth{ get; set; }

        public User()
        {
            Username = "";
            Password = "";
            UserType = "";
            FirstName = "";
            LastName = "";
            DateOfBirth = "";
        }

        public User(string username, string password, string userType, string firstName, string lastName,
            string dateOfBirth)
        {
            Username = username;
            Password = password;
            UserType = userType;
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
        }

        // Method to extract individual information fields from a user
        // given a string file line
        public void LoadUserInfo(string fileLine)
        {
            string[] userInfo = fileLine.Split(',');

            // Assign values to respective properties
            Username = userInfo[0];
            Password = userInfo[1];
            UserType = userInfo[2];
            FirstName = userInfo[3];
            LastName = userInfo[4];
            DateOfBirth = userInfo[5];
        }

        // Method to check the credentials given are correct
        // I.e.: the username and password match with the user info
        public bool AreCredentialsCorrect(string username, string password)
        {
            if (Username == username && Password == password)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Method to override ToString() to display user's info in our format (using comma)
        public override string ToString()
        {
            return Username + "," + Password + "," + UserType + "," + FirstName + "," + LastName +
                "," + DateOfBirth;
        }
    }
}
