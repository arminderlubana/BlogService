using BlogService.Data;
using BlogService.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogService.Controllers
{
    [Produces("application/json")]
    [Route("api/Employees")]
    //[EnableCors("AllowSpecificOrigin")]
    public class EmployeesController : Controller
    {
        private readonly EmployeeContext _context;

        public EmployeesController(EmployeeContext context)
        {
            _context = context;
            
        }

        // GET: api/Employees
        [HttpGet]
        public IEnumerable<Employee> GetEmployees()
        {
            return _context.Employees;
        }

        // GET: api/Employees/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployee([FromRoute] int id)
        {
             // throw new CustomeException("Custom Exception");
            
            if (!ModelState.IsValid)
            {
                return BadRequest("This is bad requst");
            }

            var employee = await _context.Employees.SingleOrDefaultAsync(m => m.EmployeeId == id);
            employee.Skills = _context.Skills.Where(m => m.EmployeeId == id).ToList();
            employee.Experiences = _context.Experiences.Where(m => m.EmployeeId == id).ToList();

            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        // PUT: api/Employees/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee([FromRoute] int id, [FromBody] Employee employee)
        {
           // throw new NotImplementedException();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Employee emp;

            if (id != employee.EmployeeId)
            {
                return BadRequest();
            }

            _context.Entry(employee).State = EntityState.Modified;
            _context.Skills.RemoveRange(_context.Skills.Where(s => s.EmployeeId == employee.EmployeeId));
            _context.Experiences.RemoveRange(_context.Experiences.Where(s => s.EmployeeId == employee.EmployeeId));
            //var skills = new List<Skills>();
            //var experience = new List<Experience>();




            try
            {
                await _context.SaveChangesAsync();
                if (employee.Skills != null && employee.Skills.Any())
                {
                    employee.Skills.ToList().ForEach(s => s.Id = 0);
                    _context.Skills.AddRange(employee.Skills);
                    await _context.SaveChangesAsync();
                }
                if (employee.Experiences != null && employee.Experiences.Any())
                {
                    employee.Experiences.ToList().ForEach(s => s.Id = 0);
                    _context.Experiences.AddRange(employee.Experiences);
                    await _context.SaveChangesAsync();
                    
                }
                emp = await _context.Employees.SingleOrDefaultAsync(m => m.EmployeeId == id);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(emp);
        }

        // POST: api/Employees
        [HttpPost]
        public async Task<IActionResult> PostEmployee([FromBody] Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var skills= new List<Skills>();
            var experience= new List<Experience>();
            if(employee.Skills!=null)
                 skills = employee.Skills.ToList();
            if(employee.Experiences != null)
             experience = employee.Experiences.ToList();
            employee.Skills = null;
            employee.Experiences = null;
            _context.Employees.Add(employee);
            try
            {
                await _context.SaveChangesAsync();
                skills.ForEach(s => s.EmployeeId = employee.EmployeeId);
                _context.Skills.AddRange(skills);
                experience.ForEach(e => e.EmployeeId = employee.EmployeeId);
                _context.Experiences.AddRange(experience);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetEmployee", new { id = employee.EmployeeId }, employee);
            }
            catch (System.Exception ex)
            {

                throw;
            }
        }

        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var employee = await _context.Employees.SingleOrDefaultAsync(m => m.EmployeeId == id);
            if (employee == null)
            {
                return NotFound();
            }

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return Accepted(employee);
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.EmployeeId == id);
        }
    }
}
