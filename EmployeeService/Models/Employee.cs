using System.ComponentModel.DataAnnotations;

namespace EmployeeService.Models
{
    public class Employee
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public int StoreId { get; set; }
        [Required]
        public string Fullname { get; set; }
        [Required]
        public string JobPosition { get; set; }
        [Required]
        public int Salary { get; set; }
        public Store Store { get; set;}
    }
}