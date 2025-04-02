using AttendanceSystem.Core.Domain.Content;
using AttendanceSystem.Core.Models;
using AttendanceSystem.Core.Models.Content;
using AttendanceSystem.Core.SeedWorks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceSystem.Api.Controllers.AdminApi
{
    [Route("api/admin/class")]
    [ApiController]
    public class ClassController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ClassController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<ClassDto>> GetClassById(Guid id)
        {
            var class1 = await _unitOfWork.Classes.GetByIdAsync(id);
            if(class1 == null)
            {
                return NotFound();
            }
            return Ok(class1);
        }
        [HttpGet]
        [Route("paging")]
        public async Task<ActionResult<PageResult<ClassInListDto>>> GetAllPaging(string? courseName, string? coureCode, int pageIndex, int pageSize = 10 )
        {
            var result = await _unitOfWork.Classes.GetClassPagingAsync(courseName, coureCode, pageIndex, pageSize);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> CreateClass([FromBody] CreateUpdateClassRequest request)
        {
            var class1 = _mapper.Map<CreateUpdateClassRequest, Class>(request);
            _unitOfWork.Classes.Add(class1);
            var result = await _unitOfWork.CompeleAsync();
            return result > 0 ? Ok(result) : BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClass(Guid id, [FromBody] CreateUpdateClassRequest request)
        {
            var class1 = await _unitOfWork.Classes.GetByIdAsync(id);
            if (class1 == null) { return NotFound(); }
            _mapper.Map(request, class1);
            var result = await _unitOfWork.CompeleAsync();
            return result > 0 ? Ok() : BadRequest();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteClasses([FromQuery] Guid[] ids)
        {
            foreach(var id in ids)
            {
                var class1 = await _unitOfWork.Classes.GetByIdAsync(id);
                if(class1 == null)
                {
                    return NotFound();
                }
                _unitOfWork.Classes.Remove(class1);
            }
            var result = await _unitOfWork.CompeleAsync();
            return result > 0 ? Ok() : BadRequest();
        }


        //Test
        [HttpGet]
        [Route("entry/{uid}")]
        public async Task<ActionResult<Enrollment>> GetAttendance(string uid, string location)
        {
            var student = await _unitOfWork.Users.GetUserByUIDAsync(uid);
            if (student == null)
            {
                return NotFound();
            }
            Console.WriteLine(student);
            var classSchedule = await _unitOfWork.ClassSchedules.GetScheduleByDateAndLocation(location, DateTime.Now);
            if (classSchedule == null)
            {
                return NotFound();
            }
            Console.WriteLine(classSchedule);
            var enrollment = await _unitOfWork.Enrollments.GetEnrollmentByStudentAndClass(student.Id, classSchedule.ClassId);
            if (enrollment == null)
            {
                return NotFound();
            }
            Console.WriteLine(enrollment);
            return Ok(enrollment);
        }
    }
}
