using AttendanceSystem.Core.Domain.Content;
using AutoMapper;

namespace AttendanceSystem.Core.Models.Content
{
    public class CreateUpdateClassRequest
    {
        public required string CourseName { get; set; }
        public required string CourseCode { get; set; }
        public required Guid LecturerID { get; set; }

        public class AutoMapperProfiles : Profile
        {
            public AutoMapperProfiles()
            {
                CreateMap<CreateUpdateClassRequest, Class>();
            }
        }
    }
}
