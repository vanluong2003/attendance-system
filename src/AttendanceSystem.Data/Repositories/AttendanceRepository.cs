using AttendanceSystem.Core.Domain.Content;
using AttendanceSystem.Core.Repositories;
using AttendanceSystem.Data.SeedWorks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using static AttendanceSystem.Core.SeedWorks.Constants.Permissions;

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

        public Task GetAttendanceByStudentAndClassSchedule(Guid studentId, Guid scheduleId)
        {
            return _context.Attendances.Where(x => x.StudentId == studentId && x.ScheduleId == scheduleId).FirstOrDefaultAsync();
        }
    }
}
