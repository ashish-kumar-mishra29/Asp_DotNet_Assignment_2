using AutoMapper;
using Employee_Management.AutoMapper;
using Employee_Management.Data;
using Employee_Management.Dto;
using Employee_Management.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Employee_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _profile;

        public EmployeeController(DataContext context , IMapper profile)
        {
            _context = context;
            _profile = profile;
        }

      

        [HttpGet]
        public async Task<IActionResult> GetEmp()
        {
            var query = from emp in _context.Employees
                        join dept in _context.Departments on emp.DepartmentId equals dept.DepartmentId
                        select new
                        {
                            Id = emp.EmpId,
                            name = emp.Name,
                            age=emp.Age,
                            salary = emp.Salary,
                            DepartmentName = dept.DepartmentName
                        };
            var data = await _context.Employees.ToListAsync();
            //var emp =  _profile.Map<List<Employee>>(data);
            return Ok(query);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee(EmployeeDto emp)
        {
           
            var addEmp = _profile.Map<Employee>(emp);
            _context.Employees.Add(addEmp);

            await _context.SaveChangesAsync();
            var query = from empl in _context.Employees
                        join dept in _context.Departments on empl.DepartmentId equals dept.DepartmentId
                        select new
                        {
                            Id = empl.EmpId,
                            name = empl.Name,
                            age = empl.Age,
                            salary = empl.Salary,
                            DepartmentName = dept.DepartmentName
                        };
            return Ok(query);
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateEmployee(EmployeeDto emp)
        {
            var updateEmp= _profile.Map<Employee>(emp);
            updateEmp = await _context.Employees.FindAsync(emp.EmpId);

            if (updateEmp == null)
            {
                return BadRequest("Employee not exist");
            }


            updateEmp.Name = emp.Name;
            updateEmp.Salary = emp.Salary;
            updateEmp.Age = emp.Age;
            updateEmp.DepartmentId = emp.DepartmentId;

            await _context.SaveChangesAsync();
            var query = from empl in _context.Employees
                        join dept in _context.Departments on empl.DepartmentId equals dept.DepartmentId
                        select new
                        {
                            Id = empl.EmpId,
                            name = empl.Name,
                            age = empl.Age,
                            salary = empl.Salary,
                            DepartmentName = dept.DepartmentName
                        };

            return Ok(query);

        }
        

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteDepartment(int Id)
        {
            var del = await _context.Employees.FindAsync(Id);

            if (del == null)
            {
                return BadRequest("Not Found");
            }
            _context.Employees.Remove(del);
            await _context.SaveChangesAsync();
            var query = from empl in _context.Employees
                        join dept in _context.Departments on empl.DepartmentId equals dept.DepartmentId
                        select new
                        {
                            Id = empl.EmpId,
                            name = empl.Name,
                            age = empl.Age,
                            salary = empl.Salary,
                            DepartmentId = dept.DepartmentId,
                            DepartmentName = dept.DepartmentName
                        };
            return Ok(query);
        }
    }
}
