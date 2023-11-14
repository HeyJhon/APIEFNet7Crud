using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Database;
using WebAPI.DTOs;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public ProductController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/<ProductController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var products = await _context.Products
                .Join(
                    _context.Categories,
                    product => product.CategoryId,
                    category => category.Id,
                    (product, category) => new ProductDTO
                    {
                        Id = product.Id,
                        Name = product.Name,
                        Stock = product.Stock,
                        Price = product.Price,
                        CategoryId = product.CategoryId,
                        CategoryName = category.Description
                    })
                .ToListAsync();
            return Ok(products);
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {

            var product = await _context.Products
                .Join(
                _context.Categories,
                product => product.CategoryId,
                category => category.Id, (product, category) => new ProductDTO
                {
                    Id = product.Id,
                    Name = product.Name,
                    Stock = product.Stock,
                    Price = product.Price,
                    CategoryId = product.CategoryId,
                    CategoryName = category.Description
                }).FirstOrDefaultAsync(p=>p.Id == id);

            if (product == null)
                return NotFound();

            return Ok(product);
        }

        // POST api/<ProductController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProductDTO product)
        {
            var productInfo = _mapper.Map<Product>(product);
            await _context.Products.AddAsync(productInfo);
            await _context.SaveChangesAsync();
            return Ok();
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ProductDTO product)
        {
            var productSearch = await _context.Products.FirstOrDefaultAsync(c => c.Id == id);
            if (productSearch == null)
                return NotFound();

            productSearch.Name = product.Name;
            productSearch.Price = product.Price;
            productSearch.Stock = product.Stock;
            productSearch.CategoryId = product.CategoryId;

            _context.Products.Update(productSearch);
            await _context.SaveChangesAsync();
            return Ok(product);
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(c => c.Id == id);
            if (product == null)
                return NotFound();

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
