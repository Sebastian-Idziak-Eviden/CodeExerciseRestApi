using RestApi.DataAccess;
using RestApi.Models;
using RestApi.Services;

namespace RestApi.UnitTest;

public class CustomerServiceUnitTest
{
    public readonly ICustomerService _customerService;

    public CustomerServiceUnitTest()
    {
        var testRepository = new InMemoryTestCustomerRepository(); // Could use Moq but for the sake of time...
        _customerService = new CustomerService(testRepository);
        testRepository.Add(new Customer
        {
            Id = 2,
            Firstname = "John",
            Surname = "Smith",
        }).Wait();
        testRepository.Add(new Customer
        {
            Id = 3,
            Firstname = "Alice",
            Surname = "Wonder",
        }).Wait();
    }

    [Fact]
    public async Task ListAll_Successful()
    {
        //Act
        var customers = await _customerService.GetAll();

        //Assert
        Assert.NotNull(customers);
        Assert.Equal(2, customers.ToList().Count);
    }

    [Fact]
    public async Task AddCustomer_Successful()
    {
        //Arrange
        var customerToAdd = new Customer
        {
            Firstname = "Bob",
            Surname = "Kiwi"
        };

        //Act
        await _customerService.Add(customerToAdd);
        var customers = await _customerService.GetAll();

        //Assert
        Assert.NotNull(customers);
        Assert.Equal(3, customers.ToList().Count);
        Assert.Contains(customers, (customer => customer.Firstname == "Bob"));
    }

    [Fact]
    public async Task AddCustomer_Fail()
    {
        //Arrange
        var customerToAdd = new Customer
        {
            Id = 2,
            Firstname = "Bob",
            Surname = "Kiwi"
        };

        //Act & Assert
        await Assert.ThrowsAsync<Exception>(async () => await _customerService.Add(customerToAdd));
    }

    [Fact]
    public async Task RemoveCustomer_Successful()
    {
        //Act
        await _customerService.Remove(2); // Remove John
        var customers = await _customerService.GetAll();

        //Assert
        Assert.NotNull(customers);
        Assert.Equal(1, customers.ToList().Count);
        Assert.DoesNotContain(customers, (customer => customer.Firstname == "John"));
    }

    [Fact]
    public async Task RemoveCustomer_Fail()
    {
        //Act & Assert
        await Assert.ThrowsAsync<Exception>(async () => await _customerService.Remove(uint.MaxValue));
    }
}