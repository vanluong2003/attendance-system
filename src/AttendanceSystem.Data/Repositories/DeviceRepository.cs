using AttendanceSystem.Core.Domain.Content;
using AttendanceSystem.Core.Models;
using AttendanceSystem.Core.Models.Content;
using AttendanceSystem.Core.Repositories;
using AttendanceSystem.Data.SeedWorks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using static AttendanceSystem.Core.SeedWorks.Constants.Permissions;

namespace AttendanceSystem.Data.Repositories
{
    public class DeviceRepository :RepositoryBase<Device, Guid>, IDeviceRepository
    {
        private readonly IMapper _mapper;
        public DeviceRepository(AttendanceSystemContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }
        public Task<Device> GetByLocation(string location)
        {
            return _context.Devices.Where(x => x.Location == location).FirstOrDefaultAsync();
        }
        public async Task<PageResult<DeviceDto>> GetAllPaging(string? keyword, int pageIndex = 1, int pageSize = 10)
        {
            var query = _context.Devices.AsQueryable();
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                query = query.Where(x => x.Location.Contains(keyword));
            }
            var totalRow = await query.CountAsync();

            query = query.OrderByDescending(x => x.LastSeen)
               .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize);

            return new PageResult<DeviceDto>
            {
                Results = await _mapper.ProjectTo<DeviceDto>(query).ToListAsync(),
                CurrentPage = pageIndex,
                RowCount = totalRow,
                PageSize = pageSize
            };
        }
    }
}
