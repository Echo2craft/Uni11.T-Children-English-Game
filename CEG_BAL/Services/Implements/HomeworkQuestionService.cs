using AutoMapper;
using CEG_BAL.Configurations;
using CEG_BAL.Services.Interfaces;
using CEG_BAL.ViewModels;
using CEG_BAL.ViewModels.Admin;
using CEG_BAL.ViewModels.Admin.Update;
using CEG_DAL.Infrastructure;
using CEG_DAL.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_BAL.Services.Implements
{
    public class HomeworkQuestionService : IHomeworkQuestionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public HomeworkQuestionService(
            IUnitOfWork unitOfWork, 
            IMapper mapper
            )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public void Create(HomeworkQuestionViewModel model, CreateNewQuestion newQus)
        {
            var ques = _mapper.Map<HomeworkQuestion>(model);
            if (newQus != null)
            {
                ques.Question = newQus.Question;
                if(ques.Homework?.HomeworkId == 0)
                {
                    ques.Homework = null;
                }
                /*ques.HomeworkId = _unitOfWork.HomeworkRepositories.GetIdByTitle(newQus.HomeworkTitle).Result;*/
            }
            _unitOfWork.HomeworkQuestionRepositories.Create(ques);
            _unitOfWork.Save();
        }
        public void CreateWithHomeworkId(HomeworkQuestionViewModel model, CreateNewQuestion newQus, int homeworkId)
        {
            var ques = _mapper.Map<HomeworkQuestion>(model);
            if (newQus != null)
            {
                ques.Question = newQus.Question;
                if (ques.Homework?.HomeworkId == 0)
                {
                    ques.Homework = null;
                }
                ques.HomeworkId = homeworkId;
                /*ques.HomeworkId = _unitOfWork.HomeworkRepositories.GetIdByTitle(newQus.HomeworkTitle).Result;*/
            }
            _unitOfWork.HomeworkQuestionRepositories.Create(ques);
            _unitOfWork.Save();
        }

        public async Task<List<HomeworkQuestionViewModel>?> GetOrderedList()
        {
            return _mapper.Map<List<HomeworkQuestionViewModel>?>(await _unitOfWork.HomeworkQuestionRepositories.GetOrderedQuestionList());
        }

        public async Task<HomeworkQuestionViewModel?> GetById(int id, int homId = 0)
        {
            var ques = homId != 0 ? 
                await _unitOfWork.HomeworkQuestionRepositories.GetByIdNoTracking(id,homId) : 
                await _unitOfWork.HomeworkQuestionRepositories.GetByIdNoTracking(id);
            if (ques != null)
            {
                var quesvm = _mapper.Map<HomeworkQuestionViewModel>(ques);
                if (ques.HomeworkId != null)
                {
                    var home = await _unitOfWork.HomeworkRepositories.GetByIdNoTracking(ques.HomeworkId.Value);
                    //quesvm.HomeworkStatus = home?.Status;
                }
                quesvm.CourseStatus = await _unitOfWork.CourseRepositories.GetStatusByQuestionIdNoTracking(id);
                return quesvm;
            }
            return null;
        }

        public async Task<List<HomeworkQuestionViewModel>> GetList()
        {
            return _mapper.Map<List<HomeworkQuestionViewModel>>(await _unitOfWork.HomeworkQuestionRepositories.GetList());
        }

        public async Task<List<HomeworkQuestionViewModel>> GetListByCourseId(int courseId)
        {
            return _mapper.Map<List<HomeworkQuestionViewModel>>(await _unitOfWork.HomeworkQuestionRepositories.GetListByCourseId(courseId));
        }

        public async Task<List<HomeworkQuestionViewModel>> GetListBySessionId(int sessionId)
        {
            return _mapper.Map<List<HomeworkQuestionViewModel>>(await _unitOfWork.HomeworkQuestionRepositories.GetListBySessionId(sessionId));
        }
        public async Task<List<HomeworkQuestionViewModel>> GetListByHomeworkId(int homId)
        {
            var homList = _mapper.Map<List<HomeworkQuestionViewModel>>(await _unitOfWork.HomeworkQuestionRepositories.GetListByHomeworkId(homId));
            for (int i = 0; i < homList.Count; i++)
            {
                homList[i].QuestionNumber = i + 1;
                homList[i].AnswersAmount = homList[i].HomeworkAnswers.Count;
                for(int j = 0; j < homList[i].HomeworkAnswers.Count; j++)
                {
                    homList[i].HomeworkAnswers[j].AnswerNumber = j + 1;
                }
            }
            return homList;
        }
        public async Task<List<HomeworkQuestionViewModel>> GetExcludedListByHomeworkId(int homId)
        {
            var homList = _mapper.Map<List<HomeworkQuestionViewModel>>(await _unitOfWork.HomeworkQuestionRepositories.GetExcludedListByHomeworkId(homId));
            for (int i = 0; i < homList.Count; i++)
            {
                homList[i].AnswersAmount = homList[i].HomeworkAnswers.Count;
            }
            return homList;
        }

        /*public void Update(HomeworkQuestionViewModel model)
        {
            var ques = _mapper.Map<HomeworkQuestion>(model);
            if (ques.Homework?.HomeworkId == 0)
            {
                ques.Homework = null;
            }
            _unitOfWork.HomeworkQuestionRepositories.Update(ques);
            _unitOfWork.Save();
        }*/

        public async Task Update(int upQueId, UpdateQuestion upQue)
        {
            if (upQue == null)
                throw new ArgumentNullException(nameof(upQue), "New question info for updating cannot be null.");

            // Fetch the existing record
            var que = await _unitOfWork.HomeworkQuestionRepositories.GetByIdNoTracking(upQueId)
                ?? throw new KeyNotFoundException("Question not found.");
            if (que.HomeworkId != null)
            {
                string? status = await _unitOfWork.CourseRepositories.GetStatusByQuestionIdNoTracking(upQueId)
                        ?? throw new ArgumentNullException("Failed to fetch course status from given question.");
                if (!status.Equals(CEGConstants.COURSE_STATUS_DRAFT))
                    throw new ArgumentException("Cannot update question for course in used.");
            }

            // Map changes from the update model to the entity
            _mapper.Map(upQue, que);

            // Save to the database
            try
            {
                _unitOfWork.HomeworkQuestionRepositories.Update(que);
                // This ensures the session is updated first
                _unitOfWork.Save();
                return;
            }
            catch (Exception ex)
            {
                // Log exception (if logging is configured)
                throw new Exception("An error occurred while updating the question.", ex);
            }
        }

        public void UpdateWithHomeworkId(int questionId, int homeworkId)
        {
            var questionDefault = _unitOfWork.HomeworkQuestionRepositories.GetByIdNoTracking(questionId).Result;
            if (questionDefault == null) return;
            if (questionDefault.HomeworkId != 0)
            {
                var newQuestion = new HomeworkQuestion
                {
                    HomeworkQuestionId = 0,  // Set to 0 or default, since it's a new record
                    HomeworkId = homeworkId,  // Use the new homeworkId
                    Question = questionDefault.Question,  // Copy other properties
                                                          // Add any other properties here as needed...
                    HomeworkAnswers = questionDefault.HomeworkAnswers?.Select(answer => new HomeworkAnswer
                    {
                        HomeworkAnswerId = 0,  // Reset the answer ID to create a new one
                        Answer = answer.Answer,  // Copy the answer text
                        Type = answer.Type
                    }).ToList() ?? []  // Convert to list after projection
                };
                _unitOfWork.HomeworkQuestionRepositories.Create(newQuestion);
                _unitOfWork.Save();
            } 
            else
            {
                questionDefault.HomeworkId = homeworkId;
                _unitOfWork.HomeworkQuestionRepositories.Update(questionDefault);
                _unitOfWork.Save();
            }
        }

        public async Task Delete(int delQueId,int homId)
        {
            // Fetch the existing record
            var que = await _unitOfWork.HomeworkQuestionRepositories.GetByIdNoTracking(delQueId,homId)
                ?? throw new KeyNotFoundException("Question not found.");
            if (homId == 0) 
                throw new KeyNotFoundException("Homework Id cannot be zero.");
            var status = await _unitOfWork.CourseRepositories.GetStatusByQuestionIdNoTracking(delQueId)
                ?? throw new ArgumentNullException("Failed to fetch course status from given question.");
            if (!status.Equals(CEGConstants.COURSE_STATUS_DRAFT))
                throw new ArgumentException("Cannot delete question in used.");
            // Save to the database
            try
            {
                foreach(var answer in que.HomeworkAnswers)
                {
                    _unitOfWork.HomeworkAnswerRepositories.Delete(answer);
                }
                _unitOfWork.HomeworkQuestionRepositories.Delete(que);
                // This ensures the session is deleted first
                _unitOfWork.Save();
                return;
            }
            catch (Exception ex)
            {
                // Log exception (if logging is configured)
                throw new Exception("An error occurred while deleting the question.", ex);
            }
        }
    }
}
