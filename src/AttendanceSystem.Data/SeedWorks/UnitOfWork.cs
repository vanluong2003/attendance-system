using AttendanceSystem.Core.Repositories;
using AttendanceSystem.Core.SeedWorks;
using AttendanceSystem.Data.Repositories;
using AutoMapper;

namespace AttendanceSystem.Data.SeedWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AttendanceSystemContext _context;
        public UnitOfWork(AttendanceSystemContext context, IMapper mapper)
        {
            _context = context;
            Classes = new ClassRepository(context, mapper);
            Devices = new DeviceRepository(context, mapper);
            Attendances = new AttendanceRepository(context, mapper);
            ClassSchedules = new ClassScheduleRepository(context, mapper);
            Enrollments = new EnrollmentRepository(context, mapper);
            Users = new UserRepository(context, mapper);
        }
        public IClassRepository Classes { get; private set; }
        public IDeviceRepository Devices { get; private set; }
        public IAttendanceRepository Attendances { get; private set; }
        public IClassScheduleRepository ClassSchedules { get; private set; }
        public IEnrollmentRepository Enrollments { get; private set; }
        public IUserRepository Users { get; private set; }
        public async Task<int> CompeleAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
