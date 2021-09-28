using System;
using System.Text;
using System.Globalization;
using System.Text.RegularExpressions;
using System.IO;
using System.Collections.Generic;
using System.Net.Mail;

namespace Assignment1
{
    // Screen parameters are the parameters defining the width, height and starting row and column of a screen
    class ScreenParameters
    {
        public int NumberOfLine { get; set; }
        public int FormWidth { get; set; }
        public int StartRow { get; set; }
        public int StartCol { get; set; }

        public ScreenParameters(int numberOfLines, int formWidth, int startRow, int startCol)
        {
            NumberOfLine = numberOfLines;
            FormWidth = formWidth;
            StartRow = startRow;
            StartCol = startCol;
        }

        // Fixed parameters for login screen
        public static ScreenParameters loginScreenParams = new ScreenParameters(9, 50, 2, 10);

        // Fixed parameters for main menu screen
        public static ScreenParameters mainMenuScreenParams = new ScreenParameters(13, 50, 2, 10);

        // Fixed parameters for create account screen
        public static ScreenParameters createAcctScreenParams = new ScreenParameters(12, 50, 2, 10);

        // Fixed parameters for search account screen
        public static ScreenParameters searchAccountScreenParams = new ScreenParameters(8, 50, 2, 10);

        // Fixed parameters for deposit and withdraw screens
        public static ScreenParameters depositAndWithdrawScreenParams = new ScreenParameters(9, 50, 2, 10);
    }

    // Account class consists of a user's bank account
    class Account
    {
        public int AccountNumber { get; set; }
        public float Balance { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public int Phone { get; set; }
        public string Email { get; set; }

        // Constructor for account given as parameters each account detail
        public Account(float balance, string firstName, string lastName, string address, int phone, string email)
        {
            AccountNumber = CreateUniqueAccountNumber();
            Balance = balance;
            FirstName = firstName;
            LastName = lastName;
            Address = address;
            Phone = phone;
            Email = email;
        }

        // Constructor for account given a string[] of account details
        public Account(string[] accountDetails)
        {
            // The account details must contain the 7 details required to create a user account
            if (accountDetails.Length != 7)
            {
                throw new IndexOutOfRangeException("Index is out of range. Account details must contain the 7 user details");
            }

            FirstName = accountDetails[0];
            LastName = accountDetails[1];
            Address = accountDetails[2];
            Phone = Convert.ToInt32(accountDetails[3]);
            Email = accountDetails[4];
            AccountNumber = Convert.ToInt32(accountDetails[5]);
            Balance = float.Parse(accountDetails[6]);
        }

        // Return the user first name and last name as a string
        public string GetFullName()
        {
            return FirstName + " " + LastName;
        }

        // Return the user details (except account number and balance) as a string[]
        public string[] GetUserDetails()
        {
            string[] userDetails = new string[5] {
                FirstName,
                LastName,
                Address,
                Phone.ToString(),
                Email
            };

            return userDetails;
        }

        // Create a unique account number using a random number generator
        private int CreateUniqueAccountNumber()
        {
            // Create a random unique number of 6 digits as account number
            Random generator = new Random();
            string accountNumber = generator.Next(0, 1000000).ToString("D6");

            // Check if the account number already exists
            // If it does, then generate a new account number
            while (File.Exists(accountNumber + ".txt"))
            {
                accountNumber = generator.Next(0, 1000000).ToString("D6");
            }

            return Convert.ToInt32(accountNumber);
        }
    }

    // User Interface contain all the screens and relevant methods to make them work
    class UserInterface
    {
        protected static int origRow;
        protected static int origCol;

        // Method to create the login screen
        public void LoginScreen(ScreenParameters loginScreenParams)
        {
            int numberOfLines = loginScreenParams.NumberOfLine;
            int formWidth = loginScreenParams.FormWidth;
            int startRow = loginScreenParams.StartRow;
            int startCol = loginScreenParams.StartCol;

            string[] loginWindowFields = { "Username: ", "Password: " };
            int[,] loginFieldsPositions = new int[2, 2];
            string[] loginUserInputs = new string[2];
            
            // Create the form with borders 
            CreateForm(numberOfLines, formWidth, startRow, startCol, true);

            // Display the form headings and field names
            WriteAt("WELCOME TO SIMPLE BANKING SYSTEM", startCol + 9, startRow + 1);
            WriteAt("LOGIN TO START", startCol + 18, startRow + 3);
            int item = DisplayFieldNamesWithRepositioning(loginWindowFields, loginFieldsPositions, startCol, startRow);

            do
            {
                // Get User Inputs
                for (int field = 0; field < item; field++)
                {
                    Console.SetCursorPosition(loginFieldsPositions[field, 0], loginFieldsPositions[field, 1]);
                    // If the input is for the Password field, we mask the input with asterisks
                    if (field == 1)
                    {
                        loginUserInputs[field] = GeneralFunctions.MaskPasswordString();
                    }
                    else
                    {
                        loginUserInputs[field] = Console.ReadLine();
                    }
                }

                // Check if the credentials are correct by reading the login.txt file
                try
                {
                    string[] loginFileLines = File.ReadAllLines("login.txt");
                    foreach (string line in loginFileLines)
                    {
                        if (line.Contains(loginUserInputs[0]) && line.Contains(loginUserInputs[1]))
                        {
                            WriteAt("Valid Credentials!", startCol, numberOfLines + 2);
                            MainMenuScreen(ScreenParameters.mainMenuScreenParams);
                        }
                    }
                }
                // Catch the FileNotFoundException in case the file cannot be found
                catch(FileNotFoundException e)
                {
                    WriteAt("Impossible to check the credentials. The login file cannot be found", startCol, numberOfLines + 2);
                    WriteAt("Add the login.txt file and press enter to try again", startCol, numberOfLines + 3);
                    WriteAt("Here are the error logs: " + e, startCol, numberOfLines + 5);
                    
                    // Reset the login user inputs
                    Array.Clear(loginUserInputs, 0, loginUserInputs.Length);
                    Console.ReadKey();

                    // Create a new login screen
                    LoginScreen(ScreenParameters.loginScreenParams);
                }
                
                // If login.txt exists but credentials are invalid, allow user to try again
                WriteAt("Invalid Credentials", startCol, numberOfLines + 2);
                WriteAt("Please press enter to try again", startCol, numberOfLines + 3);

                // Reset the login user inputs
                Array.Clear(loginUserInputs, 0, loginUserInputs.Length);
                Console.ReadKey();

                // Create a new login screen
                LoginScreen(ScreenParameters.loginScreenParams);
            } while (true);
        }

