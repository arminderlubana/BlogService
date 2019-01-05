using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlogService.Data;
using BlogService.Models;
using System.Net.Http;
using System.Net;
using Microsoft.AspNetCore.WebUtilities;
namespace BlogService.Controllers
{
    [Produces("application/json")]
    [Route("api/Students")]
    public class StudentsController : Controller
    {
        private readonly SchoolContext _context;

        public StudentsController(SchoolContext context)
        {
            _context = context;
        }

        // GET: api/Students
        //[Route("{page:int}/{pageSize:int}/{orderBy:alpha?}")]
        //public IEnumerable<Student> GetStudents(int page =1,int pageSize = 10, string orderBy = nameof(Student.ID))//, bool ascending = true)
        //{
        //    Console.Write("page" + page);
        //    Console.Write("pageSize" + pageSize);
        //    Console.Write("orderBy" + orderBy);
        //   // Console.Write("ascending" + ascending);
        //    var skipAmount = pageSize * (page - 1);
        //    return _context.Students.Skip(skipAmount)
        //        .Take(pageSize);
        //   // return _context.Students;


        //}

        // GET: api/Students
        [Route("{page:int}/{pageSize:int}/{isAsc:bool?}/{orderBy:alpha?}")]
        public async Task<IActionResult> GetStudents(int page = 1, int pageSize = 10,Boolean isAsc=true, string orderBy = nameof(Student.ID))//, bool ascending = true)
        {
           // throw new NotImplementedException();    
            //Console.Write("page" + page);
            //Console.Write("pageSize" + pageSize);
            //Console.Write("orderBy" + orderBy);
            // Console.Write("ascending" + ascending);
            var totalRecord = _context.Students.Count();
            var totalPages = totalRecord / pageSize;
            var skipAmount = pageSize * (page - 1);

            IQueryable<Student> studentQuery = _context.Students;
            var propertyInfo = typeof(Student).GetProperty(orderBy);
            if (propertyInfo != null)
            {
                // items.OrderBy(x => propertyInfo.GetValue(x, null));
                //var orderByExpression =  System.Linq.Expressions.Expression<Student>(orderBy);
                if (isAsc)
                    studentQuery = studentQuery.OrderBy(x => propertyInfo.GetValue(x, null));
                else
                    studentQuery = studentQuery.OrderByDescending(x => propertyInfo.GetValue(x, null));
            }
            else
            {
                if (isAsc)
                    studentQuery = studentQuery.OrderBy(c => c.ID);
                else
                    studentQuery = studentQuery.OrderByDescending(c => c.ID);
            }

            var result = studentQuery.Skip(skipAmount)
                .Take(pageSize);
         
            pagingResult a = new pagingResult() { Results = result, PageNumber = page, PageSize = pageSize, TotalNumberOfPages = totalPages, TotalNumberOfRecords = totalRecord };
        
              return new ObjectResult(a);
           


        }


        // GET: api/Students/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudent([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var student = await _context.Students.SingleOrDefaultAsync(m => m.ID == id);

            if (student == null)
            {
                return NotFound();
            }

            return Ok(student);
        }

        // PUT: api/Students/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudent([FromRoute] int id, [FromBody] Student student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != student.ID)
            {
                return BadRequest();
            }

            _context.Entry(student).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Students
        [HttpPost]
        public async Task<IActionResult> PostStudent([FromBody] Student student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStudent", new { id = student.ID }, student);
        }

