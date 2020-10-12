﻿using CustomerManager.Models;
using CustomerManager.Persistence;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerManager.Repositories
{
    public class SQLCustomerRepository : ICustomerRepository
    {

        private readonly ApplicationDbContext _context;

        public SQLCustomerRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public Customer Add(Customer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();
            return customer;
        }

        public Customer Delete(int id)
        {
            Customer customer = _context.Customers.Find(id);
            if(customer != null)
            {
                _context.Customers.Remove(customer);
                _context.SaveChanges();
            }
            return customer;
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            return _context.Customers;
        }

        public Customer GetCustomer(int id)
        {
            Customer customer = _context.Customers.Find(id);
            return customer;
        }

        public Customer Update(Customer customerChanges)
        {
            var customer = _context.Customers.Attach(customerChanges);
            customer.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return customerChanges; 
        }
    }
}