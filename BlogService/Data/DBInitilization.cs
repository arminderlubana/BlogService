using BlogService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Linq;

namespace BlogService.Data
{
    public static class DbInitializer
    {
        public static void EnsureMigration<T>(this DbContext context) where T : DbContext
        {
            if (context.Database.EnsureCreated())
            {
                RelationalDatabaseCreator edatabaseCreator = (RelationalDatabaseCreator)context.Database.GetService<IDatabaseCreator>();
                edatabaseCreator.CreateTables();
            }
            //context.Database.Migrate();
        }

        public static void InitializeSchoolContext<T>(this SchoolContext context) where T : DbContext
        {
            #region Student
            // Look for any students.
            //if (context.Students.Any())
            //{
                return;   // DB has been seeded
            //}

            //var students = new Student[]
            //{
            //    new Student { FirstMidName = "Carson",   LastName = "Alexander",
            //        EnrollmentDate = DateTime.Parse("2010-09-01"), Class="10th" },
            //    new Student { FirstMidName = "Meredith", LastName = "Alonso",
            //        EnrollmentDate = DateTime.Parse("2012-09-01") , Class="10th"},
            //    new Student { FirstMidName = "Arturo",   LastName = "Anand",
            //        EnrollmentDate = DateTime.Parse("2013-09-01") , Class="10th"},
            //    new Student { FirstMidName = "Gytis",    LastName = "Barzdukas",
            //        EnrollmentDate = DateTime.Parse("2012-09-01") , Class="10th"},
            //    new Student { FirstMidName = "Yan",      LastName = "Li",
            //        EnrollmentDate = DateTime.Parse("2012-09-01") , Class="10th"},
            //    new Student { FirstMidName = "Peggy",    LastName = "Justice",
            //        EnrollmentDate = DateTime.Parse("2011-09-01"), Class="10th" },
            //    new Student { FirstMidName = "Laura",    LastName = "Norman",
            //        EnrollmentDate = DateTime.Parse("2013-09-01"), Class="10th" },
            //    new Student { FirstMidName = "Nino",     LastName = "Olivetto",
            //        EnrollmentDate = DateTime.Parse("2005-09-01") , Class="10th"}
            //};

            //foreach (Student s in students)
            //{
            //    context.Students.Add(s);
            //}
            //context.SaveChanges();

            #endregion

           



        }

        public static void InitializeEmployeeContext<T>(this EmployeeContext context) where T : DbContext
        {
            #region Student
            //context.Database.
            // Look for any students.
            if (context.Employees.Any())
            {
                return;   // DB has been seeded
            }

            var employees = new Employee[]
            {
                new Employee { FirstName = "Carson",   LastName = "Alexander",
                    DOB = DateTime.Parse("2010-09-01"), email="Carson", Sex="arminder.singh@evry.com" },
                new Employee { FirstName = "Meredith", LastName = "Alonso",
                    DOB = DateTime.Parse("2012-09-01") , email="Meredith", Sex="arminder.singh@evry.com" },
                new Employee { FirstName = "Arturo",   LastName = "Anand",
                    DOB = DateTime.Parse("2013-09-01") , email="Arturo", Sex="arminder.singh@evry.com" },
                new Employee { FirstName = "Gytis",    LastName = "Barzdukas",
                     DOB= DateTime.Parse("2012-09-01") ,  email="Gytis", Sex="arminder.singh@evry.com" },
                new Employee { FirstName = "Yan",      LastName = "Li",
                    DOB = DateTime.Parse("2012-09-01") ,  email="Yan", Sex="arminder.singh@evry.com" },
                new Employee { FirstName = "Peggy",    LastName = "Justice",
                    DOB = DateTime.Parse("2011-09-01"),  email="Peggy", Sex="arminder.singh@evry.com"  },
                new Employee { FirstName = "Laura",    LastName = "Norman",
                    DOB = DateTime.Parse("2013-09-01"),  email="Laura", Sex="arminder.singh@evry.com"  },
                new Employee { FirstName = "Nino",     LastName = "Olivetto",
                    DOB = DateTime.Parse("2005-09-01") ,  email="Nino", Sex="arminder.singh@evry.com" }
            };

            foreach (Employee s in employees)
            {
                context.Employees.Add(s);
            }
            context.SaveChanges();

            #endregion

    




        }
    }
}
