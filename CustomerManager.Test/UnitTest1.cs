using CustomerManager.Controllers;
using CustomerManager.Models;
using CustomerManager.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using X.PagedList;
using Autofac.Extras.Moq;
using CustomerManager.Repositories;
using CustomerManager.Persistence;
using Microsoft.EntityFrameworkCore.InMemory;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace CustomerManager.Test
{
    public class UnitTest1
    {

        private List<Customer> CustomerInMemoryDb()
        {

            List<Customer> customerList = new List<Customer>()
            {
                new Customer
                {
                    Id = 1,
                    FirstName = "Jack",
                    MiddleName = "Harry",
                    LastName = "Williams",
                    Address = "9 Park Lane",
                    Email = "jack@gmail.com",
                    PhoneNumber = "01509234567",
                    DateCreated = DateTime.Now.ToShortDateString()
                },
                new Customer
                {
                    Id = 2,
                    FirstName = "Joe",
                    MiddleName = "Luke",
                    LastName = "Riley",
                    Address = "102 Regent Street",
                    Email = "joe@gmail.com",
                    PhoneNumber = "01509264567",
                    DateCreated = DateTime.Now.ToShortDateString()
                },

            };

            return customerList;
        }

        [Fact]
        public void SQLCustomerRepository_Should_Add_Customer()
        {
            ICustomerRepository sut = GetInMemoryCustomerRepository();
            List<Customer> customer = CustomerInMemoryDb();

            Customer savedCustomer = sut.Add(customer[0]);

            Assert.Single(sut.GetAllCustomers(1));

            Assert.Equal("Jack", savedCustomer.FirstName);
            Assert.Equal("Harry", savedCustomer.MiddleName);
            Assert.Equal("Williams", savedCustomer.LastName);
            Assert.Equal("9 Park Lane", savedCustomer.Address);
            Assert.Equal("jack@gmail.com", savedCustomer.Email);
            Assert.Equal("01509234567", savedCustomer.PhoneNumber);

        }


        [Fact]
        public void SQLCustomerRepository_Should_Delete_Customer()
        {
            ICustomerRepository sut = GetInMemoryCustomerRepository();
            List<Customer> customer = CustomerInMemoryDb();

            Customer savedCustomer = sut.Add(customer[0]);
           
            Assert.Single(sut.GetAllCustomers(1));

            Assert.Equal("Jack", savedCustomer.FirstName);
            Assert.Equal("jack@gmail.com", savedCustomer.Email);


            sut.Delete(customer[0].Id);
            Assert.Empty(sut.GetAllCustomers(1));

            

        }

        [Fact]
        public void SQLCustomerRepository_Add_Delete_From_Multiple_Customer_Entries()
        {
            ICustomerRepository sut = GetInMemoryCustomerRepository();

            List <Customer> customer = CustomerInMemoryDb();

            Customer savedCustomer1 = sut.Add(customer[0]);
            Customer savedCustomer2 = sut.Add(customer[1]);

            Assert.Equal(2, sut.GetAllCustomers(1).Count());
            

            Assert.Equal("Jack", savedCustomer1.FirstName);
            Assert.Equal("Joe", savedCustomer2.FirstName);
            

            sut.Delete(customer[0].Id);
            Assert.Single(sut.GetAllCustomers(1));
            
            Assert.Equal("Joe", sut.GetCustomer(2).FirstName);
            
          

        }

        [Fact]
        public void SQLCustomerRepository_Should_Get_Customer_By_ID()
        {
            ICustomerRepository sut = GetInMemoryCustomerRepository();
            List<Customer> customer = CustomerInMemoryDb();


            sut.Add(customer[0]);
            sut.Add(customer[1]);

            Assert.Equal(2, sut.GetAllCustomers(1).Count());

            Assert.Equal("Joe", sut.GetCustomer(2).FirstName);
            
            
        }


        private ICustomerRepository GetInMemoryCustomerRepository()
        {
            DbContextOptions<ApplicationDbContext> options;
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();

            builder.UseInMemoryDatabase(databaseName:"CustomerDatabase");
            
            options = builder.Options;
            ApplicationDbContext customerDataContext = new ApplicationDbContext(options);
            customerDataContext.Database.EnsureDeleted();
            customerDataContext.Database.EnsureCreated();

            return new SQLCustomerRepository(customerDataContext);
        }

       













        //[Fact]
        //public async Task Index_Should_Return_All_Customers()
        //{

        //    IQueryable<Customer> customers = new List<Customer>
        //    {
        //        new Customer
        //        {
        //            FirstName = "Jack",
        //            MiddleName = "Harry",
        //            LastName = "Williams",
        //            Address = "9 Park Lane",
        //            Email = "jack@gmail.com",
        //            PhoneNumber = "01509234567",
        //            DateCreated = DateTime.Now.ToShortDateString()
        //        },
        //         new Customer
        //        {
        //            Id = 3,
        //            FirstName = "Helen",
        //            MiddleName = "Rachel",
        //            LastName = "Chambers",
        //            Address = "102 Regent Street",
        //            Email = "helen@gmail.com",
        //            PhoneNumber = "0105886775",
        //            DateCreated = DateTime.Now.ToShortDateString()
        //        }
        //    }.AsQueryable();

        //    var dbSetMock = new Mock<DbSet<Customer>>();
        //    dbSetMock.As<IQueryable<Customer>>().Setup(m => m.Provider).Returns(customers.Provider);
        //    dbSetMock.As<IQueryable<Customer>>().Setup(m => m.Expression).Returns(customers.Expression);
        //    dbSetMock.As<IQueryable<Customer>>().Setup(m => m.ElementType).Returns(customers.ElementType);
        //    dbSetMock.As<IQueryable<Customer>>().Setup(m => m.GetEnumerator()).Returns(customers.GetEnumerator());

        //    var dbContextMock = new Mock<IApplicationDbContext>();
        //    dbContextMock.Setup(x => x.Customers).Returns(dbSetMock.Object);

            

        //    var context = dbContextMock.Object;

        //    var service = new CustomerController(context);



        //    var expected = customers;
        //    var result = await service.Index(null);

            

            //var viewResult = Assert.IsType<ViewResult>(result);
            ////var model = Assert.IsAssignableFrom<Customer>(viewResult.ViewData.Model);
            //var model = Assert.IsAssignableFrom<StaticPagedList<Customer>>(viewResult.ViewData.Model);

            //Assert.True(result != null);
            //Assert.Equal(2, model.Count);

          
        //}





    }

   
}
