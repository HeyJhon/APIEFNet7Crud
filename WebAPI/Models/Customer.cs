using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace WebAPI.Models
{
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        [AllowNull]
        public string Phone { get; set; }
        [AllowNull]
        public string Adress { get; set; }
        [JsonIgnore]
        public virtual List<Sale>? Sale { get; set; }

    }
}
