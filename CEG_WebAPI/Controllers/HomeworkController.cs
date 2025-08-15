using CEG_BAL.Services.Implements;
using CEG_BAL.Services.Interfaces;
using CEG_BAL.ViewModels;
using CEG_BAL.ViewModels.Admin;
using CEG_BAL.ViewModels.Admin.Update;
using CEG_WebAPI.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CEG_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeworkController : ControllerBase
    {
        private readonly IHomeworkService _homeworkService;
        private readonly ISessionService _sessionService;
        private readonly IConfiguration _config;

        public HomeworkController(IHomeworkService homeworkService, ISessionService sessionService, IConfiguration config)
        {
            _homeworkService = homeworkService;
            _sessionService = sessionService;
            _config = config;
        }
        [Obsolete("This api use old Api mapping that is not correct. Use new api instead", false)]
        [HttpGet("All")]
        [ProducesResponseType(typeof(List<HomeworkViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetHomeworkList()
        {
            return await GetList();
        }
        [Obsolete("This api use old Api mapping that is not correct. Use new api instead", false)]
        [HttpPost("Create")]
        [ProducesResponseType(typeof(HomeworkViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateHomework(
            [FromBody][Required] CreateNewHomework newHw
            )
        {
            return await Create(newHw);
        }
        [Obsolete("This api use old Api mapping that is not correct. Use new api instead", false)]
        [HttpPut("{id}/Update")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(HomeworkViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateHomework(
            [FromRoute][Required] int id,
            [FromBody][Required] UpdateHomework upHw
            )
        {
            return await Update(id, upHw);
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<HomeworkViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetList()
        {
            try
            {
                var result = await _homeworkService.GetHomeworkList();
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Homework List Not Found!"
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
        [ProducesResponseType(typeof(HomeworkViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            try
            {
                var result = await _homeworkService.GetHomeworkById(id);
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Homework Not Found!"
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

        [HttpPost]
        [ProducesResponseType(typeof(HomeworkViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(
            [FromBody][Required] CreateNewHomework newHw
            )
        {
            try
            {
                var resultSessionTitle = await _sessionService.GetSessionById(newHw.SessionId);
                if (resultSessionTitle == null)
                {
                    return BadRequest(new
                    {
                        Status = false,
                        ErrorMessage = "Session Not Found!"
                    });
                }
                await _homeworkService.Create(newHw);
                return Ok(new
                {
                    Data = true,
                    Status = true,
                    SuccessMessage = "Homework Create Successfully!"
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
        [HttpPut("{id}")]
        [Authorize(Roles = Roles.Admin)]
        [ProducesResponseType(typeof(HomeworkViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(
            [FromRoute][Required] int id,
            [FromBody][Required] UpdateHomework upHw
            )
        {
            try
            {
                var result = await _homeworkService.GetHomeworkById(id);
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Homework Does Not Exist"
                    });
                }
                // homework.HomeworkId = id;
                await _homeworkService.Update(id,upHw);
                result = await _homeworkService.GetHomeworkById(id);
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
        [HttpDelete("{id}")]
        [Authorize(Roles = Roles.Admin)]
        [ProducesResponseType(typeof(HomeworkViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(
            [FromRoute][Required] int id
            )
        {
            try
            {
                var ses = await _homeworkService.GetHomeworkById(id);
                if (ses == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "homework with given id does not exist."
                    });
                }
                await _homeworkService.Delete(id);
                return Ok(new
                {
                    Status = true,
                    Data = ses
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