        // Method to create the main menu screen
        public void MainMenuScreen(ScreenParameters mainMenuScreenParams)
        {
            int numberOfLines = mainMenuScreenParams.NumberOfLine;
            int formWidth = mainMenuScreenParams.FormWidth;
            int startRow = mainMenuScreenParams.StartRow;
            int startCol = mainMenuScreenParams.StartCol;

            string[] mainMenuWindowFields = {
                "1. Create a new account",
                "2. Search for an account",
                "3. Deposit",
                "4. Withdraw",
                "5. A/C Statement",
                "6. Delete an account",
                "7. Exit"
            };

            // Create the form with borders 
            CreateForm(numberOfLines, formWidth, startRow, startCol, true);

            // Display the form heading and field names
            WriteAt("WELCOME TO SIMPLE BANKING SYSTEM", startCol + 9, startRow + 1);
            int item = DisplayFieldNames(mainMenuWindowFields, startCol, startRow);

            // Add a divider
            for (int col = 1; col < formWidth - 1; col++)
            {
                WriteAt("═", startCol + col, startRow + 3 + item);
            }

            WriteAt("Enter your choice (1-7): ", startCol + 6, startRow + 4 + item);
            string mainMenuUserInput = Console.ReadLine();

            // Verify that the input choice is of appropriate format
            // I.e.: An integer between 1 and 7
            if (int.TryParse(mainMenuUserInput, out int menuChoiceNumber) && (menuChoiceNumber >= 1 && menuChoiceNumber <= 7))
            {
                if (menuChoiceNumber == 1)
                {
                    CreateAccountScreen(ScreenParameters.createAcctScreenParams);
                }
                if (menuChoiceNumber == 2)
                {
                    SearchAccountScreen(ScreenParameters.searchAccountScreenParams, "SearchAccount");
                }
                if (menuChoiceNumber == 3)
                {
                    DepositScreen(ScreenParameters.depositAndWithdrawScreenParams);
                }
                if (menuChoiceNumber == 4)
                {
                    WithdrawScreen(ScreenParameters.depositAndWithdrawScreenParams);
                }
                if (menuChoiceNumber == 5)
                {
                    SearchAccountScreen(ScreenParameters.searchAccountScreenParams, "AccountStatement");
                }
                if (menuChoiceNumber == 6)
                {
                    SearchAccountScreen(ScreenParameters.searchAccountScreenParams, "DeleteAccount");
                }
                if (menuChoiceNumber == 7)
                {
                    LoginScreen(ScreenParameters.loginScreenParams);
                }
            }
            else
            {
                WriteAt("Invalid choice. You must enter a number between 1 and 7", startCol, numberOfLines + 2);
                WriteAt("Please press enter to try again", startCol, numberOfLines + 3);
                Console.ReadKey();
                MainMenuScreen(ScreenParameters.mainMenuScreenParams);
            }
        }

        // Method to create the Create account screen
        public void CreateAccountScreen(ScreenParameters createAccountScreenParams)
        {
            int numberOfLines = createAccountScreenParams.NumberOfLine;
            int formWidth = createAccountScreenParams.FormWidth;
            int startRow = createAccountScreenParams.StartRow;
            int startCol = createAccountScreenParams.StartCol;

            string[] createAcctWindowFields = {
                "First Name: ",
                "Last Name: ",
                "Address: ",
                "Phone: ",
                "Email: "
            };
            int[,] createAcctFieldsPositions = new int[5, 5];
            string[] createAcctUserInputs = new string[5];

            // Create the form with borders 
            CreateForm(numberOfLines, formWidth, startRow, startCol, true);

            // Display the form headings and field names
            WriteAt("CREATE A NEW ACCOUNT", startCol + 15, startRow + 1);
            WriteAt("ENTER THE DETAILS", startCol + 16, startRow + 3);
            int item = DisplayFieldNamesWithRepositioning(createAcctWindowFields, createAcctFieldsPositions, startCol, startRow);

            do
            {
                // Get User Inputs
                for (int field = 0; field < item; field++)
                {
                    Console.SetCursorPosition(createAcctFieldsPositions[field, 0], createAcctFieldsPositions[field, 1]);
                    try
                    {
                        createAcctUserInputs[field] = Console.ReadLine();
                    }
                    // Catch IndexOutOfRangeException when trying to insert more than 5 items in createAcctUserInputs
                    catch (IndexOutOfRangeException e)
                    {
                        WriteAt(e.Message, startCol, numberOfLines + 2);
                        WriteAt("Make sure only the 5 details are being stored in string[] createAcctUserInputs", startCol, numberOfLines + 3);
                        WriteAt("Please press any key to come back to the main menu", startCol, numberOfLines + 4);
                        Console.ReadKey();
                        MainMenuScreen(ScreenParameters.mainMenuScreenParams);
                    }
                    
                }

                // Verify the phone number and email provided are of appropriate format
                VerifyAccount(startCol, numberOfLines, createAcctUserInputs[3], createAcctUserInputs[4]);

                WriteAt("Your account has been created!", startCol, numberOfLines + 2);
                WriteAt("Your account details will be sent to you by email", startCol, numberOfLines + 3);

                // Create the userAccount object
                Account newAccount = new Account(0, createAcctUserInputs[0], createAcctUserInputs[1],
                    createAcctUserInputs[2], Convert.ToInt32(createAcctUserInputs[3]), createAcctUserInputs[4]);

                // Prepare message body for email
                string messageBody = "Account details of " + newAccount.GetFullName() + "\n\n";

                // Store the account details in a file
                FileStream newAccountFile = new FileStream(newAccount.AccountNumber + ".txt", FileMode.Create, FileAccess.ReadWrite);
                
                string[] newAccountDetails = new String[7];
                // Characters to be trimmed when inserting the details in the file
                char[] charsToTrim = { ':', ' '};
                for (int loopVar = 0; loopVar < createAcctWindowFields.Length; loopVar++)
                {
                    // Add the details in newAccountDetails[] in appropriate format for the file
                    newAccountDetails[loopVar] = createAcctWindowFields[loopVar].Trim(charsToTrim) + "|" + createAcctUserInputs[loopVar];
                    
                    // Append the messageBody string for the email
                    messageBody += createAcctWindowFields[loopVar] + createAcctUserInputs[loopVar] + "\n";
                }
                newAccountDetails[5] = "AccountNo|" + newAccount.AccountNumber;
                newAccountDetails[6] = "Balance|0";

                newAccountFile.Close();

                // Write the new account details to the corresponding file
                File.WriteAllLines(newAccount.AccountNumber + ".txt", newAccountDetails);

                string displayAcctNumber = "Your account number is " + newAccount.AccountNumber;
                WriteAt(displayAcctNumber, startCol, numberOfLines + 4);

                WriteAt("Please press enter to return to the main menu", startCol, numberOfLines + 6);

                // Add account number to the message body of the email
                messageBody += "\n" + displayAcctNumber;
                // Send account details by email
                GeneralFunctions.SendEmail(newAccount.Email, newAccount.GetFullName(), newAccount.AccountNumber, messageBody, "Account Details");

                Console.ReadKey();
                MainMenuScreen(ScreenParameters.mainMenuScreenParams);

            } while (true);
        }

