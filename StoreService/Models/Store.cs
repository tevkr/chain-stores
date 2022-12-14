using System.ComponentModel.DataAnnotations;

namespace StoreService.Models
{
    public class Store
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
    }
}