using AttendanceSystem.Core.Domain.Content;
using AttendanceSystem.Core.Domain.Identity;
using AttendanceSystem.Core.SeedWorks;

namespace AttendanceSystem.Core.Repositories
{
    public interface IUserRepository : IRepository<AppUser, Guid>
    {
        Task<AppUser> GetUserByUIDAsync(string UID);
    }
}
