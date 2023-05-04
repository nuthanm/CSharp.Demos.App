using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudentEnrollment.API.DTOs;
using StudentEnrollment.Data;

namespace StudentEnrollment.API
{
    public static class CourseEndpoints
    {
        public static void MapCourseEndpoints(this IEndpointRouteBuilder app)
        {
            // Approach 1: With Attribute
            // app.MapGet("/courses", async ([FromServices] StudentEnrollmentDbContext db) => { });

            // Approach 2: Without Attributes like [FromServices]
            //app.MapGet("/courses", async (StudentEnrollmentDbContext db) =>
            //{

            //    // Approach 1
            //    // return db.Courses.ToList();

            //    // Approach 2
            //    var result = await db.Courses.ToListAsync();
            //    return Results.Ok(result);

            // Traditional way using DTO without Automapper
            //app.MapGet("/courses", async (StudentEnrollmentDbContext db) =>
            //{
            //    var dataDto = new List<CourseDto>();
            //    var courses = await db.Courses.ToListAsync();
            //    foreach (var course in courses)
            //    {
            //        dataDto.Add(new CourseDto
            //        {
            //            Title = course.Title,
            //            Credits = course.Credits,
            //            Id = course.Id,
            //        });
            //    }
            //    return Results.Ok(dataDto);
            //}).WithTags(nameof(Course))
            //  .WithName("GetCourses")
            //  .Produces<List<CourseDto>>(StatusCodes.Status200OK);

            // Using Automapper
            app.MapGet("/courses", async (StudentEnrollmentDbContext db, IMapper mapper) =>
            {
                var courses = await db.Courses.ToListAsync();
                return mapper.Map<List<CourseDto>>(courses);
            }).WithTags(nameof(Course))
              .WithName("GetCourses")
              .Produces<List<CourseDto>>(StatusCodes.Status200OK);


            // Get single course details
            app.MapGet("/courses/{id}", async (StudentEnrollmentDbContext db, int id, IMapper mapper) =>
            {

                // Find whether the requested course Id is available in table
                // Approach 1:
                // var isCourseExists = await db.Courses.AnyAsync(c => c.Id == id);
                //if (!isCourseExists)
                //    return Results.NotFound();

                //var course = await db.Courses.Where(c => c.Id == id).FirstOrDefaultAsync();
                //return Results.Ok(course);

                //// Approach 2:
                //var isCourseExists = await db.Courses.FindAsync(id);
                //if (isCourseExists == null)
                //    return Results.NotFound();

                // Approach 3 in single line
                return await db.Courses.FindAsync(id) is Course course ? Results.Ok(mapper.Map<CourseDto>(course)) : Results.NotFound();
            }).WithTags(nameof(Course))
              .WithName("GetCourseById")
              .Produces<CourseDto>(StatusCodes.Status200OK)
              .Produces<CourseDto>(StatusCodes.Status404NotFound);

            // Add a new course data
            app.MapPost("/course", async (StudentEnrollmentDbContext db, CourseDto courseDto, IMapper mapper) =>
            {
                if (courseDto.Title is null)
                    return Results.BadRequest();

                var course = mapper.Map<Course>(courseDto);
                await db.Courses.AddAsync(course);
                await db.SaveChangesAsync();
                return Results.Created($"/courses/{course.Id}", courseDto);
            }).WithTags(nameof(Course))
              .WithName("AddNewCourse")
              .Produces<CourseDto>(StatusCodes.Status400BadRequest)
              .Produces<CourseDto>(StatusCodes.Status201Created);

            app.MapPost("/courseDto", async (StudentEnrollmentDbContext db, CourseDto courseDto, IMapper mapper) =>
            {
                if (courseDto.Title is null)
                    return Results.BadRequest();

                var course = mapper.Map<Course>(courseDto);

                await db.Courses.AddAsync(course);
                await db.SaveChangesAsync();

                courseDto = mapper.Map<CourseDto>(course);
                return Results.Created($"/courses/{courseDto.Id}", courseDto);
            }).WithTags(nameof(Course))
              .WithName("AddNewCourseDto")
              .Produces<CourseDto>(StatusCodes.Status400BadRequest)
              .Produces<CourseDto>(StatusCodes.Status201Created);


            // Add a new course data
            app.MapPut("/course/{id}", async (StudentEnrollmentDbContext db, CourseDto courseDto, int id, IMapper mapper) =>
            {
                if (courseDto.Title is null || id <= 0)
                    return Results.BadRequest();

                var existingCourse = await db.Courses.Where(c => c.Id == id).FirstOrDefaultAsync();
                if (existingCourse is null)
                {
                    return Results.NotFound();
                }


                existingCourse.Title = courseDto.Title;

                db.Courses.Update(existingCourse);
                await db.SaveChangesAsync();
                courseDto = mapper.Map<CourseDto>(existingCourse);

                return Results.Created($"/courses/{courseDto.Id}", courseDto);
            }).WithTags(nameof(Course))
            .WithName("UpdateCourse")
              .Produces<CourseDto>(StatusCodes.Status400BadRequest)
              .Produces<CourseDto>(StatusCodes.Status404NotFound)
              .Produces<CourseDto>(StatusCodes.Status201Created);
            ;



            // Delete the course
            app.MapDelete("/course/{id}", async (StudentEnrollmentDbContext db, int id, IMapper mapper) =>
            {
                var course = await db.Courses.FindAsync(id);

                if (course == null)
                    return Results.NotFound();

                db.Courses.Remove(course);
                await db.SaveChangesAsync();
                return Results.NoContent();
            }).WithTags(nameof(Course))
              .WithName("DeleteCourse")
              .Produces<CourseDto>(StatusCodes.Status404NotFound)
              .Produces<CourseDto>(StatusCodes.Status204NoContent);
        }
    }
}
