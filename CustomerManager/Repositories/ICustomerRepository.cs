using CustomerManager.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CustomerManager.Repositories
{
    public interface ICustomerRepository
    {
        Customer GetCustomer(int id);
        IEnumerable<Customer> GetAllCustomers();
        Customer Add(Customer customer);
        Customer Update(Customer customerChanges);
        Customer Delete(int id);


    }
}
