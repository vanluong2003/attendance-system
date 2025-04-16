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
    public class ClassScheduleController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ClassScheduleController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpPost]
        [Authorize(ClassSchedules.View)]

        public async Task<IActionResult> CreateClassSchedule([FromBody] CreateUpdateClassScheduleRequest request)
        {
            var o = _mapper.Map<CreateUpdateClassScheduleRequest, ClassSchedule>(request);

            _unitOfWork.ClassSchedules.Add(o);

            var result = await _unitOfWork.CompeleAsync();
            return result > 0 ? Ok() : BadRequest();
        }

        [HttpPut]
        [Authorize(ClassSchedules.Edit)]
        public async Task<IActionResult> UpdateClassSchedule(Guid id, [FromBody] CreateUpdateEnrollmentRequest request)
        {
            var o = await _unitOfWork.ClassSchedules.GetByIdAsync(id);
            if (o == null)
            {
                return NotFound();
            }
            _mapper.Map(request, o);

            await _unitOfWork.CompeleAsync();
            return Ok();
        }

        [HttpDelete]
        [Authorize(ClassSchedules.Delete)]
        public async Task<IActionResult> DeleteClassSchedule([FromQuery] Guid[] ids)
        {
            foreach (var id in ids)
            {
                var o = await _unitOfWork.ClassSchedules.GetByIdAsync(id);
                if (o == null)
                {
                    return NotFound();
                }
                _unitOfWork.ClassSchedules.Remove(o);
            }
            var result = await _unitOfWork.CompeleAsync();
            return result > 0 ? Ok() : BadRequest();
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize(ClassSchedules.View)]
        public async Task<ActionResult<ClassScheduleDto>> GetClassScheduleById(Guid id)
        {
            var o = await _unitOfWork.ClassSchedules.GetByIdAsync(id);
            if (o == null)
            {
                return NotFound();
            }
            var oDto = _mapper.Map<ClassScheduleDto>(o);
            return Ok(oDto);
        }

        [HttpGet]
        [Route("paging")]
        [Authorize(ClassSchedules.View)]
        public async Task<ActionResult<PageResult<ClassScheduleDto>>> GetClassSchedulesPaging(Guid keyword,
            int pageIndex, int pageSize = 10)
        {
            var result = await _unitOfWork.ClassSchedules.GetAllPaging(keyword, pageIndex, pageSize);
            return Ok(result);
        }

        [HttpGet]
        [Authorize(ClassSchedules.View)]
        public async Task<ActionResult<List<ClassScheduleDto>>> GetClassSchedules()
        {
            var query = await _unitOfWork.ClassSchedules.GetAllAsync();
            var model = _mapper.Map<List<ClassScheduleDto>>(query);
            return Ok(model);
        }
    }
}
