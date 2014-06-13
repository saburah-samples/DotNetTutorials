using HelloWorld.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld.ViewModels
{
    public class CustomerListViewModel: ViewModelBase
    {
        private ICustomerRepository repository;
        private Customer currentCustomer;

        public CustomerListViewModel()
            :this(new FakeRepository())
        { }
        
        public CustomerListViewModel(ICustomerRepository repository)
        {
            Debug.Assert(repository != null, "Argument 'repository' is null");
            this.repository = repository;
            
            Customers = new ObservableCollection<Customer>(this.repository.FindAll());
            CreateCommands();
        }

        private void CreateCommands()
        {
            this.AddCustomerCommand = new DelegateCommand(AddCustomer);
            this.EditCustomerCommand = new DelegateCommand(EditCustomer, p => { return CurrentCustomer != null; });
            this.DeleteCustomerCommand = new DelegateCommand(DeleteCustomer, p => { return CurrentCustomer != null; });
        }

        private void AddCustomer(object obj)
        {
            //throw new NotImplementedException();
            Customers.Add(repository.Find(null));
        }

        private void EditCustomer(object obj)
        {
            throw new NotImplementedException();
        }

        private void DeleteCustomer(object obj)
        {
            //throw new NotImplementedException();
            Customers.Remove(repository.Find(null));
        }

        public ICollection<Customer> Customers { get; private set; }
        public Customer CurrentCustomer
        {
            get { return this.currentCustomer; }
            set
            {
                if (this.currentCustomer != value)
                {
                    this.currentCustomer = value;
                    OnPropertyChanged("Customer");
                }
            }
        }

        public DelegateCommand AddCustomerCommand { get; private set; }
        public DelegateCommand EditCustomerCommand { get; private set; }
        public DelegateCommand DeleteCustomerCommand { get; private set; }

        class FakeRepository : ICustomerRepository
        {
            private List<Customer> customers;
            public FakeRepository()
            {
                customers = new List<Customer>
                {
                    new Customer(){ Id = 1, FullName="Dana Birkby", Phone="394-555-0181"},
                    new Customer(){ Id = 2, FullName="Adriana Giorgi", Phone="117-555-0119"},
                    new Customer(){ Id = 3, FullName="Wei Yu", Phone="798-555-0118"}
                };
            }


            public Customer Find(Predicate<Customer> filter)
            {
                return customers[0];
            }

            public IEnumerable<Customer> FindAll(Predicate<Customer> filter = null)
            {
                return customers;
            }
        }

    }

}