        // Method to create the Search account screen
        public void SearchAccountScreen(ScreenParameters searchAccountScreenParams, string currentScreen)
        {
            int numberOfLines = searchAccountScreenParams.NumberOfLine;
            int formWidth = searchAccountScreenParams.FormWidth;
            int startRow = searchAccountScreenParams.StartRow;
            int startCol = searchAccountScreenParams.StartCol;

            // Create the form with borders 
            CreateForm(numberOfLines, formWidth, startRow, startCol, true);

            // Change title and the statement when the account is found depending on whether the current screen is the
            // Search account screen, the Account Statement screen or the Delete account screen
            string title = "", accountFoundStatement = "";
            if (currentScreen == "SearchAccount")
            {
                title = "SEARCH AN ACCOUNT";
                accountFoundStatement = "You can find your account details below: ";
            }
            if (currentScreen == "AccountStatement")
            {
                title = "ACCOUNT STATEMENT";
                accountFoundStatement = "Your account statement is displayed below: ";
            }
            if (currentScreen == "DeleteAccount")
            {
                title = "DELETE AN ACCOUNT";
                accountFoundStatement = "Here are the details of the account you wish to delete: ";
            }

            // Display the form headings
            WriteAt(title, startCol + 16, startRow + 1);
            WriteAt("ENTER YOUR ACCOUNT NUMBER", startCol + 12, startRow + 3);

            WriteAt("Account Number: ", startCol + 6, startRow + 5);
            string userInputAcctNumber = Console.ReadLine();

            // Verify the account number is of appropriate format (integer of no more than 10 characters)
            if (VerifyAccountNumber(userInputAcctNumber, startCol, numberOfLines))
            {
                // Check if the account exists
                string fileName = userInputAcctNumber + ".txt";
                if (File.Exists(fileName))
                {
                    WriteAt("Account found! " + accountFoundStatement, startCol, numberOfLines + 3);

                    // Retrieve the user information
                    string[] userDetails = new string[7];
                    List<string> transactions = new List<string>();
                    string[] userDetailsFileLines = File.ReadAllLines(fileName);

                    for (int loopVar = 0; loopVar < userDetailsFileLines.Length; loopVar++)
                    {
                        // The first 7 lines are the account detais. We are only interested in the details, not the field name
                        if (loopVar <= 6)
                        {
                            string[] splittedUserDetails = userDetailsFileLines[loopVar].Split('|');
                            userDetails[loopVar] = splittedUserDetails[1];
                        }
                        // The rest are the transactions so we store the whole line in the transactions list
                        else
                        {
                            transactions.Add(userDetailsFileLines[loopVar]);
                        }
                    }
                    try
                    {
                        Account userAccount = new Account(userDetails);
                        // Create the account details screen
                        // The screen will be different based on the current screen provided (SearchAccount, AccountStatement or DeleteAccount)
                        AccountDetailsScreen(new ScreenParameters(12, 50, numberOfLines + 4, 10), userAccount, transactions, currentScreen);
                    }
                    catch(IndexOutOfRangeException e)
                    {
                        WriteAt(new string(' ', Console.WindowWidth), startCol, numberOfLines + 3);
                        WriteAt(e.Message, startCol, numberOfLines + 3);
                        WriteAt("Ensure the userDetails string[] in SearchAccountScreen contains the 7 elements", startCol, numberOfLines + 4);
                        WriteAt("Press enter to go back to the main menu", startCol, numberOfLines + 5);
                        Console.ReadKey();
                        MainMenuScreen(ScreenParameters.mainMenuScreenParams);
                    }
                }
                else
                {
                    // If the file/account does not exist, ask the user a yes/no question about whether they want try again
                    DisplayYesNoQuestion(startCol, numberOfLines + 2, currentScreen);
                }
            }
            // If the account number is not provided in an appropriate format, the VerifyAccountNumber() function displays 
            // an error message. Then, we create a new SearchAccountScreen to allow the user to try again
            else
            {
                Console.ReadKey();
                SearchAccountScreen(ScreenParameters.searchAccountScreenParams, currentScreen);
            }
        }

