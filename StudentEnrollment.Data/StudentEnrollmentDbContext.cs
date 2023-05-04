using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using StudentEnrollment.Data.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentEnrollment.Data
{
    public class StudentEnrollmentDbContext : IdentityDbContext
    {
        public StudentEnrollmentDbContext(DbContextOptions<StudentEnrollmentDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Approach 1: If there is too many entities to add some default data when there is a migration
            // This approach is not good
            //builder.Entity<Course>().HasData(
            //    new Course()
            //    {
            //        Id = 1,
            //        Title = "Basic Minimal API Development",
            //    },
            //    new Course()
            //    {
            //        Id = 2,
            //        Title = "Advanced API Development"
            //    }
            //);
            //builder.Entity<Student>().HasData(
            //    new Student()
            //    {
            //        Id = 1,
            //        FirstName ="Nani",
            //        LastName ="M"
            //    },
            //    new Student()
            //    {
            //        Id = 2,
            //        FirstName = "Potti",
            //        LastName="V"
            //    }
            //);

            //builder.Entity<Enrollment>().HasData(
            //    new Enrollment()
            //    {
            //        Id = 1,
            //        StudentId = 1,
            //        CourseId = 1,
            //    },
            //    new Enrollment()
            //    {
            //        Id = 2,
            //        StudentId = 2,
            //        CourseId = 1,
            //    }
            //);

            // Approach 2: Use Configuration (IEntityTypeConfiguration)
            builder.ApplyConfiguration(new CourseConfiguration());
            builder.ApplyConfiguration(new StudentConfiguration());
            builder.ApplyConfiguration(new EnrollmentConfiguration());

        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
    }

    public class StudentEnrollmentDbContextFactory : IDesignTimeDbContextFactory<StudentEnrollmentDbContext>
    {
        public StudentEnrollmentDbContext CreateDbContext(string[] args)
        {
            // Get Environment and this value is from launchSettings.json file
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            // Build Config Object
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            // Get connection string
            var optionsBuilder = new DbContextOptionsBuilder<StudentEnrollmentDbContext>();
            var connectionString = config.GetConnectionString("StundentEnrollmentDBConnection");
            optionsBuilder.UseSqlServer(connectionString);
            return new StudentEnrollmentDbContext(optionsBuilder.Options);
        }
    }
}
