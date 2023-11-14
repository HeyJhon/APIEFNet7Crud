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
    public class SellerController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public SellerController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        // GET: api/<SellerController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var seller = await _context.Sellers.ToListAsync();
            return Ok(seller);
        }

        // GET api/<SellerController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var seller = await _context.Sellers.FirstOrDefaultAsync(c => c.Id == id);
            if (seller == null)
                return NotFound();
            
            return Ok(seller);
        }

        // POST api/<SellerController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SellerDTO seller)
        {
            var sellerInfo = _mapper.Map<Seller>(seller);
            await _context.Sellers.AddAsync(sellerInfo);
            await _context.SaveChangesAsync();
            return Ok();
        }

        // PUT api/<SellerController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] SellerDTO seller)
        {
            var sellerSearch = await _context.Sellers.FirstOrDefaultAsync(c => c.Id == id);
            if (sellerSearch == null)
                return NotFound();
            
            sellerSearch.Adress  = seller.Adress;
            sellerSearch.Name = seller.Name!;
            sellerSearch.Email = seller.Email!;
            sellerSearch.Phone = seller.Phone!;

            _context.Sellers.Update(sellerSearch);
            await _context.SaveChangesAsync();
            return Ok(seller);
        }

        // DELETE api/<SellerController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var seller =  await _context.Sellers.FirstOrDefaultAsync(c=>c.Id == id);
            if (seller == null)
                return NotFound();

             _context.Sellers.Remove(seller);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
