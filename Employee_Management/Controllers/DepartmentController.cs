using AutoMapper;
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
    public class DepartmentController : ControllerBase
    {

        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public DepartmentController(DataContext context , IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> GetDepartment()
        {
            var value =  await _context.Departments.ToListAsync();
            return Ok(value);
        }


        [HttpPost]
        public async Task<IActionResult> AddDepartment(DepartmentDto dep)
        {
            if(dep != null) {
                var department = _mapper.Map<Department>(dep);
                _context.Departments.Add(department);
            }
            await _context.SaveChangesAsync();
            return Ok(await _context.Departments.ToListAsync());
        }

        [HttpPatch("{Id}")]
        public async Task<IActionResult> UpdateDepartment(Department updateVal)
        {
            var updateDb = await _context.Departments.FindAsync(updateVal.DepartmentId);
            updateDb.DepartmentName = updateVal.DepartmentName;

            await _context.SaveChangesAsync();
            return Ok(await _context.Departments.ToListAsync());
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteDepartment(int Id)
        {
            var del = await _context.Departments.FindAsync(Id);

            if(del == null)
            {
                return BadRequest("Not Found");
            }
            _context.Departments.Remove(del);
            await _context.SaveChangesAsync();
            return Ok(await _context.Departments.ToListAsync());
        }

    }
}
