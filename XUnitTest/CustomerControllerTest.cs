using CustomerManager.Controllers;
using CustomerManager.Services;
using Xunit;

namespace XUnitTest
{
    public class CustomerControllerTest
    {

        private IApplicationDbContext _db;

        public CustomerControllerTest(IApplicationDbContext db)
        {
            _db = db;
        }

        [Fact]
        public void ValidateCustomerFirstName()
        {




            ////Arrange
            //int expected = 5;

            //// Act
            //int actual = CustomerController.AddNums(2, 3);

            ////Assert
            //Assert.Equal(expected, actual);

        }


    }
}
