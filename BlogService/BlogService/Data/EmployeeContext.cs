using BlogService.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogService.Data
{
    public class EmployeeContext : DbContext
    {

        public EmployeeContext(DbContextOptions<EmployeeContext> options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Employee>().ToTable("Employee");



        }
    }
}
