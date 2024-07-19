using RestApi.Models;

namespace RestApi.DataAccess;

public interface ICustomerRepository
{
    Task Add(Customer customer);
    Task Remove(uint id);
    Task<IEnumerable<Customer>> GetAll();
}