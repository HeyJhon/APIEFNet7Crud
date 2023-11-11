using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    public class SaleDetail
    {

        [Key]
        public int SaleId { get; set; }
        [Key]
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        [Precision(18, 2)]
        public decimal SalePrice{ get; set; }
        public Sale Sale { get; set; }
        public Product Product { get; set; }
    }
}
