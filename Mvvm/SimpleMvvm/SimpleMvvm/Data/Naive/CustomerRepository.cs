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
			return customers.Select(e => new Customer
			{
				CustomerID = e.CustomerID,
				FullName = e.FullName,
				Phone = e.Phone,
			}).ToList();
		}

		public void UpdateCustomer(Customer customer)
		{
			Customer customerToChange = customers.Single(c => c.CustomerID == customer.CustomerID);
			customerToChange.FullName = customer.FullName;
			customerToChange.Phone = customer.Phone;
		}
	}
}
