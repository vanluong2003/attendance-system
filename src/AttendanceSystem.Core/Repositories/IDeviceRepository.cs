using AttendanceSystem.Core.Domain.Content;
using AttendanceSystem.Core.Models.Content;
using AttendanceSystem.Core.Models;
using AttendanceSystem.Core.SeedWorks;

namespace AttendanceSystem.Core.Repositories
{
    public interface IDeviceRepository : IRepository<Device, Guid>
    {
        Task<Device> GetByLocation(string location);
        Task<PageResult<DeviceDto>> GetAllPaging(string? keyword, int pageIndex = 1, int pageSize = 10);
    }
}
