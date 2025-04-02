using AttendanceSystem.Core.Domain.Content;
using AttendanceSystem.Core.Repositories;
using AttendanceSystem.Data.SeedWorks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AttendanceSystem.Data.Repositories
{
    public class ClassScheduleRepository : RepositoryBase<ClassSchedule, Guid>, IClassScheduleRepository
    {
        private readonly IMapper _mapper;
        public ClassScheduleRepository(AttendanceSystemContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }
        public Task<ClassSchedule> GetScheduleByDateAndLocation(string location, DateTime date)
        {
            return _context.ClassSchedules.Where(x => x.Location == location &&
                                          x.StartTime <= date && date <= x.EndTime)
                .FirstOrDefaultAsync();
        }
    }
}
