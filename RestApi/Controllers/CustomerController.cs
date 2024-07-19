using Microsoft.AspNetCore.Mvc;
using RestApi.Models;
using RestApi.Services;

namespace RestApi.Controllers;

[ApiController]
[Route("[controller]")]
public class CustomerController(
    ILogger<CustomerController> logger,
    ICustomerService customerService)
    : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IList<Customer>>> GetAll()
    {
        logger.LogInformation("Requested GetAll");
        var customers = await customerService.GetAll();
        return Ok(customers.ToList());
    }

    [HttpPost]
    public async Task<ActionResult<IList<Customer>>> Add([FromBody] Customer customer)
    {
        try
        {
            await customerService.Add(customer);
            logger.LogInformation("Added customer with data {data}", customer.ToString());
            return Ok("Successfully added Customer");
        }
        catch (Exception e)
        {
            logger.LogError(e, "Unable to add Customer with data {data}", customer.ToString());
            return BadRequest("Unable to add Customer");
        }
    }

    [HttpDelete("{customerId}")]
    public async Task<ActionResult<IList<Customer>>> Remove([FromRoute] uint customerId)
    {
        try
        {
            await customerService.Remove(customerId);
            logger.LogInformation("Removed customer with id {customerId}", customerId);
            return Ok("Successfully removed Customer");
        }
        catch (Exception e)
        {
            logger.LogError(e, "Unable to remove Customer with id {customerId}", customerId);
            return BadRequest("Unable to remove Customer");
        }
    }
}