using System;
using System.IO;
/*
 The given text file contains 5 set of user IDs and Password. 
 The file stores userID and password in the following format:
<User>, <password>
Hello, abc1234
Admin, admin,
User, user
Write a program to read a given text file and print the data about the user Id and password. 
Write appropriate exception handling code to handle exceptions which might occur while reading the text file.  
 */


namespace Week6LabProgram
{
    class Program2
    {
        static void Main(string[] args)
        {
            try
            {
                // Read the text file line wise
                string[] lines = System.IO.File.ReadAllLines("userIdDB.txt");

                // Split each line using "," as delimiter and print the values as
                // User Name: ------, Passowrd: .... "
                foreach (string line in lines)
                {
                    string[] splits = line.Split(',');
                    Console.WriteLine("UserName: {0}, Password: {1}", splits[0], splits[1]);
                }
                Console.ReadKey();
            }

            // Catch the FileNotFoundException exception
            catch(FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
            }

        }
    }
}

/*
Test Case:
Expeted Output: 

UserName: admin, Password: admin
UserName: normaluser, Password: user
UserName: Adam, Password: hello
UserName: George, Password: yessir
UserName: hacker, Password: nohack

    */