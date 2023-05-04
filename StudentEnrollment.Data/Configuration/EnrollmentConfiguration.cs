using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentEnrollment.Data.Configuration
{
    internal class EnrollmentConfiguration : IEntityTypeConfiguration<Enrollment>
    {
        public void Configure(EntityTypeBuilder<Enrollment> builder)
        {
            builder.HasData(
               new Enrollment()
               {
                   Id = 1,
                   StudentId = 1,
                   CourseId = 1,
               },
               new Enrollment()
               {
                   Id = 2,
                   StudentId = 2,
                   CourseId = 1,
               }
           );
        }
    }
}
