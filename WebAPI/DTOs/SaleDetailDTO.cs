using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using WebAPI.Models;

namespace WebAPI.DTOs
{
    public class SaleDetailDTO
    {
        public int SaleId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
