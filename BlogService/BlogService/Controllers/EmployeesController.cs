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

            try
            {
                await _context.SaveChangesAsync();
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

            _context.Employees.Add(employee);
            try
            {
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetEmployee", new { id = employee.EmployeeId }, employee);
            }
            catch (System.Exception)
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