        // DELETE: api/Students/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var student = await _context.Students.SingleOrDefaultAsync(m => m.ID == id);
            if (student == null)
            {
                return NotFound();
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return Ok(student);
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.ID == id);
        }

    //    public async Task<IActionResult> Get(int? page = null,
    //int pageSize = 10, string orderBy = nameof(Employee.Id), bool ascending = true)
    //    {
    //        //if (page == null)
    //        //    return Ok(await EntityContext.Employees.ToListAsync());

    //        //var employees = await CreatePagedResults<Employee, EmployeeModel>
    //        //    (EntityContext.Employees, page.Value, pageSize, orderBy, ascending);
    //        //return Ok(employees);
    //    }

        //protected async Task<PagedResults<TReturn>> CreatePagedResults<T, TReturn>(
        //IQueryable<T> queryable,
        //int page,
        //int pageSize,
        //string orderBy,
        //bool ascending)
        //{
        //    var skipAmount = pageSize * (page - 1);

        //    var projection = queryable
        //        .OrderByPropertyOrField(orderBy, ascending)
        //        .Skip(skipAmount)
        //        .Take(pageSize).ProjectTo<TReturn>();

        //    var totalNumberOfRecords = await queryable.CountAsync();
        //    var results = await projection.ToListAsync();

        //    var mod = totalNumberOfRecords % pageSize;
        //    var totalPageCount = (totalNumberOfRecords / pageSize) + (mod == 0 ? 0 : 1);

        //    var nextPageUrl =
        //    page == totalPageCount
        //        ? null
        //        : Url?.Link("DefaultApi", new
        //        {
        //            page = page + 1,
        //            pageSize,
        //            orderBy,
        //            ascending
        //        });

        //    return new PagedResults<TReturn>
        //    {
        //        Results = results,
        //        PageNumber = page,
        //        PageSize = results.Count,
        //        TotalNumberOfPages = totalPageCount,
        //        TotalNumberOfRecords = totalNumberOfRecords,
        //        NextPageUrl = nextPageUrl
        //    };
        //}
    }

    public static class Extensions
    {
        /// <summary>
        /// Order the IQueryable by the given property or field.
        /// </summary>

        /// <typeparam name="T">The type of the IQueryable being ordered.</typeparam>
        /// <param name="queryable">The IQueryable being ordered.</param>
        /// <param name="propertyOrFieldName">
        /// The name of the property or field to order by.</param>
        /// <param name="ascending">Indicates whether or not 
        /// the order should be ascending (true) or descending (false.)</param>
        /// <returns>Returns an IQueryable ordered by the specified field.</returns>
        //public static IQueryable<T> OrderByPropertyOrField<T>
        //(this IQueryable<T> queryable, string propertyOrFieldName, bool ascending = true)
        //{
        //    var elementType = typeof(T);
        //    var orderByMethodName = ascending ? "OrderBy" : "OrderByDescending";

        //    var parameterExpression = Expression.Parameter(elementType);
        //    var propertyOrFieldExpression =
        //        Expression.PropertyOrField(parameterExpression, propertyOrFieldName);
        //    var selector = Expression.Lambda(propertyOrFieldExpression, parameterExpression);

        //    var orderByExpression = Expression.Call(typeof(Queryable), orderByMethodName,
        //        new[] { elementType, propertyOrFieldExpression.Type }, queryable.Expression, selector);

        //    return queryable.Provider.CreateQuery<T>(orderByExpression);
        //}
    }
    public class PagedResults<T>
    {
        /// <summary>
        /// The page number this page represents.
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        /// The size of this page.
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// The total number of pages available.
        /// </summary>
        public int TotalNumberOfPages { get; set; }

        /// <summary>
        /// The total number of records available.
        /// </summary>
        public int TotalNumberOfRecords { get; set; }

        /// <summary>
        /// The URL to the next page - if null, there are no more pages.
        /// </summary>
        public string NextPageUrl { get; set; }

        /// <summary>
        /// The records this page represents.
        /// </summary>
        public IEnumerable<T> Results { get; set; }
    }

    public class pagingResult //: IActionResult
    {
        //private HttpStatusCode _statusCode;
        private string _message;

        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        /// The size of this page.
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// The total number of pages available.
        /// </summary>
        public int TotalNumberOfPages { get; set; }

        /// <summary>
        /// The total number of records available.
        /// </summary>
        public int TotalNumberOfRecords { get; set; }

        /// <summary>
        /// The URL to the next page - if null, there are no more pages.
        /// </summary>
       // public string NextPageUrl { get; set; }

        /// <summary>
        /// The records this page represents.
        /// </summary>
        public IEnumerable<Student> Results { get; set; }

        //public pagingResult(HttpStatusCode statusCode, int pageNumber, int pageSize, int totalPNumber, int totalRecords, IEnumerable<Student> students)
        //{
        //    _statusCode = statusCode;
        //    Results = students;
        //    PageNumber = pageNumber;
        //    pageSize = PageSize;
        //    TotalNumberOfPages = totalPNumber;
        //    TotalNumberOfRecords = totalRecords;
        //}

        //public Task ExecuteResultAsync(ActionContext context)
        //{
        //    HttpResponseMessage response = new HttpResponseMessage(_statusCode)
        //    {
        //        Content = new StringContent(_message)
        //    };
        //    return Task.FromResult(response);
        //}
    }
}
