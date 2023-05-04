using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<CMSDatabaseContext>(
    options => options.UseInMemoryDatabase("CmsDatabase"));

// Configure Automapper
builder.Services.AddAutoMapper(typeof(CMSAutoMapper));

var app = builder.Build();

// Configure the HTTP request pipeline.

// app.MapGet("/", () => "Hello Nani!");

#region Controllers

// Get all the courses - Synchronous 
app.MapGet("/courses", (CMSDatabaseContext db) =>
{
    // Approach 1:
    // return db.Courses.ToList();

    // Approach 2:
    var result = db.Courses.ToList();
    return Results.Ok(result);
});

// Get all the courses - AsyncSynchronous 
app.MapGet("/coursesAsync", async (CMSDatabaseContext db) =>
{
    // Approach 1:
    // return await db.Courses.ToListAsync();

    // Approach 2:
    var result = await db.Courses.ToListAsync();
    return Results.Ok(result);
});

// Create a new course - Without Automapper
app.MapPost("/courses", async (Course course, CMSDatabaseContext db) =>
    {
        db.Courses.Add(course);
        await db.SaveChangesAsync();
        return Results.Created($"/courses/{course.CourseId}", course);
    });


// Create a new course - With Automapper
// app.MapPost("/coursesDto", async ([FromBody] CourseDto courseDto, [FromServices] CMSDatabaseContext db, [FromServices] IMapper mapper) =>
app.MapPost("/coursesDto", async (CourseDto courseDto, CMSDatabaseContext db, IMapper mapper) =>
{
    // convert dto to data model
    var newCourse = mapper.Map<Course>(courseDto);

    db.Courses.Add(newCourse);
    await db.SaveChangesAsync();

    // convert data model to dto
    var result = mapper.Map<CourseDto>(newCourse);
    return Results.Created($"/courses/{result.CourseId}", result);
});


// Get single course
app.MapGet("/courses/{courseId}", async (int courseId, CMSDatabaseContext db) =>
{
    var result = await db.Courses.Where(c => c.CourseId == courseId).FirstOrDefaultAsync();
    if(result == null)
        return Results.NotFound();

    return Results.Ok(result);
});


// Get single course
app.MapGet("/coursesDto/{courseId}", async (int courseId, CMSDatabaseContext db, IMapper mapper) =>
{
    var result = await db.Courses.Where(c => c.CourseId == courseId).FirstOrDefaultAsync();
    // var result = await db.Courses.FindAsync(courseId);
    if (result == null)
        return Results.NotFound();

    var resultDto = mapper.Map<CourseDto>(result);

    return Results.Ok(resultDto);
});

// Update the course item
app.MapPut("/courseDto/{courseId}", async (int courseId, CourseDto courseDto, CMSDatabaseContext db, IMapper mapper) =>
{
    try
    {
        var course = await db.Courses.FindAsync(courseId);
        if (course == null)
            return Results.NotFound();

        if(courseDto == null)
            return Results.BadRequest();

        course.CourseName = courseDto.CourseName;
        course.CourseType = (int)courseDto.CourseType;

        db.Update(course);
        await db.SaveChangesAsync();

        var resultDto = mapper.Map<CourseDto>(course);
        return Results.Ok(resultDto);
       
    }
    catch(Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});

// Delete the course item
app.MapDelete("/courseDto/{courseId}", async (int courseId, CMSDatabaseContext db, IMapper mapper) =>
{
    try
    {
        var course = await db.Courses.FindAsync(courseId);
        if (course == null)
            return Results.NotFound();

        db.Courses.Remove(course);
        await db.SaveChangesAsync();

        var resultDto = mapper.Map<CourseDto>(course);
        return Results.Ok(resultDto);
    }
    catch(Exception ex)
    {
        return Results.Problem(ex.Message);

    }
});

#endregion Controllers
app.Run();


#region DataModels
public class Course
{
    public int CourseId { get; set; }
    public string CourseName { get; set; } = string.Empty;

    public int CourseDuration { get; set; }

    public int CourseType { get; set; }

}

public class CourseDto
{
    public int CourseId { get; set; }
    public string CourseName { get; set; } = string.Empty;

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public COURSE_TYPE CourseType { get; set; }

}

public enum COURSE_TYPE
{
    ENGINEERING = 0,
    MEDICAL,
    CIVIL,
}
#endregion DataModels


#region DatabaseContext
public class CMSDatabaseContext : DbContext
{
    public CMSDatabaseContext(DbContextOptions options): base(options)
    {

    }

    // public DbSet<Course> Courses { get; set; }
    public DbSet<Course> Courses => Set<Course>();

}
#endregion DatabaseContext

#region Automapper
public class CMSAutoMapper : Profile
{
    public CMSAutoMapper()
    {
        // Approach 1
        CreateMap<Course, CourseDto>()
            .ReverseMap();

        // Approach 2
        // CreateMap<Course, CourseDto>();
        // CreateMap<CourseDto, Course>()

    }
}
#endregion Automapper