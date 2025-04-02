using AttendanceSystem.Core.Repositories;
using AttendanceSystem.Data.SeedWorks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using AttendanceSystem.Core.Domain.Identity;

namespace AttendanceSystem.Data.Repositories
{
    public class UserRepository : RepositoryBase<AppUser, Guid>, IUserRepository
    {
        private readonly IMapper _mapper;
        public UserRepository(AttendanceSystemContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public Task<AppUser> GetUserByUIDAsync(string UID)
        {
            return _context.Users.Where(x => x.UID == UID).FirstOrDefaultAsync();
        }
    }
}
