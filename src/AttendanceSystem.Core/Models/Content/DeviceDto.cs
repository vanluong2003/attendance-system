using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AttendanceSystem.Core.Domain.Content;
using AutoMapper;
using static AttendanceSystem.Core.Domain.Content.Device;

namespace AttendanceSystem.Core.Models.Content
{
    public class DeviceDto
    {
        public Guid Id { get; set; }
        public required string Location { get; set; }
        public required DeviceStatus Status { get; set; }
        public DateTime? LastSeen { get; set; }
        //public enum DeviceStatus
        //{
        //    Inactive = 0,
        //    Active = 1
        //}
        public class AutoMapperProfiles : Profile
        {
            public AutoMapperProfiles()
            {
                CreateMap<Device, DeviceDto>();
            }
        }
    }
}
