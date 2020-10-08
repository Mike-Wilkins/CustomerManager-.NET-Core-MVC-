using CustomerManager.Models;
using System;

namespace CustomerManager.NewCustomer
{
    interface INewCustomerProcessor
    {
        void CreateCustomer(Customer customer);

    }
}
