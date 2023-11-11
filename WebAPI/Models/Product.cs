using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } 
        public string Name { get; set; }

        [Precision(18, 2)]
        public decimal Price{ get; set; }
        public int Stock { get; set; }
        public int CategoryId { get; set; }
        List<SaleDetail> Details { get; set; }

    }
}
