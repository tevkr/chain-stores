using System.ComponentModel.DataAnnotations;

namespace StoreService.Dtos
{
    public class StoreWriteDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
    }
}