using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace AttendanceSystem.Core.Domain.Identity
{
    public class AppRole : IdentityRole<Guid>
    {
        [Required]
        [MaxLength(200)]
        public required string DisplayName { get; set; }
    }
}
