using System;
namespace SimpleMvvm.Model
{
	public interface ICustomerRepository
	{
		System.Collections.Generic.List<Customer> GetCustomers();
		void UpdateCustomer(Customer SelectedCustomer);
	}
}
