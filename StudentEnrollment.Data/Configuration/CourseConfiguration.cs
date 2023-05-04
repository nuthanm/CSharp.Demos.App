﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentEnrollment.Data.Configuration
{
    internal class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasData(
               new Course()
               {
                   Id = 1,
                   Title = "Basic Minimal API Development",
               },
               new Course()
               {
                   Id = 2,
                   Title = "Advanced API Development"
               }
           );
        }
    }
}
