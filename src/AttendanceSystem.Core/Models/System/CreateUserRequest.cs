﻿using AttendanceSystem.Core.Domain.Identity;
using AutoMapper;

namespace AttendanceSystem.Core.Models.System
{
    public class CreateUserRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string UID { get; set; }
        public string Password { get; set; }
        //public DateTime? Dob { get; set; }
        public string? Avatar { get; set; }
        public bool IsActive { get; set; }
        public class AutoMapperProfiles : Profile
        {
            public AutoMapperProfiles()
            {
                CreateMap<CreateUserRequest, AppUser>();
            }
        }
    }
}
