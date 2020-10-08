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


namespace CustomerManager.Test
{
    public class UnitTest1
    {

        //[Fact]
        //public void Index_Posts_Get_Data()
        //{
        //    //Arrange
        //    var context = CreateDbContext();
        //    var service = new CustomerController(context.Object);

        //    //Act

        //    var customer = service.Index(1);

        //    var customerCreate = service.Create();

        //    //Assert
        //    Assert.Equal(1, customer.Id);

        //}



        private Mock<IApplicationDbContext> CreateDbContext()
        {
            IQueryable<Customer> customers = new List<Customer>
            {
                new Customer
                {
                    Id = 2,
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
                    Id = 3,
                    FirstName = "Helen",
                    MiddleName = "Rachel",
                    LastName = "Chambers",
                    Address = "102 Regent Street",
                    Email = "helen@gmail.com",
                    PhoneNumber = "0105886775",
                    DateCreated = DateTime.Now.ToShortDateString()
                },




            }.AsQueryable();

            var dbSetMock = new Mock<DbSet<Customer>>();
            dbSetMock.As<IQueryable<Customer>>().Setup(m => m.Provider).Returns(customers.Provider);
            dbSetMock.As<IQueryable<Customer>>().Setup(m => m.Expression).Returns(customers.Expression);
            dbSetMock.As<IQueryable<Customer>>().Setup(m => m.ElementType).Returns(customers.ElementType);
            dbSetMock.As<IQueryable<Customer>>().Setup(m => m.GetEnumerator()).Returns(customers.GetEnumerator());

            var dbContextMock = new Mock<IApplicationDbContext>();
            dbContextMock.Setup(x => x.Customers).Returns(dbSetMock.Object);

            return dbContextMock;
        }

        [Fact]
        public void Create_Should_Return_Invalid_Model()
        {
            // Arrange
            var context = CreateDbContext();

            var service = new CustomerController(context.Object);
            service.ModelState.AddModelError("Failed", "Failed");

            var newCustomer = new Customer();
            newCustomer.FirstName = "Jack";


            // Act
            var result = service.Create(newCustomer, null).Result;

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Customer>(
                viewResult.ViewData.Model);



            // Assert
            Assert.Equal("Jack", model.FirstName);
            
        }

        [Fact]
        public async Task Create_Should_Post_Valid_Model()
        {
            // Arrange
            var context = CreateDbContext();
            var service = new CustomerController(context.Object);

            var newCustomer = new Customer();
            
           
            // Act
            var result = await service.Create(newCustomer, null);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<StaticPagedList<Customer>>(
                viewResult.ViewData.Model);

            
            Assert.Equal(2, model.Count());
            Assert.Equal("Index", viewResult.ViewName);
            
            

        }
    }
}
