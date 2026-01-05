namespace EFCore_SQLServer
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel2 = new TableLayoutPanel();
            btnAddTopping = new Button();
            btnAddSauce = new Button();
            btnAddPizza = new Button();
            btnConnect = new Button();
            tbxName = new TextBox();
            tbxCalory = new TextBox();
            cbxIsVegan = new CheckBox();
            tableLayoutPanel3 = new TableLayoutPanel();
            lbStatus = new Label();
            dataGridView1 = new DataGridView();
            dataGridView2 = new DataGridView();
            dataGridView3 = new DataGridView();
            dataGridView4 = new DataGridView();
            pizzaBindingSource = new BindingSource(components);
            sauceBindingSource = new BindingSource(components);
            idDataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            nameDataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            isVeganDataGridViewCheckBoxColumn = new DataGridViewCheckBoxColumn();
            toppingBindingSource = new BindingSource(components);
            pizzaToppingBindingSource = new BindingSource(components);
            pizzaIdDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            toppingIdDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            idDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            nameDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            sauceDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            toppingsDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            idDataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
            nameDataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
            caloriesDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            pizzasDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pizzaBindingSource).BeginInit();
            ((System.ComponentModel.ISupportInitialize)sauceBindingSource).BeginInit();
            ((System.ComponentModel.ISupportInitialize)toppingBindingSource).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pizzaToppingBindingSource).BeginInit();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 4;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 0);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel3, 0, 2);
            tableLayoutPanel1.Controls.Add(dataGridView1, 2, 0);
            tableLayoutPanel1.Controls.Add(dataGridView2, 3, 0);
            tableLayoutPanel1.Controls.Add(dataGridView3, 2, 1);
            tableLayoutPanel1.Controls.Add(dataGridView4, 3, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel1.Size = new Size(800, 450);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel1.SetColumnSpan(tableLayoutPanel2, 2);
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            tableLayoutPanel2.Controls.Add(btnAddTopping, 1, 3);
            tableLayoutPanel2.Controls.Add(btnAddSauce, 1, 2);
            tableLayoutPanel2.Controls.Add(btnAddPizza, 1, 1);
            tableLayoutPanel2.Controls.Add(btnConnect, 0, 0);
            tableLayoutPanel2.Controls.Add(tbxName, 0, 1);
            tableLayoutPanel2.Controls.Add(tbxCalory, 0, 2);
            tableLayoutPanel2.Controls.Add(cbxIsVegan, 0, 3);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(3, 3);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 5;
            tableLayoutPanel1.SetRowSpan(tableLayoutPanel2, 2);
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Size = new Size(194, 414);
            tableLayoutPanel2.TabIndex = 6;
            // 
            // btnAddTopping
            // 
            btnAddTopping.Dock = DockStyle.Fill;
            btnAddTopping.Location = new Point(119, 93);
            btnAddTopping.Name = "btnAddTopping";
            btnAddTopping.Size = new Size(72, 24);
            btnAddTopping.TabIndex = 9;
            btnAddTopping.Text = "토핑추가";
            btnAddTopping.UseVisualStyleBackColor = true;
            btnAddTopping.Click += btnAddTopping_Click;
            // 
            // btnAddSauce
            // 
            btnAddSauce.Dock = DockStyle.Fill;
            btnAddSauce.Location = new Point(119, 63);
            btnAddSauce.Name = "btnAddSauce";
            btnAddSauce.Size = new Size(72, 24);
            btnAddSauce.TabIndex = 8;
            btnAddSauce.Text = "소스추가";
            btnAddSauce.UseVisualStyleBackColor = true;
            btnAddSauce.Click += btnAddSauce_Click;
            // 
            // btnAddPizza
            // 
            btnAddPizza.Dock = DockStyle.Fill;
            btnAddPizza.Location = new Point(119, 33);
            btnAddPizza.Name = "btnAddPizza";
            btnAddPizza.Size = new Size(72, 24);
            btnAddPizza.TabIndex = 4;
            btnAddPizza.Text = "피자추가";
            btnAddPizza.UseVisualStyleBackColor = true;
            btnAddPizza.Click += btnAddPizza_Click;
            // 
            // btnConnect
            // 
            tableLayoutPanel2.SetColumnSpan(btnConnect, 2);
            btnConnect.Location = new Point(3, 3);
            btnConnect.Name = "btnConnect";
            btnConnect.Size = new Size(188, 24);
            btnConnect.TabIndex = 0;
            btnConnect.Text = "DB 연결";
            btnConnect.UseVisualStyleBackColor = true;
            btnConnect.Click += btnConnect_Click;
            // 
            // tbxName
            // 
            tbxName.Dock = DockStyle.Fill;
            tbxName.Location = new Point(3, 33);
            tbxName.Multiline = true;
            tbxName.Name = "tbxName";
            tbxName.Size = new Size(110, 24);
            tbxName.TabIndex = 5;
            // 
            // tbxCalory
            // 
            tbxCalory.Dock = DockStyle.Fill;
            tbxCalory.Location = new Point(3, 63);
            tbxCalory.Name = "tbxCalory";
            tbxCalory.Size = new Size(110, 23);
            tbxCalory.TabIndex = 6;
            // 
            // cbxIsVegan
            // 
            cbxIsVegan.AutoSize = true;
            cbxIsVegan.Dock = DockStyle.Fill;
            cbxIsVegan.Location = new Point(3, 93);
            cbxIsVegan.Name = "cbxIsVegan";
            cbxIsVegan.Size = new Size(110, 24);
            cbxIsVegan.TabIndex = 7;
            cbxIsVegan.Text = "Is Vegan?";
            cbxIsVegan.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.BackColor = SystemColors.ActiveCaption;
            tableLayoutPanel3.ColumnCount = 2;
            tableLayoutPanel1.SetColumnSpan(tableLayoutPanel3, 4);
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.Controls.Add(lbStatus, 0, 0);
            tableLayoutPanel3.Dock = DockStyle.Fill;
            tableLayoutPanel3.Location = new Point(3, 423);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 1;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.Size = new Size(794, 24);
            tableLayoutPanel3.TabIndex = 7;
            // 
            // lbStatus
            // 
            lbStatus.AutoSize = true;
            lbStatus.Dock = DockStyle.Left;
            lbStatus.Location = new Point(3, 0);
            lbStatus.Name = "lbStatus";
            lbStatus.Size = new Size(78, 24);
            lbStatus.TabIndex = 1;
            lbStatus.Text = "상태 : 대기중";
            lbStatus.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // dataGridView1
            // 
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { idDataGridViewTextBoxColumn, nameDataGridViewTextBoxColumn, sauceDataGridViewTextBoxColumn, toppingsDataGridViewTextBoxColumn });
            dataGridView1.DataSource = pizzaBindingSource;
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.Location = new Point(203, 3);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(294, 204);
            dataGridView1.TabIndex = 8;
            // 
            // dataGridView2
            // 
            dataGridView2.AutoGenerateColumns = false;
            dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView2.Columns.AddRange(new DataGridViewColumn[] { idDataGridViewTextBoxColumn1, nameDataGridViewTextBoxColumn1, isVeganDataGridViewCheckBoxColumn });
            dataGridView2.DataSource = sauceBindingSource;
            dataGridView2.Dock = DockStyle.Fill;
            dataGridView2.Location = new Point(503, 3);
            dataGridView2.Name = "dataGridView2";
            dataGridView2.Size = new Size(294, 204);
            dataGridView2.TabIndex = 9;
            // 
            // dataGridView3
            // 
            dataGridView3.AutoGenerateColumns = false;
            dataGridView3.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView3.Columns.AddRange(new DataGridViewColumn[] { idDataGridViewTextBoxColumn2, nameDataGridViewTextBoxColumn2, caloriesDataGridViewTextBoxColumn, pizzasDataGridViewTextBoxColumn });
            dataGridView3.DataSource = toppingBindingSource;
            dataGridView3.Dock = DockStyle.Fill;
            dataGridView3.Location = new Point(203, 213);
            dataGridView3.Name = "dataGridView3";
            dataGridView3.Size = new Size(294, 204);
            dataGridView3.TabIndex = 10;
            // 
            // dataGridView4
            // 
            dataGridView4.AutoGenerateColumns = false;
            dataGridView4.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView4.Columns.AddRange(new DataGridViewColumn[] { pizzaIdDataGridViewTextBoxColumn, toppingIdDataGridViewTextBoxColumn });
            dataGridView4.DataSource = pizzaToppingBindingSource;
            dataGridView4.Dock = DockStyle.Fill;
            dataGridView4.Location = new Point(503, 213);
            dataGridView4.Name = "dataGridView4";
            dataGridView4.Size = new Size(294, 204);
            dataGridView4.TabIndex = 11;
            // 
            // pizzaBindingSource
            // 
            pizzaBindingSource.DataSource = typeof(Models.Pizza);
            // 
            // sauceBindingSource
            // 
            sauceBindingSource.DataSource = typeof(Models.Sauce);
            // 
            // idDataGridViewTextBoxColumn1
            // 
            idDataGridViewTextBoxColumn1.DataPropertyName = "Id";
            idDataGridViewTextBoxColumn1.HeaderText = "Id";
            idDataGridViewTextBoxColumn1.Name = "idDataGridViewTextBoxColumn1";
            // 
            // nameDataGridViewTextBoxColumn1
            // 
            nameDataGridViewTextBoxColumn1.DataPropertyName = "Name";
            nameDataGridViewTextBoxColumn1.HeaderText = "Name";
            nameDataGridViewTextBoxColumn1.Name = "nameDataGridViewTextBoxColumn1";
            // 
            // isVeganDataGridViewCheckBoxColumn
            // 
            isVeganDataGridViewCheckBoxColumn.DataPropertyName = "IsVegan";
            isVeganDataGridViewCheckBoxColumn.HeaderText = "IsVegan";
            isVeganDataGridViewCheckBoxColumn.Name = "isVeganDataGridViewCheckBoxColumn";
            // 
            // toppingBindingSource
            // 
            toppingBindingSource.DataSource = typeof(Models.Topping);
            // 
            // pizzaToppingBindingSource
            // 
            pizzaToppingBindingSource.DataSource = typeof(Models.PizzaTopping);
            // 
            // pizzaIdDataGridViewTextBoxColumn
            // 
            pizzaIdDataGridViewTextBoxColumn.DataPropertyName = "PizzaId";
            pizzaIdDataGridViewTextBoxColumn.HeaderText = "PizzaId";
            pizzaIdDataGridViewTextBoxColumn.Name = "pizzaIdDataGridViewTextBoxColumn";
            // 
            // toppingIdDataGridViewTextBoxColumn
            // 
            toppingIdDataGridViewTextBoxColumn.DataPropertyName = "ToppingId";
            toppingIdDataGridViewTextBoxColumn.HeaderText = "ToppingId";
            toppingIdDataGridViewTextBoxColumn.Name = "toppingIdDataGridViewTextBoxColumn";
            // 
            // idDataGridViewTextBoxColumn
            // 
            idDataGridViewTextBoxColumn.DataPropertyName = "Id";
            idDataGridViewTextBoxColumn.HeaderText = "Id";
            idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            // 
            // nameDataGridViewTextBoxColumn
            // 
            nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
            nameDataGridViewTextBoxColumn.HeaderText = "Name";
            nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            // 
            // sauceDataGridViewTextBoxColumn
            // 
            sauceDataGridViewTextBoxColumn.DataPropertyName = "Sauce";
            sauceDataGridViewTextBoxColumn.HeaderText = "Sauce";
            sauceDataGridViewTextBoxColumn.Name = "sauceDataGridViewTextBoxColumn";
            sauceDataGridViewTextBoxColumn.Visible = false;
            // 
            // toppingsDataGridViewTextBoxColumn
            // 
            toppingsDataGridViewTextBoxColumn.DataPropertyName = "Toppings";
            toppingsDataGridViewTextBoxColumn.HeaderText = "Toppings";
            toppingsDataGridViewTextBoxColumn.Name = "toppingsDataGridViewTextBoxColumn";
            toppingsDataGridViewTextBoxColumn.Visible = false;
            // 
            // idDataGridViewTextBoxColumn2
            // 
            idDataGridViewTextBoxColumn2.DataPropertyName = "Id";
            idDataGridViewTextBoxColumn2.HeaderText = "Id";
            idDataGridViewTextBoxColumn2.Name = "idDataGridViewTextBoxColumn2";
            // 
            // nameDataGridViewTextBoxColumn2
            // 
            nameDataGridViewTextBoxColumn2.DataPropertyName = "Name";
            nameDataGridViewTextBoxColumn2.HeaderText = "Name";
            nameDataGridViewTextBoxColumn2.Name = "nameDataGridViewTextBoxColumn2";
            // 
            // caloriesDataGridViewTextBoxColumn
            // 
            caloriesDataGridViewTextBoxColumn.DataPropertyName = "Calories";
            caloriesDataGridViewTextBoxColumn.HeaderText = "Calories";
            caloriesDataGridViewTextBoxColumn.Name = "caloriesDataGridViewTextBoxColumn";
            // 
            // pizzasDataGridViewTextBoxColumn
            // 
            pizzasDataGridViewTextBoxColumn.DataPropertyName = "Pizzas";
            pizzasDataGridViewTextBoxColumn.HeaderText = "Pizzas";
            pizzasDataGridViewTextBoxColumn.Name = "pizzasDataGridViewTextBoxColumn";
            pizzasDataGridViewTextBoxColumn.Visible = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(tableLayoutPanel1);
            Name = "Form1";
            Text = "Form1";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView3).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView4).EndInit();
            ((System.ComponentModel.ISupportInitialize)pizzaBindingSource).EndInit();
            ((System.ComponentModel.ISupportInitialize)sauceBindingSource).EndInit();
            ((System.ComponentModel.ISupportInitialize)toppingBindingSource).EndInit();
            ((System.ComponentModel.ISupportInitialize)pizzaToppingBindingSource).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private TableLayoutPanel tableLayoutPanel1;
        private Button btnConnect;
        private Label lbStatus;
        private Button btnAddPizza;
        private TextBox tbxName;
        private TableLayoutPanel tableLayoutPanel2;
        private TableLayoutPanel tableLayoutPanel3;
        private DataGridView dataGridView1;
        private DataGridView dataGridView2;
        private DataGridView dataGridView3;
        private DataGridView dataGridView4;
        private TextBox tbxCalory;
        private CheckBox cbxIsVegan;
        private Button btnAddTopping;
        private Button btnAddSauce;
        private DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn sauceDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn toppingsDataGridViewTextBoxColumn;
        private BindingSource pizzaBindingSource;
        private DataGridViewTextBoxColumn idDataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn1;
        private DataGridViewCheckBoxColumn isVeganDataGridViewCheckBoxColumn;
        private BindingSource sauceBindingSource;
        private BindingSource toppingBindingSource;
        private DataGridViewTextBoxColumn pizzaIdDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn toppingIdDataGridViewTextBoxColumn;
        private BindingSource pizzaToppingBindingSource;
        private DataGridViewTextBoxColumn idDataGridViewTextBoxColumn2;
        private DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn2;
        private DataGridViewTextBoxColumn caloriesDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn pizzasDataGridViewTextBoxColumn;
    }
}