        // Method to create the Account details screen
        public void AccountDetailsScreen(ScreenParameters accountDetailsScreenParams, Account userAccount, List<string> transactions, 
            string currentScreen)
        {
            int numberOfLines = accountDetailsScreenParams.NumberOfLine;
            int formWidth = accountDetailsScreenParams.FormWidth;
            int startRow = accountDetailsScreenParams.StartRow;
            int startCol = accountDetailsScreenParams.StartCol;

            string[] accountDetailsWindowFields = {
                "Account Number: ",
                "Account Balance: $",
                "First Name: ",
                "Last Name: ",
                "Address: ",
                "Phone: ",
                "Email: "
            };

            string[] userDetails = userAccount.GetUserDetails();

            // The account detail screen has a different title depending on the currentScreen
            // The finalQuestionOption allows us to ask the corresponding Yes/No question at the end
            string title = "", finalQuestionOption = "", emailBody = "";
            if (currentScreen == "SearchAccount")
            {
                title = "ACCOUNT DETAILS";
                finalQuestionOption = "SuccessfulSearch";
            }
            if (currentScreen == "DeleteAccount")
            {
                title = "ACCOUNT DETAILS";
                finalQuestionOption = "SuccessfulSearchForDelete";
            }
            if (currentScreen == "AccountStatement")
            {
                title = "ACCOUNT STATEMENT";
                finalQuestionOption = "SuccessfulStatement";
            }
            
            // The number of last transactions we want to display (up to 5)
            int numberOfTransactions = 5;
            List<string> lastTransactions = new List<string>();

            // Display first the account number and balance
            accountDetailsWindowFields[0] += userAccount.AccountNumber;
            accountDetailsWindowFields[1] += userAccount.Balance;

            // Then display the rest of the account details
            int count = 0;
            for (int loopVar = 2; loopVar < accountDetailsWindowFields.Length; loopVar++)
            {
                accountDetailsWindowFields[loopVar] += userDetails[count];
                count++;
            }

            // Create the form with borders 
            CreateForm(numberOfLines, formWidth, startRow, startCol, false);

            // Display the form headings and the fields
            WriteAt(title, startCol + 16, startRow + 1);
            int item = DisplayFieldNames(accountDetailsWindowFields, startCol, startRow);

            if (currentScreen == "AccountStatement")
            {
                string transactionSentence = "";
                // Display the last transactions (up to 5 transactions) if there is any
                if (transactions.Count > 0)
                {
                    // If there is less than 5 transactions recorded, change the number of transactions which will be displayed
                    if (transactions.Count < 5)
                    {
                        numberOfTransactions = transactions.Count;
                    }

                    if (numberOfTransactions == 1)
                    {
                        transactionSentence = "Last transaction: ";
                    }
                    else
                    {
                        transactionSentence = "Last " + numberOfTransactions + " transactions: ";
                    }
                    
                    WriteAt(transactionSentence, startCol, startRow + 5 + item);
                    item++;
                    // Write the last transaction(s) (up to 5)
                    for (int loopVar = transactions.Count - 1; loopVar >= transactions.Count - numberOfTransactions; loopVar--)
                    {
                        WriteAt(transactions[loopVar], startCol, startRow + 5 + item);
                        lastTransactions.Add(transactions[loopVar]);
                        item++;
                    }
                }

                // If the current screen is AccountStatement, then prepare the email body
                emailBody = "Account Statement of " + userAccount.GetFullName() + "\n\n";
                // Add the account details in the email
                foreach (string accountDetail in accountDetailsWindowFields)
                {
                    emailBody = emailBody + accountDetail + "\n";
                }
                emailBody += "\n" + transactionSentence + "\n";
                // Add the last transaction(s) in the email (up to 5)
                foreach (string transaction in lastTransactions)
                {
                    emailBody = emailBody + transaction + "\n";
                }
            }

            // Depending on the current screen, this might display the option to search another account
            // or to email an account statement
            DisplayYesNoQuestion(startCol, startRow + item + 6, finalQuestionOption, userAccount, emailBody);
        }

