using System.ComponentModel.DataAnnotations;
using AttendanceSystem.Core.Domain.Content;
using AutoMapper;

namespace AttendanceSystem.Core.Models.Content
{
    public class ClassInListDto
    {
        [MaxLength(100)]
        public required string CourseName { get; set; }
        [MaxLength(50)]
        public required string CourseCode { get; set; }
        public required Guid LecturerID { get; set; }
        public class AutoMapperProfiles : Profile
        {
            public AutoMapperProfiles()
            {
                CreateMap<Class, ClassInListDto>();
            }
        }
    }
}
