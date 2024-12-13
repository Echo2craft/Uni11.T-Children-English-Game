using CEG_BAL.Services.Implements;
using CEG_BAL.Services.Interfaces;
using CEG_BAL.ViewModels;
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

        [HttpGet("All")]
        [ProducesResponseType(typeof(List<AttendanceViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAttendanceList()
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
        public async Task<IActionResult> GetAttendanceById([FromRoute] int id)
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

        [HttpGet("All/ByScheduleId/{scheduleId}")]
        [ProducesResponseType(typeof(AttendanceViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAttendanceByScheduleId(
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
    }
}