        // Method to create the Deposit screen
        public void DepositScreen(ScreenParameters depositScreenParams)
        {
            int numberOfLines = depositScreenParams.NumberOfLine;
            int formWidth = depositScreenParams.FormWidth;
            int startRow = depositScreenParams.StartRow;
            int startCol = depositScreenParams.StartCol;

            string[] depositWindowFields = { "Account Number: ", "Amount: $" };
            int[,] depositFieldsPositions = new int[2, 2];
            string[] depositUserInputs = new string[2];

            // Create the form with borders 
            CreateForm(numberOfLines, formWidth, startRow, startCol, true);

            // Display the form headings
            WriteAt("DEPOSIT", startCol + 21, startRow + 1);
            WriteAt("ENTER THE DETAILS", startCol + 16, startRow + 3);
            int item = DisplayFieldNamesWithRepositioning(depositWindowFields, depositFieldsPositions, startCol, startRow);

            do
            {
                // Get User Inputs
                for (int field = 0; field < item; field++)
                {
                    Console.SetCursorPosition(depositFieldsPositions[field, 0], depositFieldsPositions[field, 1]);
                    depositUserInputs[field] = Console.ReadLine();

                    // Check that the account number is correct
                    if (field == 0)
                    {
                        DisplayCheckingForAccount(depositUserInputs[0], startCol, numberOfLines, "DepositScreen");
                    }
                    // Check that amount is of appropriate format (integer greather than 0).
                    if (field == 1)
                    {
                        // Clear any potential previous error messages
                        WriteAt(new string(' ', Console.WindowWidth), startCol, numberOfLines + 2);

                        if (float.TryParse(depositUserInputs[1], out float amount) && amount > 0)
                        {
                            // Add the deposit amount to the user's balance
                            string accountFileName = depositUserInputs[0] + ".txt";
                            // File existence was checked previously, so we can safely read the file
                            string[] userDetails = File.ReadAllLines(accountFileName);
                            float newBalance = 0;
                            for (int loopVar = 0; loopVar < userDetails.Length; loopVar++)
                            {
                                // Search for the line in the file containing the Balance
                                if (userDetails[loopVar].Contains("Balance"))
                                {
                                    // Split the line between the field name and the balance
                                    string[] splittedUserDetails = userDetails[loopVar].Split('|');

                                    // Add the new amount to the existing balance
                                    newBalance = float.Parse(splittedUserDetails[1]) + amount;
                                    userDetails[loopVar] = "Balance|" + newBalance;
                                }
                            }
                            // Update the file with the new balance
                            File.WriteAllLines(accountFileName, userDetails);

                            // Add the deposit transaction at the end of the file
                            DateTime today = DateTime.Today;
                            string transaction = today.ToString("dd.MM.yyyy") + "|Deposit|" + amount + "|" 
                                + newBalance + Environment.NewLine;
                            File.AppendAllText(accountFileName, transaction);

                            // Display message for successful deposit
                            DisplayYesNoQuestion(startCol, numberOfLines + 2, "SuccessfulDeposit");
                        }
                        // If the amount is not of appropriate format, display an error message and allow the user to try again
                        else
                        {
                            WriteAt("Invalid amount. You must enter a number greather than 0", startCol, numberOfLines + 2);
                            WriteAt("Please press enter to try again", startCol, numberOfLines + 3);
                            Console.ReadKey();
                            DepositScreen(ScreenParameters.depositAndWithdrawScreenParams);
                        }
                    }
                }
            } while (true);
        }

        // Method to create the Withdraw screen
        public void WithdrawScreen(ScreenParameters withdrawScreenParams)
        {
            int numberOfLines = withdrawScreenParams.NumberOfLine;
            int formWidth = withdrawScreenParams.FormWidth;
            int startRow = withdrawScreenParams.StartRow;
            int startCol = withdrawScreenParams.StartCol;

            string[] withdrawWindowFields = { "Account Number: ", "Amount: $" };
            int[,] withdrawFieldsPositions = new int[2, 2];
            string[] withdrawUserInputs = new string[2];

            // Create the form with borders 
            CreateForm(numberOfLines, formWidth, startRow, startCol, true);

            // Display the form headings
            WriteAt("WITHDRAW", startCol + 21, startRow + 1);
            WriteAt("ENTER THE DETAILS", startCol + 16, startRow + 3);
            int item = DisplayFieldNamesWithRepositioning(withdrawWindowFields, withdrawFieldsPositions, startCol, startRow);

            do
            {
                // Get User Inputs
                for (int field = 0; field < item; field++)
                {
                    Console.SetCursorPosition(withdrawFieldsPositions[field, 0], withdrawFieldsPositions[field, 1]);
                    withdrawUserInputs[field] = Console.ReadLine();

                    // Check that the account number is correct
                    if (field == 0)
                    {
                        DisplayCheckingForAccount(withdrawUserInputs[0], startCol, numberOfLines, "WithdrawScreen");
                    }
                    // Check that amount is of appropriate format (integer greather than 0)
                    // and that is less or equal to the balance
                    if (field == 1)
                    {
                        // Clear any potential previous error messages
                        WriteAt(new string(' ', Console.WindowWidth), startCol, numberOfLines + 2);

                        if (float.TryParse(withdrawUserInputs[1], out float amount) && amount > 0)
                        {
                            string accountFileName = withdrawUserInputs[0] + ".txt";
                            // File/Account existence was checked previously, so we can safely read the file
                            string[] userDetails = File.ReadAllLines(accountFileName);
                            float currentBalance = 0;
                            int balanceLineIndex = 0;
                            for (int loopVar = 0; loopVar < userDetails.Length; loopVar++)
                            {
                                // Get the current balance and the index of the line containing the balance in the file
                                if (userDetails[loopVar].Contains("Balance"))
                                {
                                    string[] splittedUserDetails = userDetails[loopVar].Split('|');
                                    currentBalance = float.Parse(splittedUserDetails[1]);
                                    balanceLineIndex = loopVar;
                                }
                            }
                            // Check that the amount is less or equal to the current balance 
                            if (amount <= currentBalance)
                            {
                                float newBalance = currentBalance - amount;
                                userDetails[balanceLineIndex] = "Balance|" + newBalance;
                                File.WriteAllLines(accountFileName, userDetails);

                                // Add the withdraw transaction at the end of the file
                                DateTime today = DateTime.Today;
                                string transaction = today.ToString("dd.MM.yyyy") + "|Withdraw|" + amount + "|"
                                    + newBalance + Environment.NewLine;
                                File.AppendAllText(accountFileName, transaction);

                                DisplayYesNoQuestion(startCol, numberOfLines + 2, "SuccessfulWithdraw");
                            }
                            // If the amount is greater than the balance, display an error message and allow the user to try again
                            else
                            {
                                WriteAt("Insufficient fund. Enter an amount less or equal to your balance", startCol, numberOfLines + 2);
                                WriteAt("Your balance is at: $" + currentBalance, startCol, numberOfLines + 3);
                                WriteAt("Please press enter to try again", startCol, numberOfLines + 4);
                                Console.ReadKey();
                                WithdrawScreen(ScreenParameters.depositAndWithdrawScreenParams);
                            }
                        }
                        // If the amount is in an invalid format, display an error message and allow the user to try again
                        else
                        {
                            WriteAt("Invalid amount. You must enter a number greather than 0", startCol, numberOfLines + 2);
                            WriteAt("Please press enter to try again", startCol, numberOfLines + 3);
                            Console.ReadKey();
                            WithdrawScreen(ScreenParameters.depositAndWithdrawScreenParams);
                        }
                    }
                }
            } while (true);
        }

