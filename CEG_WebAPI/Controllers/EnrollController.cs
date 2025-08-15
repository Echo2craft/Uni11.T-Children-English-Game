using CEG_BAL.Services.Implements;
using CEG_BAL.Services.Interfaces;
using CEG_BAL.ViewModels.Admin;
using CEG_BAL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CEG_BAL.Configurations;
using CEG_BAL.ViewModels.Parent;
using CEG_WebAPI.Constants;

namespace CEG_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollController : ControllerBase
    {
        private readonly IEnrollService _enrollService;
        private readonly IConfiguration _config;

        public EnrollController(IEnrollService enrollService, IConfiguration config)
        {
            _enrollService = enrollService;
            _config = config;
        }
        [Obsolete("This api use old Api mapping that is not correct. Use new api instead", false)]
        [HttpGet("All")]
        [ProducesResponseType(typeof(List<EnrollViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetEnrollList()
        {
            return await GetList();
        }
        [Obsolete("This api use old Api mapping that is not correct. Use new api instead", false)]
        [HttpGet("Parent/{id}")]
        [Authorize(Roles = Roles.Parent)]
        [ProducesResponseType(typeof(List<EnrollViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetEnrollListParent(
            [FromRoute][Required] int id)
        {
            return await GetListForParent(id);
        }
        [Obsolete("This api use old Api mapping that is not correct. Use new api instead", false)]
        [HttpGet("Student/{id}")]
        [Authorize(Roles = Roles.Student)]
        [ProducesResponseType(typeof(List<EnrollViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetEnrollListStudent(
            [FromRoute][Required] int id)
        {
            return await GetListForStudent(id);
        }
        [Obsolete("This api use old Api mapping that is not correct. Use new api instead", false)]
        [HttpPost("Create")]
        [ProducesResponseType(typeof(EnrollViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateEnroll(
            [FromBody][Required] CreateNewEnroll newEn
            )
        {
            return await Create(newEn);
        }
        [Obsolete("This api use old Api mapping that is not correct. Use new api instead", false)]
        [HttpPut("{id}/Update")]
        [Authorize(Roles = Roles.Admin)]
        [ProducesResponseType(typeof(HomeworkViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateEnroll(
            [FromRoute][Required] int id,
            [FromBody][Required] EnrollViewModel upEnr
            )
        {
            return await Update(id, upEnr);
        }
        [Obsolete("This api use old Api mapping that is not correct. Use new api instead", false)]
        [HttpPut("{id}/Update/Status")]
        [Authorize(Roles = Roles.Admin)]
        [ProducesResponseType(typeof(ClassViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateEnrollStatus(
            [FromRoute][Required] int id,
            [FromBody][Required] string upEnrSta
            )
        {
            return await UpdateStatus(id, upEnrSta);
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<EnrollViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetList()
        {
            try
            {
                var result = await _enrollService.GetList();
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Enroll List Not Found!"
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
        [ProducesResponseType(typeof(EnrollViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            try
            {
                var result = await _enrollService.GetById(id);
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Enroll Not Found!"
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

        [HttpGet("parent/account/{id}")]
        [Authorize(Roles = Roles.Parent)]
        [ProducesResponseType(typeof(List<EnrollViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetListForParent(
            [FromRoute][Required] int id)
        {
            try
            {
                var result = await _enrollService.GetEnrollByParentAccountId(id);
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Enroll List Not Found!"
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

        [HttpGet("student/account/{id}")]
        [Authorize(Roles = Roles.Student)]
        [ProducesResponseType(typeof(List<EnrollViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetListForStudent(
            [FromRoute][Required] int id)
        {
            try
            {
                var result = await _enrollService.GetEnrollByStudentAccountId(id);
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Enroll List Not Found!"
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
        [ProducesResponseType(typeof(EnrollViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(
            [FromBody][Required] CreateNewEnroll newEn
            )
        {
            try
            {
                /*var resultSessionTitle = await _sessionService.IsSessionExistByTitle(newHw.SessionTitle);
                if (!resultSessionTitle)
                {
                    return BadRequest(new
                    {
                        Status = false,
                        ErrorMessage = "Session Not Found!"
                    });
                }*/
                await _enrollService.Create(newEn);
                return Ok(new
                {
                    Data = true,
                    Status = true,
                    SuccessMessage = "Enroll Create Successfully!"
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
            [FromBody][Required] EnrollViewModel upEnr
            )
        {
            try
            {
                var result = await _enrollService.GetById(id);
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Enroll Does Not Exist"
                    });
                }
                upEnr.EnrollId = id;
                _enrollService.Update(upEnr);
                result = await _enrollService.GetById(upEnr.EnrollId.Value);
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
        [Authorize(Roles = Roles.Admin)]
        [ProducesResponseType(typeof(ClassViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateStatus(
            [FromRoute][Required] int id,
            [FromBody][Required] string upEnrSta
            )
        {
            try
            {
                var result = await _enrollService.GetById(id);
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Enroll does not Exist."
                    });
                }
                bool isValid = CEG_BAL_Library.IsEnrollNewStatusValid(result.Status, upEnrSta);
                if (isValid)
                {
                    _enrollService.UpdateStatus(id, upEnrSta);
                    result = await _enrollService.GetById(id);
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
                        ErrorMessage = "New status is either an old status or not a valid status for requested enroll"
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
