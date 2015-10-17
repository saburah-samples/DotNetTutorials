using SimpleMvvm.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleMvvm.ViewModel
{
	public class CustomerViewModel : ViewModelBase
	{
		private List<Customer> customers;
		private Customer currentCustomer;
		private CustomerRepository repository;

		public CustomerViewModel()
		{
			repository = new CustomerRepository();
			customers = repository.GetCustomers();

			WireCommands();
		}

		private void WireCommands()
		{
			UpdateCustomerCommand = new RelayCommand(UpdateCustomer);
		}

		public RelayCommand UpdateCustomerCommand
		{
			get;
			private set;
		}

		public List<Customer> Customers
		{
			get { return customers; }
			set { customers = value; }
		}

		public Customer CurrentCustomer
		{
			get
			{
				return currentCustomer;
			}

			set
			{
				if (currentCustomer != value)
				{
					currentCustomer = value;
					OnPropertyChanged("CurrentCustomer");
					UpdateCustomerCommand.IsEnabled = true;
				}
			}
		}

		public void UpdateCustomer()
		{
			repository.UpdateCustomer(CurrentCustomer);
		}
	}
}