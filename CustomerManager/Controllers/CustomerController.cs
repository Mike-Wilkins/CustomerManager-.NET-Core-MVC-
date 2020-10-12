using CustomerManager.Models;
using CustomerManager.Persistence;
using CustomerManager.Repositories;
using CustomerManager.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace CustomerManager.Controllers
{
    public class CustomerController : Controller
    {
        private ICustomerRepository _db;

        public CustomerController(ICustomerRepository db)
        {
            _db = db;
        }

        // GET: Index
        public async Task<IActionResult> Index(int? page)
        {

            var pageNumber = page ?? 1;
            var customers = _db.GetAllCustomers();

            var customerDetails = await customers.OrderBy(m => m.Id).ToPagedListAsync(pageNumber, 6);
            return View(customerDetails);
        }

        // GET: Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Create
        [HttpPost]
        public async Task<IActionResult> Create(Customer customer, int? page)
        {
            if (!ModelState.IsValid)
            {
                return View(customer);
            }

            customer.DateCreated = DateTime.Now.ToShortDateString();

            _db.Add(customer);
            var pageNumber = page ?? 1;
            var customers = _db.GetAllCustomers();
            var customerDetails = await customers.OrderBy(m => m.Id).ToPagedListAsync(pageNumber, 6);

            return View("Index", customerDetails);
        }

        // GET: Edit
        public IActionResult Edit(int id)
        {
            var customer = _db.GetCustomer(id);
            return View(customer);
        }

        // POST: Edit
        [HttpPost]
        public IActionResult Edit(Customer customer, int? page)
        {
            if (!ModelState.IsValid)
            {
                return View(customer);
            }
            
            _db.Delete(customer.Id);

            var editedCustomerDetails = new Customer();

            editedCustomerDetails.FirstName = customer.FirstName;
            editedCustomerDetails.MiddleName = customer.MiddleName;
            editedCustomerDetails.LastName = customer.LastName;
            editedCustomerDetails.PhoneNumber = customer.PhoneNumber;
            editedCustomerDetails.Address = customer.Address;
            editedCustomerDetails.Email = customer.Email;
            editedCustomerDetails.DateCreated = DateTime.Now.ToShortDateString();

            _db.Add(editedCustomerDetails);

            var pageNumber = page ?? 1;
            var customers = _db.GetAllCustomers();
            var collectionList = customers.OrderBy(m => m.Id).ToList().ToPagedList(pageNumber, 6);

            return View("Index", collectionList);
        }


        //GET: Delete
        public IActionResult Delete(int id)
        {
            var customer = _db.GetCustomer(id);
            return View(customer);
        }

        //POST: Delete
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteCustomer(int id, int? page)
        {
            _db.Delete(id);
            var customers = _db.GetAllCustomers();
            var pageNumber = page ?? 1;
            var collectionList = customers.OrderBy(m => m.Id).ToList().ToPagedList(pageNumber, 6);
            return View("Index", collectionList);
        }
    }
}
