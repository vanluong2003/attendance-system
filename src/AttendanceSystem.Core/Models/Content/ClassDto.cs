using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AttendanceSystem.Core.Domain.Content;
using AutoMapper;

namespace AttendanceSystem.Core.Models.Content
{
    public class ClassDto : ClassInListDto
    {
        public class AutoMapperProfiles : Profile
        {
            public AutoMapperProfiles()
            {
                CreateMap<Class, ClassDto>();
            }
        }
    }
}
