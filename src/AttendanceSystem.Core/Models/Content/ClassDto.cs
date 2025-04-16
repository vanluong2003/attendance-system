using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AttendanceSystem.Core.Domain.Content;
using AutoMapper;

namespace AttendanceSystem.Core.Models.Content
{
    public class ClassDto
    {
        public Guid Id { get; set; }
        public required string CourseName { get; set; }
        public required string CourseCode { get; set; }
        public required Guid LecturerID { get; set; }
        public class AutoMapperProfiles : Profile
        {
            public AutoMapperProfiles()
            {
                CreateMap<Class, ClassDto>();
            }
        }
    }
}
