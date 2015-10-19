using SimpleMvvm.Model;
using SimpleMvvm.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SimpleForms
{
	public partial class CustomerForm : Form
	{
		private CustomerViewModel viewModel;

		public CustomerForm()
		{
			InitializeComponent();

			this.viewModel = new CustomerViewModel();
			this.viewModel.PropertyChanged += viewModel_PropertyChanged;
			this.buttonUpdateCustomer.Click += buttonUpdateCustomer_Click;
			this.buttonRefreshCustomers.Click += buttonRefreshCustomers_Click;
			this.customerBindingSource.CurrentItemChanged += customerBindingSource_CurrentItemChanged;

			InitializeBindings();
			this.customerBindingSource.DataSource = viewModel.Customers;
		}

		private void InitializeBindings()
		{
			((System.ComponentModel.ISupportInitialize)(this.customerBindingSource)).BeginInit();
			this.SuspendLayout();
			this.comboBoxCurrentCustomer.DataSource = this.customerBindingSource;
			this.comboBoxCurrentCustomer.DisplayMember = "FullName";
			this.textBoxCustomerID.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.customerBindingSource, "CustomerID", true));
			this.textBoxCustomerName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.customerBindingSource, "FullName", true));
			this.textBoxCustomerPhone.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.customerBindingSource, "Phone", true));
			((System.ComponentModel.ISupportInitialize)(this.customerBindingSource)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();
		}

		void viewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			this.buttonUpdateCustomer.Enabled = this.viewModel.CanUpdateCustomer();
			this.buttonRefreshCustomers.Enabled = this.viewModel.CanRefreshCustomers();
			if ("Customers".Equals(e.PropertyName))
			{
				customerBindingSource.DataSource = viewModel.Customers;
			}
		}

		void customerBindingSource_CurrentItemChanged(object sender, EventArgs e)
		{
			viewModel.CurrentCustomer = (Customer)customerBindingSource.Current;
		}

		void buttonUpdateCustomer_Click(object sender, EventArgs e)
		{
			this.viewModel.UpdateCustomer();
		}

		void buttonRefreshCustomers_Click(object sender, EventArgs e)
		{
			this.viewModel.RefreshCustomers();
		}
	}
}
