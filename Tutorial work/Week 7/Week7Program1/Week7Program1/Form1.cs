using System;
using System.Linq;
using System.Windows.Forms;

namespace Week8Program1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }

        // Method to create the payment receipt 
        public string GetReceipt()
        {
            double totalPrice = 0.0;
            string receipt = "Student Restaurant \n\nYour Order Details: \n\n";

            // Create a label array for food item prices and assigned each price item to an element
            Label[] price = new Label[7];
            price[0] = teaCoffeePrice;
            price[1] = juicePrice;
            price[2] = bananaBreadPrice;
            price[3] = cerealPrice;
            price[4] = sushiPrice;
            price[5] = pizzaPrice;
            price[6] = drinksPrice;

            int count = 0;
            // Process each selected food item and calculate the total price
            foreach (CheckBox c in menuGrpBox.Controls.OfType<CheckBox>())
            {
                if (c.Checked == true)
                {
                    receipt = receipt + c.Text + ": $" + price[count].Text + "\n";
                    totalPrice += Convert.ToDouble(price[count].Text);
                }
                count++;
            }

            receipt = receipt + "Total Price: $" + totalPrice;
            return receipt;
        }

        private void orderButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show(GetReceipt(), "Receipt");
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
