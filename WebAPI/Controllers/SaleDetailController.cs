using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Database;
using WebAPI.DTOs;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleDetailController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public SaleDetailController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;            
        }

        [HttpGet]
        // GET: api/<SaleDetailController>
        public async Task<IActionResult> GetBySaleId(int saleId)
        {
            var saleDetail = await _context.SaleDetails.Where(sd=>sd.SaleId == saleId).ToListAsync();
            if(saleDetail == null || saleDetail.Count < 1)              
                return BadRequest($"No se encontraron partidas para la venta {saleId}");

            return Ok(saleDetail);
        }

        // POST api/<SaleDetailController>
        [HttpPost]
        public async Task<IActionResult> Post(SaleDetailDTO saleDetail)
        {        
            //Validamos que exista la venta
            var sale = await _context.Sales.SingleOrDefaultAsync(s => s.Id == saleDetail.SaleId);
            if (sale == null)
                return NotFound($"No se identificó la Venta: {saleDetail.SaleId}");

            //Validamos que exista el producto
            var product = await _context.Products.SingleOrDefaultAsync(p => p.Id == saleDetail.ProductId);
            if (product == null)
                return NotFound($"No se indentificó el Producto: {saleDetail.ProductId}");
            
            //Validamos Stock
            if (product.Stock < saleDetail.Quantity)
                return BadRequest($"La cantidad seleccionada es mayor que Stock disponible");

            //Validamos si existe un producto en la BD
            var saleDetailExist = await _context.SaleDetails.SingleOrDefaultAsync(
               sd => sd.ProductId == saleDetail.ProductId && sd.SaleId == saleDetail.SaleId);
            if (saleDetailExist != null)
            {
                saleDetailExist.Quantity += saleDetail.Quantity;
                //saleDetailExist.SalePrice = product.Price;
                _context.Attach(saleDetailExist);

                sale.Total += product.Price * saleDetail.Quantity;
                _context.Attach(sale);
            }
            else
            {
                var saleInfo = _mapper.Map<SaleDetail>(saleDetail);
                saleInfo.SalePrice = product.Price;
                await _context.SaleDetails.AddAsync(saleInfo);

                sale.Total += product.Price * saleInfo.Quantity;
                _context.Attach(sale);                
            }

            product.Stock -= saleDetail.Quantity;
            _context.Attach(product);

            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
