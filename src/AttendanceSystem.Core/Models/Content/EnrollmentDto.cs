using AttendanceSystem.Core.Domain.Content;
using AutoMapper;

namespace AttendanceSystem.Core.Models.Content
{
    public class EnrollmentDto
    {
        public Guid StudentId { get; set; }
        public Guid ClassId { get; set; }

        public class AutoMapperProfiles : Profile
        {
            public AutoMapperProfiles()
            {
                CreateMap<Enrollment, EnrollmentDto>();
            }
        }
    }
}
