using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Database;
using WebAPI.DTOs;
using WebAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public CustomerController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        // GET: api/<CustomerController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var customers = await _context.Customers.ToListAsync();
            return Ok(customers);
        }

        // GET api/<CustomerController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Id == id);
            if (customer == null)
                return NotFound();
            
            return Ok(customer);
        }

        // POST api/<CustomerController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CustomerDTO customer)
        {
            var customerInfo = _mapper.Map<Customer>(customer);
            await _context.Customers.AddAsync(customerInfo);
            await _context.SaveChangesAsync();
            return Ok();
        }

        // PUT api/<CustomerController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] CustomerDTO customer)
        {
            var customerSearch = await _context.Customers.FirstOrDefaultAsync(c => c.Id == id);
            if (customerSearch == null)
                return NotFound();
            
            customerSearch.Adress  = customer.Adress;
            customerSearch.Name = customer.Name;
            customerSearch.Email = customer.Email;
            customerSearch.Phone = customer.Phone;

            _context.Customers.Update(customerSearch);
            await _context.SaveChangesAsync();
            return Ok(customer);
        }

        // DELETE api/<CustomerController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var customer =  await _context.Customers.FirstOrDefaultAsync(c=>c.Id == id);
            if (customer == null)
                return NotFound();

             _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
