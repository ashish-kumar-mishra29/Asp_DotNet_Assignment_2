using Employee_Management.Dto;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Employee_Management.Entities
{
    public class Employee
    {
        [Key]
        public int EmpId { get; set; }

        [StringLength(30)]
        public  string Name { get; set; }

        [Range(21, 100)]
        public int Age { get; set; }

        public int DepartmentId { get; set; }

        [ForeignKey("DepartmentId")]
        public Department Department { get; set; }

        public  int Salary { get; set; }
    }
}
