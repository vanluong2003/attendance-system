using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AttendanceSystem.Core.Domain.Content 
{
    [Table("Devices")]
   // [Index(nameof(Location), IsUnique =true)]
    public class Device
    {
        [Key]
        public Guid Id { get; set; }
        [MaxLength(50)]
        [Column(TypeName ="varchar(50)")]
        public required string Location { get; set; }
        public required DeviceStatus Status { get; set; }
        public DateTime? LastSeen { get; set; }

        public enum DeviceStatus
        {
            Inactive = 0,
            Active = 1
        }
    }
}
