using AttendanceSystem.Core.Domain.Content;
using AttendanceSystem.Core.Models.Content;
using AttendanceSystem.Core.Models;
using AttendanceSystem.Core.SeedWorks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static AttendanceSystem.Core.SeedWorks.Constants.Permissions;

namespace AttendanceSystem.Api.Controllers.AdminApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public EnrollmentController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpPost]
        [Authorize(Enrollments.View)]

        public async Task<IActionResult> CreateEnrollment([FromBody] CreateUpdateEnrollmentRequest request)
        {
            var o = _mapper.Map<CreateUpdateEnrollmentRequest, Enrollment>(request);

            _unitOfWork.Enrollments.Add(o);

            var result = await _unitOfWork.CompeleAsync();
            return result > 0 ? Ok() : BadRequest();
        }

        [HttpPut]
        [Authorize(Enrollments.Edit)]
        public async Task<IActionResult> UpdateEnrollment(Guid id, [FromBody] CreateUpdateEnrollmentRequest request)
        {
            var o = await _unitOfWork.Enrollments.GetByIdAsync(id);
            if (o == null)
            {
                return NotFound();
            }
            _mapper.Map(request, o);

            await _unitOfWork.CompeleAsync();
            return Ok();
        }

        [HttpDelete]
        [Authorize(Enrollments.Delete)]
        public async Task<IActionResult> DeleteEnrollment([FromQuery] Guid[] ids)
        {
            foreach (var id in ids)
            {
                var o = await _unitOfWork.Enrollments.GetByIdAsync(id);
                if (o == null)
                {
                    return NotFound();
                }
                _unitOfWork.Enrollments.Remove(o);
            }
            var result = await _unitOfWork.CompeleAsync();
            return result > 0 ? Ok() : BadRequest();
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize(Enrollments.View)]
        public async Task<ActionResult<EnrollmentDto>> GetEnrollmentById(Guid id)
        {
            var o = await _unitOfWork.Enrollments.GetByIdAsync(id);
            if (o == null)
            {
                return NotFound();
            }
            var oDto = _mapper.Map<DeviceDto>(o);
            return Ok(oDto);
        }

        [HttpGet]
        [Route("paging")]
        //[Authorize(Enrollments.View)]
        public async Task<ActionResult<PageResult<StudentInClassDto>>> GetEnrollmentsPaging(Guid keyword)
        {
            var result = await _unitOfWork.Enrollments.GetAllStudent(keyword);
            return Ok(result);
        }

        [HttpGet]
        [Authorize(Enrollments.View)]
        public async Task<ActionResult<List<EnrollmentDto>>> GetEnrollments()
        {
            var query = await _unitOfWork.Devices.GetAllAsync();
            var model = _mapper.Map<List<EnrollmentDto>>(query);
            return Ok(model);
        }
    }
}
