using System.ComponentModel.DataAnnotations;

namespace Employee_Management.Dto
{
    public class EmployeeDto
    {
        public int EmpId { get; set; }

        [StringLength(30)]
        public string Name { get; set; }

        [Range(21, 100)]
        public int Age { get; set; }

        public int DepartmentId { get; set; }

        public int Salary { get; set; }

    }
}
