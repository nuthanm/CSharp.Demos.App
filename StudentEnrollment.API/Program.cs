using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentEnrollment.API;
using StudentEnrollment.API.Configurations;
using StudentEnrollment.Data;

var builder = WebApplication.CreateBuilder(args);

// Get the connection string from configuration file.
var conn = builder.Configuration.GetConnectionString("StundentEnrollmentDBConnection");

builder.Services.AddDbContext<StudentEnrollmentDbContext>(context =>
{
    context.UseSqlServer(conn);
});

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(MapperConfig));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy => policy.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");

#region Controllers

app.MapCourseEndpoints();

#endregion Controllers
app.Run();