using System.Diagnostics.CodeAnalysis;

namespace WebAPI.DTOs
{
    public class CustomerDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Adress { get; set; }
    }
}