        // Method to create the form with borders
        private void CreateForm(int numberOfLines, int formWidth, int startRow, int startCol, bool clearScreen)
        {
            if (clearScreen)
            {
                Console.Clear();
                origRow = Console.CursorTop;
                origCol = Console.CursorLeft;
            }

            for (int line = 0; line < numberOfLines; line++)
            {
                // Print the borders
                if (line == 0 | line == 2 | line == (numberOfLines - 1))
                {
                    for (int col = 0; col < formWidth; col++)
                    {
                        if (line == 0 && col == 0)
                        {
                            WriteAt("╔", startCol + col, startRow + line);
                            continue;
                        }
                        if (line == 0 && col + 1 == formWidth)
                        {
                            WriteAt("╗", startCol + col, startRow + line);
                            break;
                        }
                        if (line == 2 && col == 0)
                        {
                            WriteAt("|", startCol + col, startRow + line);
                            continue;
                        }
                        if (line == 2 && col + 1 == formWidth)
                        {
                            WriteAt("|", startCol + col, startRow + line);
                            break;
                        }
                        if (line == (numberOfLines - 1) && col == 0)
                        {
                            WriteAt("╚", startCol + col, startRow + line);
                            continue;
                        }
                        if (line == (numberOfLines - 1) && col + 1 == formWidth)
                        {
                            WriteAt("╝", startCol + col, startRow + line);
                            break;
                        }
                        WriteAt("═", startCol + col, startRow + line);
                    }
                }
                else
                {
                    WriteAt("|", startCol, startRow + line);
                    WriteAt("|", startCol + formWidth - 1, startRow + line);
                }
            }
        }

        // Method to display field names
        private int DisplayFieldNames(string[] windowFields, int startCol, int startRow)
        {
            int item = 0;
            foreach (string fieldName in windowFields)
            {
                // Maximum length of a sentence until it reaches the border of the screen
                int maxLength = 50 - startCol - 6;

                // Split the sentence in several lines if the sentence is too long
                if (fieldName.Length > maxLength)
                {
                    string[] words = fieldName.Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    StringBuilder buffer = new StringBuilder();
                    foreach (String word in words)
                    {
                        buffer.Append(word);
                        if (buffer.Length >= maxLength)
                        {
                            String line = buffer.ToString().Substring(0, buffer.Length - word.Length);
                            WriteAt(line, startCol + 6, startRow + 3 + item);
                            item++;
                            buffer.Clear();
                            buffer.Append(word);
                        }
                        buffer.Append(" ");
                    }
                    WriteAt(buffer.ToString(), startCol + 6, startRow + 3 + item);
                    item++;
                }
                // If the sentence is within the border, display it as it is
                else
                {
                    WriteAt(fieldName, startCol + 6, startRow + 3 + item);
                    item++;
                }
            }
            return item;
        }

        // Method to display field names with cursor repositioning
        // These are the field names which will ask user inputs
        private int DisplayFieldNamesWithRepositioning(string[] windowFields, int[,] fieldsPositions,
            int startCol, int startRow)
        {
            int item = 0;
            foreach (string fieldName in windowFields)
            {
                WriteAt(fieldName, startCol + 6, startRow + 5 + item);
                fieldsPositions[item, 1] = Console.CursorTop;
                fieldsPositions[item, 0] = Console.CursorLeft;
                item++;
            }
            return item;
        }

        // Method to print strings at specific positions on the console screen
        // Source: https://docs.microsoft.com/en-us/dotnet/api/system.console.setcursorposition?view=net-5.0
        protected static void WriteAt(string s, int col, int row)
        {
            try
            {
                Console.SetCursorPosition(origCol + col, origRow + row);
                Console.Write(s);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
            }
        }

        // Method to verify the information are correct when creating an account
        private void VerifyAccount(int startCol, int numberOfLines, string phoneNumber, string email)
        {
            WriteAt("Is the information correct (y/n)? ", startCol, numberOfLines + 2);
            // Cursor position is placed just after the prompt
            int inputCursorLeft = Console.CursorLeft;
            int inputCursorTop = Console.CursorTop;

            string userInput = Console.ReadLine();
            if (userInput.ToLower() == "y")
            {
                // Clear the (possible) error sentence if input from confirmation sentence was invalid
                WriteAt(new string(' ', Console.WindowWidth), startCol, numberOfLines + 4);
                int numberOfErrorLines = 0;

                // Verify that the phone number input is of appropriate format
                // I.e.: integer of no more than 10 characters
                if (!(int.TryParse(phoneNumber, out _) && phoneNumber.Length <= 10))
                {
                    WriteAt("- Invalid input for phone number. It must be a number of no more than 10 characters", startCol, numberOfLines + 2);
                    numberOfErrorLines++;
                }
                // Verify that the email input is of appropriate format
                // and that it belongs to one of those domains: gmail, outlook, uts.edu.au or student.uts.edu.au
                if (!(GeneralFunctions.IsValidEmail(email) && (email.Contains("gmail.com") || email.Contains("outlook.com")
                    || email.Contains("uts.edu.au") || email.Contains("student.uts.edu.au"))))
                {
                    WriteAt("- Invalid input for email. It must be a valid email ending with '@' followed by one of these domains: ",
                        startCol, numberOfLines + 2 + numberOfErrorLines);
                    WriteAt("  gmail.com, outlook.com, uts.edu.au or student.uts.edu.au",
                        startCol, numberOfLines + 3 + numberOfErrorLines);
                    numberOfErrorLines += 2;
                }
                // If there are errors, allow the user to try again
                if (numberOfErrorLines > 0)
                {
                    WriteAt("Please press enter to try again", startCol, numberOfLines + 3 + numberOfErrorLines);
                    Console.ReadKey();
                    CreateAccountScreen(ScreenParameters.createAcctScreenParams);
                }

                // Clear the sentence that confirms the information when there is no error
                WriteAt(new string(' ', Console.WindowWidth), startCol, numberOfLines + 2);
            }
            else if (userInput.ToLower() == "n")
            {
                CreateAccountScreen(ScreenParameters.createAcctScreenParams);
            }
            // If the choice provided is no 'y' or 'n', display an error message and allow the user to try again
            else
            {
                WriteAt("Invalid choice. You must enter 'y' or 'n'", startCol, numberOfLines + 4);

                // Reset choice to an empty string
                Console.SetCursorPosition(inputCursorLeft, inputCursorTop);
                Console.Write(new string(' ', userInput.Length));
                Console.SetCursorPosition(inputCursorLeft, inputCursorTop);

                VerifyAccount(startCol, numberOfLines, phoneNumber, email);
            }
        }

