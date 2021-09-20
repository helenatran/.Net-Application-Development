namespace Week8Program2
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.title = new System.Windows.Forms.Label();
            this.menuGroupBox = new System.Windows.Forms.GroupBox();
            this.priceListBox = new System.Windows.Forms.ListBox();
            this.priceLabel = new System.Windows.Forms.Label();
            this.itemsLabel = new System.Windows.Forms.Label();
            this.menuItemsListBox = new System.Windows.Forms.ListBox();
            this.ReceipGroupBox = new System.Windows.Forms.GroupBox();
            this.receiptLabel = new System.Windows.Forms.Label();
            this.orderButton = new System.Windows.Forms.Button();
            this.exitButton = new System.Windows.Forms.Button();
            this.menuGroupBox.SuspendLayout();
            this.ReceipGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // title
            // 
            this.title.AutoSize = true;
            this.title.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.title.Location = new System.Drawing.Point(229, 39);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(345, 40);
            this.title.TabIndex = 0;
            this.title.Text = "Student Restaurant";
            // 
            // menuGroupBox
            // 
            this.menuGroupBox.Controls.Add(this.priceListBox);
            this.menuGroupBox.Controls.Add(this.priceLabel);
            this.menuGroupBox.Controls.Add(this.itemsLabel);
            this.menuGroupBox.Controls.Add(this.menuItemsListBox);
            this.menuGroupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuGroupBox.Location = new System.Drawing.Point(48, 129);
            this.menuGroupBox.Name = "menuGroupBox";
            this.menuGroupBox.Size = new System.Drawing.Size(394, 350);
            this.menuGroupBox.TabIndex = 1;
            this.menuGroupBox.TabStop = false;
            this.menuGroupBox.Text = "Menu";
            // 
            // priceListBox
            // 
            this.priceListBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.priceListBox.FormattingEnabled = true;
            this.priceListBox.ItemHeight = 26;
            this.priceListBox.Location = new System.Drawing.Point(272, 97);
            this.priceListBox.Name = "priceListBox";
            this.priceListBox.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.priceListBox.Size = new System.Drawing.Size(90, 212);
            this.priceListBox.TabIndex = 5;
            // 
            // priceLabel
            // 
            this.priceLabel.AutoSize = true;
            this.priceLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.priceLabel.Location = new System.Drawing.Point(267, 51);
            this.priceLabel.Name = "priceLabel";
            this.priceLabel.Size = new System.Drawing.Size(95, 29);
            this.priceLabel.TabIndex = 4;
            this.priceLabel.Text = "Price $";
            // 
            // itemsLabel
            // 
            this.itemsLabel.AutoSize = true;
            this.itemsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.itemsLabel.Location = new System.Drawing.Point(27, 51);
            this.itemsLabel.Name = "itemsLabel";
            this.itemsLabel.Size = new System.Drawing.Size(76, 29);
            this.itemsLabel.TabIndex = 3;
            this.itemsLabel.Text = "Items";
            // 
            // menuItemsListBox
            // 
            this.menuItemsListBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuItemsListBox.FormattingEnabled = true;
            this.menuItemsListBox.ItemHeight = 26;
            this.menuItemsListBox.Location = new System.Drawing.Point(32, 97);
            this.menuItemsListBox.Name = "menuItemsListBox";
            this.menuItemsListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.menuItemsListBox.Size = new System.Drawing.Size(171, 212);
            this.menuItemsListBox.TabIndex = 0;
            // 
            // ReceipGroupBox
            // 
            this.ReceipGroupBox.Controls.Add(this.receiptLabel);
            this.ReceipGroupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ReceipGroupBox.Location = new System.Drawing.Point(485, 129);
            this.ReceipGroupBox.Name = "ReceipGroupBox";
            this.ReceipGroupBox.Size = new System.Drawing.Size(335, 432);
            this.ReceipGroupBox.TabIndex = 2;
            this.ReceipGroupBox.TabStop = false;
            this.ReceipGroupBox.Text = "Receipt";
            // 
            // receiptLabel
            // 
            this.receiptLabel.AutoSize = true;
            this.receiptLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.receiptLabel.Location = new System.Drawing.Point(31, 66);
            this.receiptLabel.Name = "receiptLabel";
            this.receiptLabel.Size = new System.Drawing.Size(249, 78);
            this.receiptLabel.TabIndex = 5;
            this.receiptLabel.Text = "Press order to \r\nfinalise your order and\r\nget the receipt.\r\n";
            // 
            // orderButton
            // 
            this.orderButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.orderButton.Location = new System.Drawing.Point(48, 505);
            this.orderButton.Name = "orderButton";
            this.orderButton.Size = new System.Drawing.Size(181, 56);
            this.orderButton.TabIndex = 3;
            this.orderButton.Text = "Order";
            this.orderButton.UseVisualStyleBackColor = true;
            this.orderButton.Click += new System.EventHandler(this.orderButton_Click);
            // 
            // exitButton
            // 
            this.exitButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exitButton.Location = new System.Drawing.Point(261, 505);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(181, 56);
            this.exitButton.TabIndex = 4;
            this.exitButton.Text = "Exit";
            this.exitButton.UseVisualStyleBackColor = true;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(852, 617);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.orderButton);
            this.Controls.Add(this.ReceipGroupBox);
            this.Controls.Add(this.menuGroupBox);
            this.Controls.Add(this.title);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Form1";
            this.Text = "Student Restaurant Management System";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuGroupBox.ResumeLayout(false);
            this.menuGroupBox.PerformLayout();
            this.ReceipGroupBox.ResumeLayout(false);
            this.ReceipGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label title;
        private System.Windows.Forms.GroupBox menuGroupBox;
        private System.Windows.Forms.ListBox menuItemsListBox;
        private System.Windows.Forms.GroupBox ReceipGroupBox;
        private System.Windows.Forms.Label itemsLabel;
        private System.Windows.Forms.ListBox priceListBox;
        private System.Windows.Forms.Label priceLabel;
        private System.Windows.Forms.Button orderButton;
        private System.Windows.Forms.Label receiptLabel;
        private System.Windows.Forms.Button exitButton;
    }
}

