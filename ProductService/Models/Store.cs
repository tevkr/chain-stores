using System.ComponentModel.DataAnnotations;

namespace ProductService.Models
{
    public class Store
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public int ExternalId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}