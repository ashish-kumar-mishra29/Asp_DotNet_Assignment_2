using Employee_Management.Dto;
using System.ComponentModel.DataAnnotations;

namespace Employee_Management.Entities
{
    public class Department
    {
        [Key]
        public int DepartmentId { get; set; }

        public  string DepartmentName {  get; set; }

       // public ICollection<Employee>? Employees { get; set; } //One department can have many employees that's why we use Icollection , it is not editable and it does not have more functionalities as list.
    }
}
