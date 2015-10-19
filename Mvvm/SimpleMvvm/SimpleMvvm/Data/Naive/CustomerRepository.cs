using SimpleMvvm.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleMvvm.Data.Naive
{
	public class CustomerRepository : ICustomerRepository
	{
		private List<Customer> customers;

		public CustomerRepository()
		{
			customers = new List<Customer>
			{
				new Customer(){ CustomerID = 1, FullName="Dana Birkby", Phone="394-555-0181"},
				new Customer(){ CustomerID = 2, FullName="Adriana Giorgi", Phone="117-555-0119"},
				new Customer(){ CustomerID = 3, FullName="Wei Yu", Phone="798-555-0118"}
			};
		}

		public List<Customer> GetCustomers()
		{
			return customers;
		}

		public void UpdateCustomer(Customer SelectedCustomer)
		{
			Customer customerToChange = customers.Single(c => c.CustomerID == SelectedCustomer.CustomerID);
			customerToChange = SelectedCustomer;
		}
	}
}
