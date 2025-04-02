using AttendanceSystem.Core.Domain.Content;
using AttendanceSystem.Core.Repositories;
using AttendanceSystem.Data.SeedWorks;
using AutoMapper;

namespace AttendanceSystem.Data.Repositories
{
    public class AttendanceRepository : RepositoryBase<Attendance, Guid>, IAttendanceRepository
    {
        private readonly IMapper _mapper;
        public AttendanceRepository(AttendanceSystemContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }
        public async Task AddAttendaceAsync(Guid studentId, Guid scheduleId, DateTime? checkInTime, Attendance.AttendanceStatus attendanceStatus)
        {
            var attendance = new Attendance
            {
                StudentId = studentId,
                ScheduleId = scheduleId,
                CheckInTime = checkInTime,
                Status = attendanceStatus
            };

            await _context.Attendances.AddAsync(attendance);
            await _context.SaveChangesAsync();
        }
    }
}
