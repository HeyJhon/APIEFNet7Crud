using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Database;
using WebAPI.DTOs;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public SaleController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        // GET: api/<SaleController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {            
            var sales = await _context.Sales.ToListAsync();
            if (sales == null)
                return NotFound();
            return Ok(sales);            
        }

        //// GET: api/<SaleController>
        //[HttpGet]
        //public async Task<IActionResult> GetByDate(DateTime date)
        //{
        //    if(date!=null && date> DateTime.MinValue)
        //    {
        //        var sales = await _context.Sales.Where(s => s.Date >= date && s.Date <= date.AddDays(1)).ToListAsync();
        //        return Ok(sales);
        //    }
        //    return BadRequest($"Ingrese una fecha valida {date}");
        //}

        // POST api/<SaleController>
        [HttpPost]
        public async Task<IActionResult> Post(SaleDTO sale)
        {
            var saleInfo = _mapper.Map<Sale>(sale);
            saleInfo.Date = DateTime.Now;
            await _context.Sales.AddAsync(saleInfo);
            await _context.SaveChangesAsync();

            var insertedId = saleInfo.Id;
            return Ok(new { Id = insertedId });
        }

    }
}
