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
		private Customer originCustomer;
		private ICustomerRepository repository;

		public CustomerViewModel()
		{
			repository = ModelContext.Default.CustomerRepository;
			customers = repository.GetCustomers();

			WireCommands();
		}

		private void WireCommands()
		{
			UpdateCustomerCommand = new RelayCommand(UpdateCustomer);
			RefreshCustomersCommand = new RelayCommand(RefreshCustomers);

			RefreshCustomersCommand.IsEnabled = true;
		}

		public RelayCommand UpdateCustomerCommand
		{
			get;
			private set;
		}

		public RelayCommand RefreshCustomersCommand
		{
			get;
			private set;
		}

		public List<Customer> Customers
		{
			get { return customers; }
			set
			{
				if (customers != value)
				{
					customers = value;
					OnPropertyChanged("Customers");
				}
			}
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
					if (value == null)
					{
						originCustomer = null;
					}
					else
					{
						originCustomer = new Customer
						{
							CustomerID = value.CustomerID,
							FullName = value.FullName,
							Phone = value.Phone
						};
					}
					currentCustomer = value;
					UpdateCustomerCommand.IsEnabled = true;
					OnPropertyChanged("CurrentCustomer");
				}
			}
		}

		public void UpdateCustomer()
		{
			repository.UpdateCustomer(CurrentCustomer);
		}

		public bool CanUpdateCustomer()
		{
			return UpdateCustomerCommand.IsEnabled;
		}

		public void RefreshCustomers()
		{
			Customers = repository.GetCustomers();
		}

		public bool CanRefreshCustomers()
		{
			return RefreshCustomersCommand.IsEnabled;
		}
	}
}