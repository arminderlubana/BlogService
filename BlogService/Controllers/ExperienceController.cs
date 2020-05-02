using BlogService.Data;
using BlogService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace BlogService.Controllers
{
    [Produces("application/json")]
    [Route("api/Experience")]
    public class ExperienceController : Controller
    {
        private readonly EmployeeContext _context;
        // GET: api/Skills

        public ExperienceController(EmployeeContext context)
        {
            _context = context;
        }

        // GET: api/Experience
        [HttpGet]
        public IEnumerable<Experience> Get()
        {
            return _context.Experiences;
        }

        // GET: api/Experience/5
        [HttpGet("{id}", Name = "GetExperience")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            //    // throw new CustomeException("Custom Exception");

            if (!ModelState.IsValid)
            {
                return BadRequest("This is bad requst");
            }

            var exp = await _context.Experiences.SingleOrDefaultAsync(m => m.Id == id);

            if (exp == null)
            {
                return NotFound();
            }

            return Ok(exp);
        }

            // POST: api/Experience
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Experience exp)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Experiences.Add(exp);
            try
            {
                await _context.SaveChangesAsync();

                return CreatedAtAction("Get", new { id = exp.Id }, exp);
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        // PUT: api/Experience/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] Experience exp)
        {
            // throw new NotImplementedException();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Experience emp;

            if (id != exp.Id)
            {
                return BadRequest();
            }

            _context.Entry(exp).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                emp = await _context.Experiences.SingleOrDefaultAsync(m => m.Id == id);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExpExists(id))
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
        private bool ExpExists(int id)
        {
            return _context.Experiences.Any(e => e.Id == id);
        }
    }
}
