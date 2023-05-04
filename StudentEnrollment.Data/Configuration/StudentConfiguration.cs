using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentEnrollment.Data.Configuration
{
    internal class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.HasData(
               new Student()
               {
                   Id = 1,
                   FirstName = "Nani",
                   LastName = "M"
               },
               new Student()
               {
                   Id = 2,
                   FirstName = "Potti",
                   LastName = "V"
               }
           );
        }
    }
}
