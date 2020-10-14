using CustomerManager.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomerManager.Repositories
{
    public interface ICustomerRepository
    {
        Customer GetCustomer(int id);
        //Task<IEnumerable<Customer>> GetAllCustomers(int page);
        IEnumerable<Customer> GetAllCustomers(int page);
        Customer Add(Customer customer);
        Customer Update(Customer customerChanges);
        Customer Delete(int id);


    }
}
