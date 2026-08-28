using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DemoAngularCrudApi.Models;

namespace DemoAngularCrudApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [EnableCors("SmartCors")]
    // [Produces("application/json")]
    public class CustomersController : ControllerBase
    {
        // private readonly AngCustDBContext _context;

        // public CustomersController(AngCustDBContext context)
        // {
        //     _context = context;
        // }

        // Customer customer = new Customer()
        // {
        //     Id = 1,
        //     Name = "John Doe",
        //     PhoneNo = "123-456-7890",
        //     Address = "123 Main St, Anytown, USA",
        //     Amt = 100
        // };
        private static List<Customer> customers = new List<Customer>
        {
            new Customer { Id = 1, Name = "John Doe", PhoneNo = "123-456-7890", Address = "123 Main St, Anytown, USA", Amt = 100 },
            new Customer { Id = 2, Name = "Jane Smith", PhoneNo = "987-654-3210", Address = "456 Elm St, Othertown, USA", Amt = 200 },
            new Customer { Id = 3, Name = "Alice Johnson", PhoneNo = "555-555-5555", Address = "789 Oak St, Sometown, USA", Amt = 300 },
            new Customer { Id = 4, Name = "Bob Brown", PhoneNo = "444-444-4444", Address = "321 Pine St, Anycity, USA", Amt = 400 }
        };
        // GET: api/Customers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
          return await Task.FromResult(customers);
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
          var customer = customers.FirstOrDefault(c => c.Id == id);
          if (customer == null)
          {
              return NotFound();
          }
            return customer;
        }

        // PUT: api/Customers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(int id, Customer customer)
        {
            if (id != customer.Id)
            {
                return BadRequest();
            }

           var existingCustomer = customers.FirstOrDefault(c => c.Id == id);
           if (existingCustomer == null)
           {
               return NotFound();
           }

           existingCustomer.Name = customer.Name;
           existingCustomer.PhoneNo = customer.PhoneNo;
           existingCustomer.Address = customer.Address;
           existingCustomer.Amt = customer.Amt;

            try
            {
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                if (!CustomerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw new Exception(ex.Message);
                }
            }

            return NoContent();
        }

        // POST: api/Customers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(Customer customer)
        {
          if (customers == null)
          {
              return Problem("Entity set 'AngCustDBContext.Customers'  is null.");
          }

            customers.Add(customer);

            return await Task.FromResult(customer);
        }

            //return CreatedAtAction("GetCustomer", new { id = customer.Id }, customer);

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            if (customers == null)
            {
                return NotFound();
            }
            var customer = customers.FirstOrDefault(c => c.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            customers.Remove(customer);
            await Task.CompletedTask;

            return NoContent();
        }

        private bool CustomerExists(int id)
        {
            return (customers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
