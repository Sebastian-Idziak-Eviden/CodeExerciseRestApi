using RestApi.Models;

namespace RestApi.Services;

public interface ICustomerService
{
    Task Add(Customer customer);
    Task Remove(uint id);
    Task<IEnumerable<Customer>> GetAll();
}