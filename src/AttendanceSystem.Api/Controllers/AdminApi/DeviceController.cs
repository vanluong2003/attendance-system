using AttendanceSystem.Core.Domain.Content;
using AttendanceSystem.Core.Models;
using AttendanceSystem.Core.Models.Content;
using AttendanceSystem.Core.SeedWorks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static AttendanceSystem.Core.SeedWorks.Constants.Permissions;

namespace AttendanceSystem.Api.Controllers.AdminApi
{
    [Route("api/admin/device")]
    public class DeviceController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public DeviceController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpPost]
        [Authorize(Devices.View)]

        public async Task<IActionResult> CreateDevice([FromBody] CreateUpdateDeviceRequest request)
        {
            var device = _mapper.Map<CreateUpdateDeviceRequest, Device>(request);

            _unitOfWork.Devices.Add(device);

            var result = await _unitOfWork.CompeleAsync();
            return result > 0 ? Ok() : BadRequest();
        }

        [HttpPut]
        [Authorize(Devices.Edit)]
        public async Task<IActionResult> UpdateDevice(Guid id, [FromBody] CreateUpdateDeviceRequest request)
        {
            var device = await _unitOfWork.Devices.GetByIdAsync(id);
            if (device == null)
            {
                return NotFound();
            }
            _mapper.Map(request, device);

            await _unitOfWork.CompeleAsync();
            return Ok();
        }

        [HttpDelete]
        [Authorize(Devices.Delete)]
        public async Task<IActionResult> DeleteDevice([FromQuery] Guid[] ids)
        {
            foreach (var id in ids)
            {
                var device = await _unitOfWork.Devices.GetByIdAsync(id);
                if (device == null)
                {
                    return NotFound();
                }
                _unitOfWork.Devices.Remove(device);
            }
            var result = await _unitOfWork.CompeleAsync();
            return result > 0 ? Ok() : BadRequest();
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize(Devices.View)]
        public async Task<ActionResult<DeviceDto>> GetDeviceById(Guid id)
        {
            var category = await _unitOfWork.Devices.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            var categoryDto = _mapper.Map<DeviceDto>(category);
            return Ok(categoryDto);
        }

        [HttpGet]
        [Route("paging")]
        [Authorize(Devices.View)]
        public async Task<ActionResult<PageResult<DeviceDto>>> GetDevicesPaging(string? keyword,
            int pageIndex, int pageSize = 10)
        {
            var result = await _unitOfWork.Devices.GetAllPaging(keyword, pageIndex, pageSize);
            return Ok(result);
        }

        [HttpGet]
        [Authorize(Devices.View)]
        public async Task<ActionResult<List<DeviceDto>>> GetDevices()
        {
            var query = await _unitOfWork.Devices.GetAllAsync();
            var model = _mapper.Map<List<DeviceDto>>(query);
            return Ok(model);
        }
    }
}