        // Method to verify the account number input is of appropriate format
        // I.e.: integer of no more than 10 characters
        private bool VerifyAccountNumber(string inputAccountNumber, int startCol, int numberOfLines)
        {
            if (int.TryParse(inputAccountNumber, out _) && inputAccountNumber.Length <= 10)
            {
                return true;
            }
            else
            {
                WriteAt("Invalid account number. You must enter a number of no more than 10 characters", startCol, numberOfLines + 2);
                WriteAt("Please press enter to try again", startCol, numberOfLines + 3);
                return false;
            }
        }

        // Method to display outputs when checking for an account 
        // It checks whether the account number if of correct format and whether it exists
        private void DisplayCheckingForAccount(string accountNumber, int startCol, int numberOfLines, string currentScreen)
        {
            string action = "", typeOfAction = "";
            if (currentScreen == "DepositScreen")
            {
                action = "deposit";
                typeOfAction = "SearchAccountDeposit";
            }
            if (currentScreen == "WithdrawScreen")
            {
                action = "withdraw";
                typeOfAction = "SearchAccountWithdraw";
            }

            if (VerifyAccountNumber(accountNumber, startCol, numberOfLines))
            {
                if (File.Exists(accountNumber + ".txt"))
                {
                    WriteAt("Please enter an amount to " + action, startCol, numberOfLines + 2);
                }
                // If the file/account does not exist, display an error message and yes/no question asking the user
                // whether they want to try again
                else
                {
                    DisplayYesNoQuestion(startCol, numberOfLines + 2, typeOfAction);
                }
            }
            // If the account number is not in an appropriate format, VerifyAccountNumber() displays an error message
            // Then we redirect the user to the appropriate screen to try again
            else
            {
                Console.ReadKey();
                if (currentScreen == "DepositScreen")
                {
                    DepositScreen(ScreenParameters.depositAndWithdrawScreenParams);
                }
                if (currentScreen == "WithdrawScreen")
                {
                    WithdrawScreen(ScreenParameters.depositAndWithdrawScreenParams);
                }
            }
        }

        // Method to display a y/n (Yes/No) question
        private void DisplayYesNoQuestion(int startCol, int numberOfLines, 
            string currentScreen, Account userAccount = null, string emailBody = "")
        {
            // If the account number does not exist, allow the user to search for another account
            if (currentScreen == "SearchAccount" || currentScreen == "SearchAccountDeposit" || currentScreen == "SearchAccountWithdraw" 
                || currentScreen == "AccountStatement" || currentScreen == "DeleteAccount")
            {
                WriteAt("Account not found!", startCol, numberOfLines);
                WriteAt("Would you like to search another account (y/n)? ", startCol, numberOfLines + 1);
            }
            if (currentScreen == "SuccessfulSearch")
            {
                WriteAt("Would you like to search another account (y/n)? ", startCol, numberOfLines);
            }
            if (currentScreen == "SuccessfulDeposit")
            {
                WriteAt("Deposit Successful!", startCol, numberOfLines);
                WriteAt("Would you like to make another deposit (y/n)? ", startCol, numberOfLines + 1);
            }
            if (currentScreen == "SuccessfulWithdraw")
            {
                WriteAt("Withdraw Successful!", startCol, numberOfLines);
                WriteAt("Would you like to make another withdrawal (y/n)? ", startCol, numberOfLines + 1);
            }
            // If the account statement has been successfully displayed, allow the user to email their statement if they wish to
            if (currentScreen == "SuccessfulStatement")
            {
                WriteAt("Would you like to email your statement (y/n)? ", startCol, numberOfLines);
            }
            if (currentScreen == "AnotherStatement")
            {
                WriteAt("Would you like to get the statement for another account (y/n)? ", startCol, numberOfLines);
            }
            if (currentScreen == "SuccessfulSearchForDelete")
            {
                WriteAt("Would you like to delete this account (y/n)? ", startCol, numberOfLines);
            }

            // Cursor position placed just after the prompt
            int inputCursorLeft = Console.CursorLeft;
            int inputCursorTop = Console.CursorTop;

            string userInput = Console.ReadLine();
            if (userInput.ToLower() == "y")
            {
                if (currentScreen == "SearchAccount" || currentScreen == "SuccessfulSearch"
                    || currentScreen == "AccountStatement" || currentScreen == "DeleteAccount")
                {
                    if (currentScreen == "SuccessfulSearch")
                    {
                        currentScreen = "SearchAccount";
                    }
                    SearchAccountScreen(ScreenParameters.searchAccountScreenParams, currentScreen);
                }
                if (currentScreen == "SearchAccountDeposit" || currentScreen == "SuccessfulDeposit")
                {
                    DepositScreen(ScreenParameters.depositAndWithdrawScreenParams);
                }
                if (currentScreen == "SearchAccountWithdraw" || currentScreen == "SuccessfulWithdraw")
                {
                    WithdrawScreen(ScreenParameters.depositAndWithdrawScreenParams);
                }
                if (currentScreen == "SuccessfulStatement")
                {
                    bool isSuccessFullySent = GeneralFunctions.SendEmail(userAccount.Email, userAccount.GetFullName(), 
                        userAccount.AccountNumber, emailBody, "Account Statement");
                    if (isSuccessFullySent)
                    {
                        WriteAt("Email sent successfully!", startCol, numberOfLines + 1);
                        // Ask whether the user would like to get another account statement
                        DisplayYesNoQuestion(startCol, numberOfLines + 2, "AnotherStatement");
                    }
                }
                // If the user would like another account statement, redirect them to the SearchAccountScreen for AccountStatement
                if (currentScreen == "AnotherStatement")
                {
                    SearchAccountScreen(ScreenParameters.searchAccountScreenParams, "AccountStatement");
                }
                // If the file to be deleted has been found, delete the file and display an appropriate message
                if (currentScreen == "SuccessfulSearchForDelete")
                {
                    // Clear any potential previous error message(s)
                    WriteAt(new string(' ', Console.WindowWidth), startCol, numberOfLines + 2);

                    // Delete the account file
                    File.Delete(userAccount.AccountNumber + ".txt");

                    WriteAt("Account deleted!", startCol, numberOfLines + 2);
                    WriteAt("Please press enter to return back to the menu", startCol, numberOfLines + 3);
                    Console.ReadKey();
                    MainMenuScreen(ScreenParameters.mainMenuScreenParams);
                }
            }
            else if (userInput.ToLower() == "n")
            {
                MainMenuScreen(ScreenParameters.mainMenuScreenParams);
            }
            else
            {
                WriteAt("Invalid choice. You must enter 'y' or 'n'", startCol, numberOfLines + 2);

                // Reset choice to an empty string
                Console.SetCursorPosition(inputCursorLeft, inputCursorTop);
                Console.Write(new string(' ', userInput.Length));
                Console.SetCursorPosition(inputCursorLeft, inputCursorTop);

                DisplayYesNoQuestion(startCol, numberOfLines, currentScreen, userAccount, emailBody);
            }
        }
    }

