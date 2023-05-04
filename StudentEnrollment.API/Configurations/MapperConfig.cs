using AutoMapper;
using StudentEnrollment.API.DTOs;
using StudentEnrollment.Data;

namespace StudentEnrollment.API.Configurations
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<Course, CourseDto>().ReverseMap();
            // CreateMap<Student, StundentDto>().ReverseMap();
            // CreateMap<Enrollment, EnrollmentDto>().ReverseMap();
        }
    }
}
