using CustomerManager.Models;
using CustomerManager.Persistence;
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
        private IApplicationDbContext _db;

        public CustomerController(IApplicationDbContext db)
        {
            _db = db;
        }

        // GET: Index
        public async Task<IActionResult> Index(int? page)
        {

            var pageNumber = page ?? 1;

            var customerDetails = await _db.Customers.OrderBy(m => m.Id).ToPagedListAsync(pageNumber, 6);
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

            _db.Customers.Add(customer);
            await _db.SaveChangesAsync();

            var pageNumber = page ?? 1;
           
            var customerDetails = await _db.Customers.OrderBy(m => m.Id).ToPagedListAsync(pageNumber, 6);

            return View("Index", customerDetails);
        }

        // GET: Edit
        public IActionResult Edit(int id)
        {
            var result = _db.Customers.Where(m => m.Id == id).FirstOrDefault();

            return View(result);
        }
        // POST: Edit
        [HttpPost]
        public IActionResult Edit(Customer customer, int? page)
        {
            if (!ModelState.IsValid)
            {
                return View(customer);
            }
            _db.Customers.Remove(customer);
            _db.SaveChanges();

            var editedCustomerDetails = new Customer();

            editedCustomerDetails.FirstName = customer.FirstName;
            editedCustomerDetails.MiddleName = customer.MiddleName;
            editedCustomerDetails.LastName = customer.LastName;
            editedCustomerDetails.PhoneNumber = customer.PhoneNumber;
            editedCustomerDetails.Address = customer.Address;
            editedCustomerDetails.Email = customer.Email;
            editedCustomerDetails.DateCreated = DateTime.Now.ToShortDateString();

            _db.Customers.Add(editedCustomerDetails);
            _db.SaveChanges();

            var pageNumber = page ?? 1;
            var collectionList = _db.Customers.OrderBy(m => m.Id).ToList().ToPagedList(pageNumber, 6);


            return View("Index", collectionList);
        }

        //GET: Delete
        public IActionResult Delete(int id)
        {
            var result = _db.Customers.Where(m => m.Id == id).FirstOrDefault();
            return View(result);
        }

        //POST: Delete
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteCustomer(int id, int? page)
        {
            var result = _db.Customers.Where(m => m.Id == id).FirstOrDefault();
            _db.Customers.Remove(result);
            _db.SaveChanges();

            var pageNumber = page ?? 1;
            var collectionList = _db.Customers.OrderBy(m => m.Id).ToList().ToPagedList(pageNumber, 6);
            return View("Index", collectionList);
        }
    }
}
