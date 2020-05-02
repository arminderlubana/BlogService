using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogService.Data;
using BlogService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogService.Controllers
{
    [Produces("application/json")]
    [Route("api/Skills")]
    public class SkillsController : Controller
    {
        private readonly EmployeeContext _context;
        // GET: api/Skills

        public SkillsController(EmployeeContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IEnumerable<Skills> Get()
        {
            return _context.Skills;
        }

        // GET: api/Skills/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            // throw new CustomeException("Custom Exception");

            if (!ModelState.IsValid)
            {
                return BadRequest("This is bad requst");
            }

            var employee = await _context.Skills.SingleOrDefaultAsync(m => m.Id == id);

            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }
        
        // POST: api/Skills
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Skills skill)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Skills.Add(skill);
            try
            {
                await _context.SaveChangesAsync();

                return CreatedAtAction("Get", new { id = skill.Id }, skill);
            }
            catch (System.Exception)
            {

                throw;
            }
        }
        
        // PUT: api/Skills/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] Skills skill)
        {
            // throw new NotImplementedException();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Skills emp;

            if (id != skill.Id)
            {
                return BadRequest();
            }

            _context.Entry(skill).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                emp = await _context.Skills.SingleOrDefaultAsync(m => m.Id == id);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SkillsExists(id))
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
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        private bool SkillsExists(int id)
        {
            return _context.Skills.Any(e => e.Id == id);
        }
    }
}
