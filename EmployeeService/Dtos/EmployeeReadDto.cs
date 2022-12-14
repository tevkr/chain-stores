namespace EmployeeService.Dtos
{
    public class EmployeeReadDto
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        public string Fullname { get; set; }
        public string JobPosition { get; set; }
        public int Salary { get; set; }
    }
}