using CEG_BAL.Configurations;
using CEG_BAL.Services.Implements;
using CEG_BAL.Services.Interfaces;
using CEG_BAL.ViewModels.Admin.Update;
using CEG_BAL.ViewModels.Admin;
using CEG_BAL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CEG_WebAPI.Constants;

namespace CEG_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private readonly IClassService _classService;
        private readonly IScheduleService _scheduleService;
        private readonly ISessionService _sessionService;
        private readonly IConfiguration _config;

        public ScheduleController(IClassService classService, IScheduleService scheduleService, ISessionService sessionService, IConfiguration config)
        {
            _classService = classService;
            _scheduleService = scheduleService;
            _sessionService = sessionService;
            _config = config;
        }
        [Obsolete("This api use old Api mapping that is not correct. Use new api instead", false)]
        [HttpGet("All")]
        [ProducesResponseType(typeof(List<ScheduleViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetScheduleList()
        {
            return await GetList();
        }
        [Obsolete("This api use old Api mapping that is not correct. Use new api instead", false)]
        [HttpGet("Admin/All")]
        [Authorize(Roles = Roles.Admin)]
        [ProducesResponseType(typeof(List<ScheduleViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetScheduleListAdmin()
        {
            return await GetListForAdmin();
        }
        [Obsolete("This api use old Api mapping that is not correct. Use new api instead", false)]
        [HttpPut("{id}/Update/Status")]
        [Authorize(Roles = Roles.Admin +"," + Roles.Teacher)]
        [ProducesResponseType(typeof(ScheduleViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateScheduleStatus(
            [FromRoute][Required] int id,
            [FromBody][Required] string status
            )
        {
            return await UpdateStatus(id, status);
        }
        [Obsolete("This api use old Api mapping that is not correct. Use new api instead", false)]
        [HttpPut("{id}/Update")]
        [Authorize(Roles = Roles.Admin)]
        [ProducesResponseType(typeof(ScheduleViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateSchedule(
            [FromRoute][Required] int id,
            [FromBody][Required] UpdateSchedule upSch
            )
        {
            return await Update(id, upSch);
        }
        [Obsolete("This api use old Api mapping that is not correct. Use new api instead", false)]
        [HttpPost("Create")]
        [Authorize(Roles = Roles.Admin)]
        [ProducesResponseType(typeof(ScheduleViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateSchedule(
            [FromBody][Required] CreateNewSchedule newSch
            )
        {
            return await Create(newSch);
        }
        [Obsolete("This api use old Api mapping that is not correct. Use new api instead", false)]
        [HttpDelete("{id}/Delete")]
        [Authorize(Roles = Roles.Admin)]
        [ProducesResponseType(typeof(ScheduleViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteSchedule(
            [FromRoute][Required] int id
            )
        {
            return await Delete(id);
        }
        [HttpGet]
        [ProducesResponseType(typeof(List<ScheduleViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetList()
        {
            try
            {
                var result = await _scheduleService.GetList();
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Schedule List Not Found!"
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

        [HttpGet("for-admin")]
        [Authorize(Roles = Roles.Admin)]
        [ProducesResponseType(typeof(List<ScheduleViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetListForAdmin()
        {
            try
            {
                var result = await _scheduleService.GetListAdmin();
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Schedule List Not Found!"
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
        [ProducesResponseType(typeof(ScheduleViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            try
            {
                var result = await _scheduleService.GetById(id);
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Schedule Not Found!"
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
        //[HttpGet("Admin/{id}")]
        //[ProducesResponseType(typeof(ScheduleViewModel), StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public async Task<IActionResult> GetClassByIdAdmin([FromRoute] int id)
        //{
        //    try
        //    {
        //        var result = await _classService.GetByIdAdmin(id);
        //        if (result == null)
        //        {
        //            return NotFound(new
        //            {
        //                Status = false,
        //                ErrorMessage = "Class Not Found!"
        //            });
        //        }
        //        return Ok(new
        //        {
        //            Status = true,
        //            Data = result
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new
        //        {
        //            Status = false,
        //            ErrorMessage = ex.Message,
        //            InnerExceptionMessage = ex.InnerException?.Message
        //        });
        //    }
        //}
        [HttpPut("{id}/status")]
        [Authorize(Roles = Roles.Admin + "," + Roles.Teacher)]
        [ProducesResponseType(typeof(ScheduleViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateStatus(
            [FromRoute][Required] int id,
            [FromBody][Required] string status
            )
        {
            try
            {
                var result = await _scheduleService.GetById(id);
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Schedule Does Not Exist!"
                    });
                }
                bool isValid = CEG_BAL_Library.IsScheduleNewStatusValid(result.Status, status);
                if (isValid)
                {
                    await _scheduleService.UpdateStatus(id, status);
                    result = await _scheduleService.GetById(id);
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
                        ErrorMessage = "New status is either an old status or not a valid status for requested schedule"
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
        [HttpPut("{id}")]
        [Authorize(Roles = Roles.Admin)]
        [ProducesResponseType(typeof(ScheduleViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(
            [FromRoute][Required] int id,
            [FromBody][Required] UpdateSchedule upSch
            )
        {
            try
            {
                var result = await _scheduleService.GetById(id);
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Schedule Does Not Exist!"
                    });
                }
                await _scheduleService.Update(id,upSch);
                result = await _scheduleService.GetById(id);
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
        [HttpPost]
        [Authorize(Roles = Roles.Admin)]
        [ProducesResponseType(typeof(ScheduleViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(
            [FromBody][Required] CreateNewSchedule newSch
            )
        {
            try
            {
                var isClassExistOrEditable = await _classService.IsEditableById(newSch.ClassId);
                if (!isClassExistOrEditable)
                {
                    return BadRequest(new
                    {
                        Status = false,
                        ErrorMessage = "Class not found or not in Editable state."
                    });
                }
                var getSession = await _sessionService.GetSessionById(newSch.SessionId);
                if (getSession == null)
                {
                    return BadRequest(new
                    {
                        Status = false,
                        ErrorMessage = "Session not found."
                    });
                }
                await _scheduleService.Create(newSch);
                return Ok(new
                {
                    Data = true,
                    Status = true,
                    SuccessMessage = "Schedule create successfully."
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
        [HttpDelete("{id}")]
        [Authorize(Roles = Roles.Admin)]
        [ProducesResponseType(typeof(ScheduleViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(
            [FromRoute][Required] int id
            )
        {
            try
            {
                var sch = await _scheduleService.GetById(id);
                if (sch == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "schedule with given id does not exist."
                    });
                }
                await _scheduleService.Delete(id);
                return Ok(new
                {
                    Status = true,
                    Data = sch
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
