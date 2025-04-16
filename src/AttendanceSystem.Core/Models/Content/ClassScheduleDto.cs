using AttendanceSystem.Core.Domain.Content;
using AutoMapper;

namespace AttendanceSystem.Core.Models.Content
{
    public class ClassScheduleDto
    {
        public Guid Id { get; set; }
        public required Guid ClassId { get; set; }
        public required string Location { get; set; }
        public required DateTime StartTime { get; set; }
        public required DateTime EndTime { get; set; }

        public class AutoMapperProfiles : Profile
        {
            public AutoMapperProfiles()
            {
                CreateMap<ClassSchedule, ClassScheduleDto>();
            }
        }
    }
}
