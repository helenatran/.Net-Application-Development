using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Week8Program2
{
    public partial class Form1 : Form
    {
        List<string> menuItems = new List<string> {
            "Tea/Coffee",
            "Juice",
            "Banana Bread",
            "Cereal",
            "Sushi",
            "Pizza",
            "Drinks"
        };

        List<string> prices = new List<string>
        {
            "4.80",
            "3.50",
            "2.50",
            "5.50",
            "3.00",
            "9.00",
            "3.50"
        };

        public Form1()
        {
            InitializeComponent();

            // Populate the list box with menu items
            menuItemsListBox.DataSource = menuItems;

            // Populate the prices list box with items prices
            priceListBox.DataSource = prices;
        }

        // Method to create the payment receipt
        public string GetReceipt()
        {
            double totalPrice = 0.0;
            string receipt = "Student Restaurant \n\nYour Order Details: \n\n";

            foreach (int index in menuItemsListBox.SelectedIndices)
            {
                receipt = receipt + menuItemsListBox.Items[index].ToString() + ": $" + prices[index]+ "\n";
                totalPrice += Convert.ToDouble(prices[index]);

            }
            // Add the total price to the Receipt
            receipt = receipt + "\nTotal Price: $" + totalPrice.ToString();
            return receipt;
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void orderButton_Click(object sender, EventArgs e)
        {
            receiptLabel.Text = GetReceipt();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
