using System.Security.Claims;
using AttendanceSystem.Core.Domain.Content;
using AttendanceSystem.Core.Domain.Identity;
using AttendanceSystem.Core.Models;
using AttendanceSystem.Core.Models.Content;
using AttendanceSystem.Core.SeedWorks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static AttendanceSystem.Core.SeedWorks.Constants.Permissions;

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

        [HttpPost]
        [Authorize(Classes.View)]

        public async Task<IActionResult> CreateClass([FromBody] CreateUpdateClassRequest request)
        {
            var class1 = _mapper.Map<CreateUpdateClassRequest, Class>(request);
            var checkClass = await _unitOfWork.Classes.GetByCourseCode(class1.CourseCode);
            if (checkClass != null)
            {
                return BadRequest("Mã lớp đã tồn tại");
            }
            _unitOfWork.Classes.Add(class1);

            var result = await _unitOfWork.CompeleAsync();
            return result > 0 ? Ok() : BadRequest();
        }

        // Check
        [HttpPost]
        [Route("enroll")]
        [Authorize(Enrollments.Edit)]

        public async Task<IActionResult> CreateClassEnroll([FromBody] CreateUpdateEnrollmentRequest request)
        {
            var enrollment = _mapper.Map<CreateUpdateEnrollmentRequest, Enrollment>(request);

            _unitOfWork.Enrollments.Add(enrollment);

            var result = await _unitOfWork.CompeleAsync();
            return result > 0 ? Ok() : BadRequest();
        }

        [HttpPost]
        [Route("schedule")]
        [Authorize(ClassSchedules.Edit)]

        public async Task<IActionResult> CreateClassSchedule([FromBody] CreateUpdateClassScheduleRequest request)
        {
            var classSchedule = _mapper.Map<CreateUpdateClassScheduleRequest, ClassSchedule>(request);

            _unitOfWork.ClassSchedules.Add(classSchedule);

            var result = await _unitOfWork.CompeleAsync();
            return result > 0 ? Ok() : BadRequest();
        }
        //

        [HttpPut]
        [Authorize(Classes.Edit)]
        public async Task<IActionResult> UpdateClass(Guid id, [FromBody] CreateUpdateClassRequest request)
        {
            var class1 = await _unitOfWork.Classes.GetByIdAsync(id);
            string preCourseCode = class1.CourseCode;
            string curCourseCode = request.CourseCode;
            if (preCourseCode != curCourseCode)
            {
                var checkClass = await _unitOfWork.Classes.GetByCourseCode(curCourseCode);
                if (checkClass != null)
                {
                    return BadRequest("Mã lớp đã tồn tại");
                }
            }
            _mapper.Map(request, class1);

            await _unitOfWork.CompeleAsync();
            return Ok();
        }

        [HttpDelete]
        [Authorize(Classes.Delete)]
        public async Task<IActionResult> DeleteClass([FromQuery] Guid[] ids)
        {
            foreach (var id in ids)
            {
                var class1 = await _unitOfWork.Classes.GetByIdAsync(id);
                if (class1 == null)
                {
                    return NotFound();
                }
                _unitOfWork.Classes.Remove(class1);
            }
            var result = await _unitOfWork.CompeleAsync();
            return result > 0 ? Ok() : BadRequest();
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize(Classes.View)]
        public async Task<ActionResult<ClassDto>> GetClassById(Guid id)
        {
            var class1 = await _unitOfWork.Classes.GetByIdAsync(id);
            if (class1 == null)
            {
                return NotFound();
            }
            //
            //var userName = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //Console.WriteLine(userName);
            //
            var classDto = _mapper.Map<ClassDto>(class1);
            return Ok(classDto);
        }

        [HttpGet]
        [Route("paging")]
        [Authorize(Classes.View)]
        public async Task<ActionResult<PageResult<ClassDto>>> GetClassesPaging(string? keyword,
            int pageIndex, int pageSize = 10)
        {
            var result = await _unitOfWork.Classes.GetAllPaging(keyword, pageIndex, pageSize);
            return Ok(result);
        }

        [HttpGet]
        [Authorize(Classes.View)]
        public async Task<ActionResult<List<ClassDto>>> GetClasses()
        {
            var query = await _unitOfWork.Classes.GetAllAsync();
            var model = _mapper.Map<List<ClassDto>>(query);
            return Ok(model);
        }
    }
}
