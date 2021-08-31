using AutoMapper;
using ContosoUniversity.Models;

namespace ContosoUniversity.AutoMapper
{
    public class EnrollmentProfile : Profile
    {
        public EnrollmentProfile()
        {
            CreateMap<Enrollment, EnrollmentModel>()
                .ForMember(m => m.EnrollmentIdentifier, opt => opt.MapFrom(e => e.EnrollmentId))
            ;
        }
    }
}