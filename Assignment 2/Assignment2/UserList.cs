using System.Collections.Generic;
using System.IO;

namespace Assignment2
{
    class UserList
    {
        // List of all users with an account
        private List<User> users;

        // The currently logged-in user
        public User CurrentUser { get; set; }

        public UserList()
        {
            users = new List<User>();
        }

        // Method to load all users from the login.txt file
        public void LoadAllUsers()
        {
            StreamReader fileContent = new StreamReader("login.txt");

            // Read the StremReader until the last line
            while (!fileContent.EndOfStream)
            {
                User user = new User();

                // Read each line from the file
                string line = fileContent.ReadLine();

                // Load the user detail from the file to the respective fields
                user.LoadUserInfo(line);

                // Add the user to our users list
                users.Add(user);
            }

            fileContent.Close();
        }

        // Method to store all users in the login.txt file
        public void StoreAllUsers()
        {
            using StreamWriter sw = new StreamWriter("login.txt");
            foreach (User user in users)
            {
                // Write a line for each user in our users list
                sw.WriteLine(user.ToString());
            }
        }

        // Method to verify the user credentials are correct
        // I.e.: the user credentials are in the login.txt file
        public bool VerifyUserCredentials(string username, string password)
        {
            foreach(User user in users)
            {
                if (user.AreCredentialsCorrect(username, password))
                {
                    // If the credentials are correct during the login process
                    // Then this user is our currently logged-in user
                    CurrentUser = user;
                    return true;
                }
            }
            return false;
        }

        // Method to add new user to the list of users
        public void AddUser(User newUser)
        {
            users.Add(newUser);
        }

        // Method to check whether the username already exists
        public bool UsernameExists(string username)
        {
            foreach(User user in users)
            {
                if (user.Username == username)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
