using RestApi.DataAccess;
using RestApi.Models;

namespace RestApi.Services;

public class CustomerService(ICustomerRepository customerRepository) : ICustomerService
{
    public async Task Add(Customer customer)
    {
        await customerRepository.Add(customer);
    }

    public async Task Remove(uint id)
    {
        await customerRepository.Remove(id);
    }

    public async Task<IEnumerable<Customer>> GetAll()
    {
        return await customerRepository.GetAll();
    }
}