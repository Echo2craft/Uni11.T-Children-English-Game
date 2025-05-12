using CEG_BAL.Services.Interfaces;
using CEG_BAL.ViewModels.Admin;
using CEG_BAL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CEG_BAL.ViewModels.Admin.Update;
using CEG_WebAPI.Constants;

namespace CEG_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly IHomeworkQuestionService _questionService;
        private readonly IHomeworkService _homeworkService;
        private readonly IConfiguration _config;

        public QuestionController(IHomeworkQuestionService questionService, IHomeworkService homeworkService, IConfiguration config)
        {
            _questionService = questionService;
            _homeworkService = homeworkService;
            _config = config;
        }
        [Obsolete("This api use old Api mapping that is not correct. Use new api instead", false)]
        [HttpGet("All")]
        [ProducesResponseType(typeof(List<HomeworkQuestionViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetQuestionList()
        {
            return await GetList();
        }
        [Obsolete("This api use old Api mapping that is not correct. Use new api instead", false)]
        [HttpGet("All/ByCourse/{courseId}")]
        [ProducesResponseType(typeof(List<HomeworkQuestionViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetQuestionListByCourseId(
            [FromRoute][Required] int courseId
            )
        {
            return await GetListByCourseId(courseId);
        }
        [Obsolete("This api use old Api mapping that is not correct. Use new api instead", false)]
        [HttpGet("All/BySession/{sessionId}")]
        [ProducesResponseType(typeof(List<HomeworkQuestionViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetQuestionListBySessionId(
            [FromRoute][Required] int sessionId
            )
        {
            return await GetListBySessionId(sessionId);
        }
        [Obsolete("This api use old Api mapping that is not correct. Use new api instead", false)]
        [HttpGet("All/ByHomework/{homId}")]
        [ProducesResponseType(typeof(List<HomeworkQuestionViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetQuestionListByHomeworkId(
            [FromRoute][Required] int homId
            )
        {
            return await GetListByHomeworkId(homId);
        }
        [Obsolete("This api use old Api mapping that is not correct. Use new api instead", false)]
        [HttpGet("All/Exclude/ByHomework/{homId}")]
        [Authorize(Roles = Roles.Admin)]
        [ProducesResponseType(typeof(List<HomeworkQuestionViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetQuestionListExcludedByHomeworkId(
            [FromRoute][Required] int homId
            )
        {
            return await GetListExcludedByHomeworkId(homId);
        }
        [Obsolete("This api use old Api mapping that is not correct. Use new api instead", false)]
        [HttpGet("All/Ordered")]
        [Authorize(Roles = Roles.Admin)]
        [ProducesResponseType(typeof(List<HomeworkQuestionViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetOrderedQuestionList()
        {
            return await GetOrderedList();
        }
        [Obsolete("This api use old Api mapping that is not correct. Use new api instead", false)]
        [HttpGet("All/Question")]
        [Authorize(Roles = Roles.Teacher)]
        [ProducesResponseType(typeof(List<HomeworkQuestionViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetQuestionListForTeacher()
        {
            return await GetOrderedList();
        }
        [Obsolete("This api use old Api mapping that is not correct. Use new api instead", false)]
        [HttpPost("Create")]
        [ProducesResponseType(typeof(HomeworkQuestionViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateQuestion(
            [FromBody][Required] CreateNewQuestion newSes
            )
        {
            return await Create(newSes);
        }
        [Obsolete("This api use old Api mapping that is not correct. Use new api instead", false)]
        [HttpPost("Create/HomeworkId/{homeworkId}")]
        [ProducesResponseType(typeof(HomeworkQuestionViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateQuestionWithHomeworkId(
            [FromRoute][Required] int homeworkId,
            [FromBody][Required] CreateNewQuestion newSes
            )
        {
            return await CreateByHomeworkId(homeworkId, newSes);
        }
        [Obsolete("This api use old Api mapping that is not correct. Use new api instead", false)]
        [HttpPut("{id}/Update")]
        [Authorize(Roles = Roles.Admin)]
        [ProducesResponseType(typeof(HomeworkQuestionViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateQuestion(
            [FromRoute][Required] int id,
            [FromBody][Required] UpdateQuestion upQue
            )
        {
            return await Update(id, upQue);
        }
        [Obsolete("This api use old Api mapping that is not correct. Use new api instead", false)]
        [HttpPut("{questionId}/Update/HomeworkId/{homeworkId}")]
        [Authorize(Roles = Roles.Admin)]
        [ProducesResponseType(typeof(HomeworkQuestionViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateQuestion(
            [FromRoute][Required] int questionId,
            [FromRoute][Required] int homeworkId
            )
        {
            return await UpdateWithHomework(questionId, homeworkId);
        }
        [Obsolete("This api use old Api mapping that is not correct. Use new api instead", false)]
        [HttpDelete("{id}/Homework/{homId}/Delete")]
        [Authorize(Roles = Roles.Admin)]
        [ProducesResponseType(typeof(HomeworkQuestionViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteQuestion(
            [FromRoute][Required] int id,
            [FromRoute][Required] int homId
            )
        {
            return await Delete(id, homId);
        }
        [HttpGet]
        [ProducesResponseType(typeof(List<HomeworkQuestionViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetList()
        {
            try
            {
                var result = await _questionService.GetList();
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Question List Not Found!"
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

        [HttpGet("course/{courseId}")]
        [ProducesResponseType(typeof(List<HomeworkQuestionViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetListByCourseId(
            [FromRoute][Required] int courseId
            )
        {
            try
            {
                var result = await _questionService.GetListByCourseId(courseId);
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Question List based on course Id not found!"
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

        [HttpGet("session/{sessionId}")]
        [ProducesResponseType(typeof(List<HomeworkQuestionViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetListBySessionId(
            [FromRoute][Required] int sessionId
            )
        {
            try
            {
                var result = await _questionService.GetListBySessionId(sessionId);
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Question List based on session Id not found!"
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

        [HttpGet("homework/{homId}")]
        [ProducesResponseType(typeof(List<HomeworkQuestionViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetListByHomeworkId(
            [FromRoute][Required] int homId
            )
        {
            try
            {
                var result = await _questionService.GetListByHomeworkId(homId);
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Question List based on homework Id not found!"
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

        [HttpGet("excluded-from-homework/{homId}")]
        [Authorize(Roles = Roles.Admin)]
        [ProducesResponseType(typeof(List<HomeworkQuestionViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetListExcludedByHomeworkId(
            [FromRoute][Required] int homId
            )
        {
            try
            {
                var result = await _questionService.GetExcludedListByHomeworkId(homId);
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Excluded question list based on homework Id not found!"
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

        [HttpGet("ordered")]
        [Authorize(Roles = Roles.Admin + "," + Roles.Teacher)]
        [ProducesResponseType(typeof(List<HomeworkQuestionViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetOrderedList()
        {
            try
            {
                var result = await _questionService.GetOrderedList();
                if (result == null || result.Count == 0)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Question List Not Found!"
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
        [ProducesResponseType(typeof(HomeworkQuestionViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            try
            {
                var result = await _questionService.GetById(id);
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Question Not Found!"
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

        /*[HttpGet("ByCourse/{courseId}")]
        [ProducesResponseType(typeof(List<HomeworkQuestionViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetQuestionListByCourseId([FromRoute] int courseId)
        {
            try
            {
                var result = await _questionService.GetQuestionListById(courseId);
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "No Questions Found With This Course!"
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
        }*/

        [HttpPost]
        [ProducesResponseType(typeof(HomeworkQuestionViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(
            [FromBody][Required] CreateNewQuestion newSes
            )
        {
            try
            {
                /*var resulthomeworkName = await _homeworkService.IsHomeworkExistByTitle(newSes.HomeworkTitle);
                if (!resulthomeworkName)
                {
                    return BadRequest(new
                    {
                        Status = false,
                        ErrorMessage = "Homework Not Found!"
                    });
                }*/
                // HomeworkQuestionViewModel sess = new HomeworkQuestionViewModel();
                await _questionService.Create(newSes);
                return Ok(new
                {
                    Data = true,
                    Status = true,
                    SuccessMessage = "Question Create Successfully!"
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

        [HttpPost("for-homework/{homeworkId}")]
        [ProducesResponseType(typeof(HomeworkQuestionViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateByHomeworkId (
            [FromRoute][Required] int homeworkId,
            [FromBody][Required] CreateNewQuestion newSes
            )
        {
            try
            {
                /*var resulthomeworkName = await _homeworkService.IsHomeworkExistByTitle(newSes.HomeworkTitle);
                if (!resulthomeworkName)
                {
                    return BadRequest(new
                    {
                        Status = false,
                        ErrorMessage = "Homework Not Found!"
                    });
                }*/
                /*HomeworkQuestionViewModel sess = new HomeworkQuestionViewModel();*/
                await _questionService.CreateWithHomeworkId(newSes, homeworkId);
                return Ok(new
                {
                    Data = true,
                    Status = true,
                    SuccessMessage = "Question Create Successfully!"
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
        [ProducesResponseType(typeof(HomeworkQuestionViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(
            [FromRoute][Required] int id,
            [FromBody][Required] UpdateQuestion upQue
            )
        {
            try
            {
                var result = await _questionService.GetById(id);
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Question Does Not Exist"
                    });
                }
                // question.HomeworkQuestionId = id;
                await _questionService.Update(id, upQue);
                result = await _questionService.GetById(id);
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
        [HttpPut("{questionId}/for-homework/{homeworkId}")]
        [Authorize(Roles = Roles.Admin)]
        [ProducesResponseType(typeof(HomeworkQuestionViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateWithHomework(
            [FromRoute][Required] int questionId,
            [FromRoute][Required] int homeworkId
            )
        {
            try
            {
                var result = await _questionService.GetById(questionId);
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Question Does Not Exist"
                    });
                }
                await _questionService.UpdateWithHomeworkId(questionId, homeworkId);
                result = await _questionService.GetById(questionId);
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
        [HttpDelete("{id}/for-homework/{homId}")]
        [Authorize(Roles = Roles.Admin)]
        [ProducesResponseType(typeof(HomeworkQuestionViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(
            [FromRoute][Required] int id,
            [FromRoute][Required] int homId
            )
        {
            try
            {
                var hom = await _homeworkService.GetHomeworkById(homId);
                if (hom == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "homework with given Id does not exist."
                    });
                }
                var ses = await _questionService.GetById(id, homId);
                if (ses == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "question with given id does not exist."
                    });
                }
                await _questionService.Delete(id, homId);
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
