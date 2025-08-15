using CEG_BAL.Configurations;
using CEG_BAL.Services.Implements;
using CEG_BAL.Services.Interfaces;
using CEG_BAL.ViewModels;
using CEG_WebAPI.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CEG_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private readonly IAttendanceService _attendanceService;
        private readonly IConfiguration _configuration;

        public AttendanceController(IAttendanceService attendanceService, IConfiguration configuration)
        {
            _attendanceService = attendanceService;
            _configuration = configuration;
        }
        [Obsolete("This api use old Api url mapping that is not correct. Use new api instead", false)]
        [HttpGet("All")]
        [ProducesResponseType(typeof(List<AttendanceViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAttendanceList()
        {
            return await GetList();
        }
        [Obsolete("This api use old Api url mapping that is not correct. Use new api instead", false)]
        [HttpGet("All/ByScheduleId/{scheduleId}")]
        [Authorize(Roles = Roles.Teacher)]
        [ProducesResponseType(typeof(AttendanceViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAttendanceByScheduleId(
            [FromRoute][Required] int scheduleId
            )
        {
            return await GetListByScheduleId(scheduleId);
        }
        [Obsolete("This api use old Api url mapping that is not correct. Use new api instead", false)]
        [HttpPut("{id}/Update/Status")]
        [Authorize(Roles = Roles.Teacher)]
        [ProducesResponseType(typeof(ClassViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateAttendanceStatus(
            [FromRoute][Required] int id,
            [FromBody][Required] string status
            )
        {
            return await UpdateStatus(id, status);
        }
        [HttpGet]
        [ProducesResponseType(typeof(List<AttendanceViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetList()
        {
            try
            {
                var result = await _attendanceService.GetListNoTracking();
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Attendance List Not Found!"
                    });
                }
                return Ok(new
                {
                    Status = true,
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Status = false,
                    ErrorMessage = ex.Message,
                    InnerExceptionMessage = ex.InnerException?.Message
                });
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(AttendanceViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            try
            {
                var result = await _attendanceService.GetById(id);
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Account Not Found!"
                    });
                }
                return Ok(new
                {
                    Status = true,
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Status = false,
                    ErrorMessage = ex.Message,
                    InnerExceptionMessage = ex.InnerException?.Message
                });
            }
        }

        [HttpGet("schedule/{scheduleId}")]
        [Authorize(Roles = Roles.Teacher)]
        [ProducesResponseType(typeof(AttendanceViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetListByScheduleId(
            [FromRoute][Required] int scheduleId
            )
        {
            try
            {
                var result = await _attendanceService.GetListByScheduleId(scheduleId);
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Attendance List Not Found!"
                    });
                }
                return Ok(new
                {
                    Status = true,
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Status = false,
                    ErrorMessage = ex.Message,
                    InnerExceptionMessage = ex.InnerException?.Message
                });
            }
        }

        [HttpPut("{id}/status")]
        [Authorize(Roles = Roles.Teacher)]
        [ProducesResponseType(typeof(ClassViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateStatus(
            [FromRoute][Required] int id,
            [FromBody][Required] string status
            )
        {
            try
            {
                var result = await _attendanceService.GetById(id);
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Attendance does not exist."
                    });
                }
                bool isValid = CEG_BAL_Library.IsAttendanceNewStatusValid(result.HasAttended, status);
                if (isValid)
                {
                    await _attendanceService.UpdateStatus(id, status);
                    result = await _attendanceService.GetById(id);
                    return Ok(new
                    {
                        Status = true,
                        Data = result
                    });
                }
                else
                {
                    return BadRequest(new
                    {
                        Status = false,
                        ErrorMessage = "New status is either an old status or not a valid status for requested attendance"
                    });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Status = false,
                    ErrorMessage = ex.Message,
                    InnerExceptionMessage = ex.InnerException?.Message
                });
            }
        }
    }
}
