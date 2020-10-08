using CustomerManager.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerManager.NewCustomer
{
    public class NewCustomerProcessor : INewCustomerProcessor
    {
        public void CreateCustomer(Customer customer)
        {
            if(customer.FirstName == null)
            {
                throw new ArgumentException("Customer FirstName cannot be null");
            }
        }
    }
}
