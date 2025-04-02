using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace AttendanceSystem.Core.Domain.Identity
{
    public class AppUser : IdentityUser<Guid>
    {
        [MaxLength(100)]
        public required string FirstName { get; set; }
        [MaxLength(100)]
        public required string LastName { get; set; }
        [MaxLength(100)]
        public required string UID { get; set; }
        public bool IsActive { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime ModifiedDate { get; set; }
        [MaxLength(500)]
        public string? Avatar { get; set; }
        public DateTime? LastLoginDate { get; set; }
    }
}