    // GeneralFunctions consist of general functions used in the program
    // These functions are outsourced from the internet 
    class GeneralFunctions
    {
        // Method to hide the password with asteriks
        // Source: https://www.youtube.com/watch?v=sFgQCEzqQ20
        public static string MaskPasswordString()
        {
            string password;
            StringBuilder builder = new StringBuilder();
            ConsoleKeyInfo keyInfo;
            do
            {
                keyInfo = Console.ReadKey(true);
                if (!char.IsControl(keyInfo.KeyChar))
                {
                    builder.Append(keyInfo.KeyChar);
                    Console.Write("*");
                }
                // Allow for deletion of the password when backspacing
                else if (keyInfo.Key == ConsoleKey.Backspace && builder.Length > 0)
                {
                    builder.Remove(builder.Length - 1, 1);
                    Console.Write("\b \b");
                }
            }
            while (keyInfo.Key != ConsoleKey.Enter);
            {
                // Return the password as a string to be able to store it in the account file
                password = builder.ToString();
                return password;
            }
        }

        // Method to verify that the input is a valid email
        // Source: https://docs.microsoft.com/en-us/dotnet/standard/base-types/how-to-verify-that-strings-are-in-valid-email-format
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // Normalize the domain
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                        RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    string domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException e)
            {
                return false;
            }
            catch (ArgumentException e)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        // Method to send email
        // Source: https://docs.microsoft.com/en-us/dotnet/api/system.net.mail.smtpclient?view=net-5.0
        // Source for exception handling: https://docs.microsoft.com/en-us/dotnet/api/system.net.mail.smtpexception?view=net-5.0
        public static bool SendEmail(string userEmail, string userFullName, int userAccountNumber, string messageBody, string userAction)
        {
            bool hasBeenSuccessfullySent = false;
            MailMessage mail = new MailMessage();

            // Set destinations for the email message.
            mail.To.Add(userEmail);

            // Email sender
            mail.From = new MailAddress("simplebankingsystem.helena@gmail.com", "Simple Banking System @Helena");

            // Specify the message body and subject
            mail.Subject = "Simple Banking System - " + userAction + " of " + userFullName + " #" + userAccountNumber;
            mail.Body = messageBody;
            mail.IsBodyHtml = false;
            mail.BodyEncoding = Encoding.UTF8;
            mail.BodyTransferEncoding = System.Net.Mime.TransferEncoding.EightBit;

            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential("simplebankingsystem.helena@gmail.com", "simplebankingsystem-Helena");
            try
            {
                // Send the email
                client.Send(mail);
                hasBeenSuccessfullySent = true;
            }
            // Catch SmtpFailedRecipientsException if the recipient cannot be found/reached
            catch (SmtpFailedRecipientsException e)
            {
                for (int i = 0; i < e.InnerExceptions.Length; i++)
                {
                    SmtpStatusCode status = e.InnerExceptions[i].StatusCode;
                    if (status == SmtpStatusCode.MailboxBusy ||
                        status == SmtpStatusCode.MailboxUnavailable)
                    {
                        Console.WriteLine("Delivery failed - retrying in 5 seconds.");
                        System.Threading.Thread.Sleep(5000);
                        client.Send(mail);
                        hasBeenSuccessfullySent = true;
                    }
                    else
                    {
                        Console.WriteLine("Failed to deliver message to {0}", e.InnerExceptions[i].FailedRecipient);
                        hasBeenSuccessfullySent = false;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception caught in RetryIfBusy(): {0}", e.ToString());
                hasBeenSuccessfullySent = false;
            }

            return hasBeenSuccessfullySent;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            UserInterface ConsoleInterface = new UserInterface();

            // Create the Login Interface
            ConsoleInterface.LoginScreen(ScreenParameters.loginScreenParams);
        }
    }
}
