using AutoMapper;
using ContosoUniversity.Models;

namespace ContosoUniversity.AutoMapper
{
    public class StudentProfile : Profile
    {
        public StudentProfile()
        {
            CreateMap<Student, StudentModel>();
        }
    }
}