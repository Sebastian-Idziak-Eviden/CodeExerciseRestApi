using RestApi.Models;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace RestApi.DataAccess;

public class InMemoryTestCustomerRepository : ICustomerRepository
{
    private readonly SynchronizedCollection<Customer> _customers = [];

    public async Task Add(Customer customer)
    {
        await Task.Run(() =>
        {
            if (customer.Id == 0)
            {
                customer.Id = (_customers.ToList().Max(c => (uint?)c.Id) ?? 0) + 1;
            }
            else if (_customers.Any(c => c.Id == customer.Id))
            {
                throw new Exception($"Customer with id: {customer.Id} already exists");
            }
            _customers.Add(customer);
        });
    }

    public async Task Remove(uint id)
    {
        await Task.Run(() =>
        {
            var customer = _customers.FirstOrDefault(c => c.Id == id);
            if (customer is null)
            {
                throw new Exception($"There is no Customer with id: {id}");
            }
            _customers.Remove(customer);
        });
    }

    public async Task<IEnumerable<Customer>> GetAll()
    {
        return await Task.Run(() => _customers.AsEnumerable());
    }
}