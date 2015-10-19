namespace SimpleForms
{
	partial class CustomerForm
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
			this.components = new System.ComponentModel.Container();
			this.buttonRefreshCustomers = new System.Windows.Forms.Button();
			this.buttonUpdateCustomer = new System.Windows.Forms.Button();
			this.comboBoxCurrentCustomer = new System.Windows.Forms.ComboBox();
			this.textBoxCustomerID = new System.Windows.Forms.TextBox();
			this.textBoxCustomerName = new System.Windows.Forms.TextBox();
			this.textBoxCustomerPhone = new System.Windows.Forms.TextBox();
			this.labelCustomerID = new System.Windows.Forms.Label();
			this.labelCustomerName = new System.Windows.Forms.Label();
			this.labelCustomerPhone = new System.Windows.Forms.Label();
			this.customerBindingSource = new System.Windows.Forms.BindingSource(this.components);
			((System.ComponentModel.ISupportInitialize)(this.customerBindingSource)).BeginInit();
			this.SuspendLayout();
			// 
			// buttonRefreshCustomers
			// 
			this.buttonRefreshCustomers.Location = new System.Drawing.Point(12, 12);
			this.buttonRefreshCustomers.Name = "buttonRefreshCustomers";
			this.buttonRefreshCustomers.Size = new System.Drawing.Size(260, 23);
			this.buttonRefreshCustomers.TabIndex = 8;
			this.buttonRefreshCustomers.Text = "Refresh Customers";
			this.buttonRefreshCustomers.UseVisualStyleBackColor = true;
			// 
			// buttonUpdateCustomer
			// 
			this.buttonUpdateCustomer.Location = new System.Drawing.Point(12, 148);
			this.buttonUpdateCustomer.Name = "buttonUpdateCustomer";
			this.buttonUpdateCustomer.Size = new System.Drawing.Size(260, 23);
			this.buttonUpdateCustomer.TabIndex = 0;
			this.buttonUpdateCustomer.Text = "UpdateCustomer";
			this.buttonUpdateCustomer.UseVisualStyleBackColor = true;
			// 
			// comboBoxCurrentCustomer
			// 
			this.comboBoxCurrentCustomer.FormattingEnabled = true;
			this.comboBoxCurrentCustomer.Location = new System.Drawing.Point(12, 41);
			this.comboBoxCurrentCustomer.Name = "comboBoxCurrentCustomer";
			this.comboBoxCurrentCustomer.Size = new System.Drawing.Size(260, 21);
			this.comboBoxCurrentCustomer.TabIndex = 1;
			// 
			// textBoxCustomerID
			// 
			this.textBoxCustomerID.Location = new System.Drawing.Point(59, 68);
			this.textBoxCustomerID.Name = "textBoxCustomerID";
			this.textBoxCustomerID.Size = new System.Drawing.Size(213, 20);
			this.textBoxCustomerID.TabIndex = 2;
			// 
			// textBoxCustomerName
			// 
			this.textBoxCustomerName.Location = new System.Drawing.Point(59, 95);
			this.textBoxCustomerName.Name = "textBoxCustomerName";
			this.textBoxCustomerName.Size = new System.Drawing.Size(213, 20);
			this.textBoxCustomerName.TabIndex = 3;
			// 
			// textBoxCustomerPhone
			// 
			this.textBoxCustomerPhone.Location = new System.Drawing.Point(59, 122);
			this.textBoxCustomerPhone.Name = "textBoxCustomerPhone";
			this.textBoxCustomerPhone.Size = new System.Drawing.Size(213, 20);
			this.textBoxCustomerPhone.TabIndex = 4;
			// 
			// labelCustomerID
			// 
			this.labelCustomerID.AutoSize = true;
			this.labelCustomerID.Location = new System.Drawing.Point(12, 71);
			this.labelCustomerID.Name = "labelCustomerID";
			this.labelCustomerID.Size = new System.Drawing.Size(18, 13);
			this.labelCustomerID.TabIndex = 5;
			this.labelCustomerID.Text = "ID";
			// 
			// labelCustomerName
			// 
			this.labelCustomerName.AutoSize = true;
			this.labelCustomerName.Location = new System.Drawing.Point(12, 98);
			this.labelCustomerName.Name = "labelCustomerName";
			this.labelCustomerName.Size = new System.Drawing.Size(35, 13);
			this.labelCustomerName.TabIndex = 6;
			this.labelCustomerName.Text = "Name";
			// 
			// labelCustomerPhone
			// 
			this.labelCustomerPhone.AutoSize = true;
			this.labelCustomerPhone.Location = new System.Drawing.Point(12, 125);
			this.labelCustomerPhone.Name = "labelCustomerPhone";
			this.labelCustomerPhone.Size = new System.Drawing.Size(38, 13);
			this.labelCustomerPhone.TabIndex = 7;
			this.labelCustomerPhone.Text = "Phone";
			// 
			// customerBindingSource
			// 
			this.customerBindingSource.DataSource = typeof(SimpleMvvm.Model.Customer);
			// 
			// CustomerForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(284, 182);
			this.Controls.Add(this.buttonRefreshCustomers);
			this.Controls.Add(this.labelCustomerPhone);
			this.Controls.Add(this.labelCustomerName);
			this.Controls.Add(this.labelCustomerID);
			this.Controls.Add(this.textBoxCustomerPhone);
			this.Controls.Add(this.textBoxCustomerName);
			this.Controls.Add(this.textBoxCustomerID);
			this.Controls.Add(this.comboBoxCurrentCustomer);
			this.Controls.Add(this.buttonUpdateCustomer);
			this.Name = "CustomerForm";
			this.Text = "CustomerForm";
			((System.ComponentModel.ISupportInitialize)(this.customerBindingSource)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button buttonRefreshCustomers;
		private System.Windows.Forms.Button buttonUpdateCustomer;
		private System.Windows.Forms.ComboBox comboBoxCurrentCustomer;
		private System.Windows.Forms.TextBox textBoxCustomerID;
		private System.Windows.Forms.TextBox textBoxCustomerName;
		private System.Windows.Forms.TextBox textBoxCustomerPhone;
		private System.Windows.Forms.Label labelCustomerID;
		private System.Windows.Forms.Label labelCustomerName;
		private System.Windows.Forms.Label labelCustomerPhone;
		private System.Windows.Forms.BindingSource customerBindingSource;
	}
}

