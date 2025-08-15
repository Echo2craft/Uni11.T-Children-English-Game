using CEG_BAL.Configurations;
using CEG_BAL.Services.Implements;
using CEG_BAL.Services.Interfaces;
using CEG_BAL.ViewModels;
using CEG_BAL.ViewModels.Admin;
using CEG_BAL.ViewModels.Admin.Get;
using CEG_BAL.ViewModels.Admin.Update;
using CEG_WebAPI.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;

namespace CEG_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassController : ControllerBase
    {
        private readonly IClassService _classService;
        private readonly ICourseService _courseService;
        private readonly ITeacherService _teacherService;
        private readonly IConfiguration _config;

        public ClassController(IClassService classService, ICourseService courseService, ITeacherService teacherService, IConfiguration config)
        {
            _classService = classService;
            _courseService = courseService;
            _teacherService = teacherService;
            _config = config;
        }
        [Obsolete("This api use old Api mapping that is not correct. Use new api instead", false)]
        [HttpGet("All")]
        [ProducesResponseType(typeof(List<ClassViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetClassList()
        {
            return await GetList();
        }
        [Obsolete("This api use old Api mapping that is not correct. Use new api instead", false)]
        [HttpGet("All/Count")]
        [Authorize(Roles = Roles.Admin)]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetTotalClassAmount()
        {
            return await GetTotalAmount();
        }
        [Obsolete("This api use old Api mapping that is not correct. Use new api instead", false)]
        [HttpGet("All/Home")]
        [ProducesResponseType(typeof(List<ClassViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetClassForHomePage()
        {
            return await GetListForHomePage();
        }
        [Obsolete("This api use old Api mapping that is not correct. Use new api instead", false)]
        [HttpGet("All/Status/Open")]
        [ProducesResponseType(typeof(List<GetClassForTransaction>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetClassOptionListByStatusOpen(
            [FromQuery] string stuFullname)
        {
            return await GetOptionListByStatusOpen(stuFullname);
        }
        [Obsolete("This api use old Api mapping that is not correct. Use new api instead", false)]
        [HttpGet("All/Date/Update/Status")]
        [Authorize(Roles = Roles.Admin)]
        [ProducesResponseType(typeof(List<GetClassForTransaction>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateClassListByDate()
        {
            return await UpdateListStatusByDate();
        }
        [Obsolete("This api use old Api mapping that is not correct. Use new api instead", false)]
        [HttpGet("Parent/All")]
        [Authorize(Roles = Roles.Parent)]
        [ProducesResponseType(typeof(List<ClassViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetClassListParent()
        {
            return await GetListForParent();
        }
        [Obsolete("This api use old Api mapping that is not correct. Use new api instead", false)]
        [HttpGet("{id}/All")]
        [Authorize(Roles = Roles.Teacher)]
        [ProducesResponseType(typeof(List<ClassViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetClassListTeacher([FromRoute] int id)
        {
            return await GetListForTeacherByAccountId(id);
        }
        [Obsolete("This api use old Api mapping that is not correct. Use new api instead", false)]
        [HttpGet("All/ByStudent/{id}")]
        [Authorize(Roles = Roles.Student)]
        [ProducesResponseType(typeof(List<ClassViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetClassListStudent([FromRoute] int id)
        {
            return await GetListForStudentByAccountId(id);
        }
        [Obsolete("This api use old Api mapping that is not correct. Use new api instead", false)]
        [HttpGet("{id}/All/{status}/Count")]
        [Authorize(Roles = Roles.Teacher)]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetTotalClassAmountByTeacher(
            [FromRoute] int id,
            [FromRoute] string status
            )
        {
            return await GetTotalAmountByTeacherAccountId(id, status);
        }
        [Obsolete("This api use old Api mapping that is not correct. Use new api instead", false)]
        [HttpGet("Admin/{id}")]
        [Authorize(Roles = Roles.Admin)]
        [ProducesResponseType(typeof(ClassViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetClassByIdAdmin([FromRoute] int id)
        {
            return await GetByIdForAdmin(id);
        }
        [Obsolete("This api use old Api mapping that is not correct. Use new api instead", false)]
        [HttpGet("Teacher/{id}")]
        [Authorize(Roles = Roles.Teacher)]
        [ProducesResponseType(typeof(ClassViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetClassByIdTeacher([FromRoute] int id)
        {
            return await GetByIdForTeacher(id);
        }
        [Obsolete("This api use old Api mapping that is not correct. Use new api instead", false)]
        [HttpGet("Student/{id}")]
        [Authorize(Roles = Roles.Student)]
        [ProducesResponseType(typeof(ClassViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetClassByIdStudent([FromRoute] int id)
        {
            return await GetByIdForStudent(id);
        }
        [Obsolete("This api use old Api mapping that is not correct. Use new api instead", false)]
        [HttpGet("Parent/{id}")]
        [Authorize(Roles = Roles.Parent)]
        [ProducesResponseType(typeof(ClassViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetClassByIdParent([FromRoute] int id)
        {
            return await GetByIdForParent(id);
        }
        [Obsolete("This api use old Api mapping that is not correct. Use new api instead", false)]
        [HttpPost("Name")]
        [ProducesResponseType(typeof(ClassViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetClassByName([FromBody] string className)
        {
            return await GetByClassCode(className);
        }
        [Obsolete("This api use old Api mapping that is not correct. Use new api instead", false)]
        [HttpPut("{id}/Update/Status")]
        [Authorize(Roles = Roles.Teacher + "," + Roles.Admin)]
        [ProducesResponseType(typeof(ClassViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateClassStatus(
            [FromRoute][Required] int id,
            [FromBody][Required] string status
            )
        {
            return await UpdateStatus(id, status);
        }
        [Obsolete("This api use old Api mapping that is not correct. Use new api instead", false)]
        [HttpPut("{id}/Update")]
        [Authorize(Roles = Roles.Admin)]
        [ProducesResponseType(typeof(ClassViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateClass(
            [FromRoute][Required] int id,
            [FromBody][Required] UpdateClass upCla
            )
        {
            return await Update(id, upCla);
        }
        [Obsolete("This api use old Api mapping that is not correct. Use new api instead", false)]
        [HttpPost("Create")]
        [Authorize(Roles = Roles.Admin)]
        [ProducesResponseType(typeof(ClassViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateClass(
            [FromBody][Required] CreateNewClass newCla
            )
        {
            return await Create(newCla);
        }
        [HttpGet]
        [ProducesResponseType(typeof(List<ClassViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetList()
        {
            try
            {
                var result = await _classService.GetClassList();
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Class List Not Found!"
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

        [HttpGet("count")]
        [Authorize(Roles = Roles.Admin)]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetTotalAmount()
        {
            try
            {
                var result = await _classService.GetTotalAmount();
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

        [HttpGet("home")]
        [ProducesResponseType(typeof(List<ClassViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetListForHomePage()
        {
            try
            {
                var result = await _classService.GetClassListHome();
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Class list empty."
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

        [HttpGet("options/status/open")]
        [ProducesResponseType(typeof(List<GetClassForTransaction>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetOptionListByStatusOpen(
            [FromQuery] string stuFullname)
        {
            try
            {
                var result = await _classService.GetOptionListByStatusOpen(stuFullname);
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Class option list by status Open not found!"
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

        [HttpPut("status")]
        [Authorize(Roles = Roles.Admin)]
        [ProducesResponseType(typeof(List<GetClassForTransaction>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateListStatusByDate()
        {
            try
            {
                var result = await _classService.UpdateListStatusByDate();
                if (!result)
                {
                    return BadRequest(new
                    {
                        Status = false,
                        ErrorMessage = "No classes was updated."
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

        [HttpGet("parent")]
        [Authorize(Roles = Roles.Parent)]
        [ProducesResponseType(typeof(List<ClassViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetListForParent()
        {
            try
            {
                var result = await _classService.GetClassListParent();
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Class List Not Found or Empty!"
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
        [HttpGet("teacher/account/{id}")]
        [Authorize(Roles = Roles.Teacher)]
        [ProducesResponseType(typeof(List<ClassViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetListForTeacherByAccountId([FromRoute] int id)
        {
            try
            {
                var result = await _classService.GetListByTeacherAccountId(id);
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Class List Not Found!"
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
        [ProducesResponseType(typeof(List<ClassViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetListForStudentByAccountId([FromRoute] int id)
        {
            try
            {
                var result = await _classService.GetListByStudentAccountId(id);
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Class List Not Found!"
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
        [HttpGet("count/teacher/account/{id}/status/{status}")]
        [Authorize(Roles = Roles.Teacher)]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetTotalAmountByTeacherAccountId(
            [FromRoute] int id,
            [FromRoute] string status
            )
        {
            try
            {
                var result = await _classService.GetTotalAmountByTeacherAccountIdAndClassStatus(id, status);
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
        [ProducesResponseType(typeof(ClassViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            try
            {
                var result = await _classService.GetById(id);
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Class Not Found!"
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
        [HttpGet("{id}/admin")]
        [Authorize(Roles = Roles.Admin)]
        [ProducesResponseType(typeof(ClassViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetByIdForAdmin([FromRoute] int id)
        {
            try
            {
                var result = await _classService.GetById(id, true, true, true, true, true);
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Class Not Found!"
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

        [HttpGet("{id}/teacher")]
        [Authorize(Roles = Roles.Teacher)]
        [ProducesResponseType(typeof(ClassViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetByIdForTeacher([FromRoute] int id)
        {
            try
            {
                var result = await _classService.GetById(id, true, true, true, true, true);
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Class Not Found!"
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
        [HttpGet("{id}/student")]
        [Authorize(Roles = Roles.Student)]
        [ProducesResponseType(typeof(ClassViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetByIdForStudent([FromRoute] int id)
        {
            try
            {
                var result = await _classService.GetById(id,true,true,true,true,true);
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Class Not Found!"
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

        [HttpGet("{id}/parent")]
        [Authorize(Roles = Roles.Parent)]
        [ProducesResponseType(typeof(ClassViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetByIdForParent([FromRoute] int id)
        {
            try
            {
                var result = await _classService.GetById(id, true, true, true, true, true);
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Class Not Found!"
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

        [HttpGet("code/{classCode}")]
        [ProducesResponseType(typeof(ClassViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetByClassCode([FromRoute] string classCode)
        {
            try
            {
                var result = await _classService.GetByClassCode(classCode);
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Class Not Found!"
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
        [Authorize(Roles = Roles.Teacher + "," + Roles.Admin)]
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
                var result = await _classService.GetById(id);
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Class Does Not Exist!"
                    });
                }
                bool isValid = CEG_BAL_Library.IsClassNewStatusValid(result.Status, status);
                if (isValid)
                {
                    await _classService.UpdateStatus(id, status);
                    result = await _classService.GetById(id);
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
                        ErrorMessage = "New status is either an old status or not a valid status for requested class"
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
        [ProducesResponseType(typeof(ClassViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(
            [FromRoute][Required] int id,
            [FromBody][Required] UpdateClass upCla
            )
        {
            try
            {
                var result = await _classService.GetById(id);
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Class Does Not Exist!"
                    });
                }
                var resultTeacherName = await _teacherService.IsExistById(upCla.TeacherId);
                if (!resultTeacherName)
                {
                    return BadRequest(new
                    {
                        Status = false,
                        ErrorMessage = "Teacher not found."
                    });
                }
                await _classService.Update(id, upCla);
                result = await _classService.GetById(id);
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
        [ProducesResponseType(typeof(ClassViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(
            [FromBody][Required] CreateNewClass newCla
            )
        {
            try
            {
                var resultCourseName = await _courseService.IsAvailableById(newCla.CourseId);
                if (!resultCourseName)
                {
                    return BadRequest(new
                    {
                        Status = false,
                        ErrorMessage = "Course not found or course not Available."
                    });
                }
                var resultTeacherName = await _teacherService.IsExistById(newCla.TeacherId);
                if (!resultTeacherName)
                {
                    return BadRequest(new
                    {
                        Status = false,
                        ErrorMessage = "Teacher not found."
                    });
                }
                await _classService.Create(newCla);
                return Ok(new
                {
                    Data = true,
                    Status = true,
                    SuccessMessage = "Class create successfully."
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
