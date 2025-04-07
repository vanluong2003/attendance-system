using AttendanceSystem.Core.Domain.Content;
using AutoMapper;
using static AttendanceSystem.Core.Domain.Content.Device;

namespace AttendanceSystem.Core.Models.Content
{
    public class CreateUpdateDeviceRequest
    {
        public required string Location { get; set; }
        public required DeviceStatus Status { get; set; }
        //public enum DeviceStatus
        //{
        //    Inactive = 0,
        //    Active = 1
        //}
        public class AutoMapperProfiles : Profile
        {
            public AutoMapperProfiles()
            {
                CreateMap<CreateUpdateDeviceRequest, Device>();
            }
        }
    }
}
