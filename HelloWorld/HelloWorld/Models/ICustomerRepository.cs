using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld.Models
{
    public interface ICustomerRepository
    {
        Customer Find(Predicate<Customer> filter);
        IEnumerable<Customer> FindAll(Predicate<Customer> filter = null);
    }
}
