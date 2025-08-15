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
    public class HomeworkAnswerService : IHomeworkAnswerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public HomeworkAnswerService(
            IUnitOfWork unitOfWork,
            IMapper mapper
            )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public void Create(HomeworkAnswerViewModel model, CreateNewAnswer newAnsw)
        {
            var answ = _mapper.Map<HomeworkAnswer>(model);
            if (newAnsw != null)
            {
                answ.Answer = newAnsw.Answer;
                answ.Type = newAnsw.Type;
                answ.HomeworkQuestionId = newAnsw.QuestionId.Value;
            }
            _unitOfWork.HomeworkAnswerRepositories.Create(answ);
            _unitOfWork.Save();
        }

        public async Task Delete(int delAnsId)
        {
            // Fetch the existing record
            var ans = await _unitOfWork.HomeworkAnswerRepositories.GetByIdNoTracking(delAnsId)
                ?? throw new KeyNotFoundException("Answer not found.");
            if (ans.HomeworkQuestionId == 0)
                throw new KeyNotFoundException("Homework question Id cannot be zero.");
            var status = await _unitOfWork.CourseRepositories.GetStatusByQuestionIdNoTracking(ans.HomeworkQuestionId)
                ?? throw new ArgumentNullException("Failed to fetch course status from given answer.");
            if (!status.Equals(CEGConstants.COURSE_STATUS_DRAFT))
                throw new ArgumentException("Cannot delete answer in used.");
            // Save to the database
            try
            {
                _unitOfWork.HomeworkAnswerRepositories.Delete(ans);
                // This ensures the session is deleted first
                _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                // Log exception (if logging is configured)
                throw new Exception("An error occurred while deleting the answer.", ex);
            }
        }

        public async Task<HomeworkAnswerViewModel?> GetById(int id)
        {
            var ans = await _unitOfWork.HomeworkAnswerRepositories.GetByIdNoTracking(id);
            return ans != null ? _mapper.Map<HomeworkAnswerViewModel>(ans) : null;
        }

        public async Task<List<HomeworkAnswerViewModel>> GetList()
        {
            return _mapper.Map<List<HomeworkAnswerViewModel>>(await _unitOfWork.HomeworkAnswerRepositories.GetList());
        }

        public async Task<List<HomeworkAnswerViewModel>?> GetListByCourseId(int courseId)
        {
            return _mapper.Map<List<HomeworkAnswerViewModel>>(await _unitOfWork.HomeworkAnswerRepositories.GetListByCourseId(courseId));
        }

        public async Task<List<HomeworkAnswerViewModel>?> GetListByQuestionId(int questionId)
        {
            return _mapper.Map<List<HomeworkAnswerViewModel>>(await _unitOfWork.HomeworkAnswerRepositories.GetListByQuestionId(questionId));
        }

        public async Task<List<HomeworkAnswerViewModel>?> GetListBySessionId(int sessionId)
        {
            return _mapper.Map<List<HomeworkAnswerViewModel>>(await _unitOfWork.HomeworkAnswerRepositories.GetListBySessionId(sessionId));
        }

        /*public void Update(HomeworkAnswerViewModel model)
        {
            var answ = _mapper.Map<HomeworkAnswer>(model);
            var answerDefault = _unitOfWork.HomeworkAnswerRepositories.GetByIdNoTracking(model.HomeworkAnswerId.Value).Result;
            answ.HomeworkQuestionId = answerDefault.HomeworkQuestionId;
            _unitOfWork.HomeworkAnswerRepositories.Update(answ);
            _unitOfWork.Save();
        }*/

        public async Task Update(int upAnsId, UpdateAnswer upAns)
        {
            if (upAns == null)
                throw new ArgumentNullException(nameof(upAns), "New answer info for updating cannot be null.");

            // Fetch the existing record
            var ans = await _unitOfWork.HomeworkAnswerRepositories.GetByIdNoTracking(upAnsId)
                ?? throw new KeyNotFoundException("Answer not found.");

            string? status = await _unitOfWork.CourseRepositories.GetStatusByQuestionIdNoTracking(ans.HomeworkQuestionId)
                ?? throw new ArgumentNullException("Failed to fetch course status from given answer.");
            if (!status.Equals(CEGConstants.COURSE_STATUS_DRAFT))
                throw new ArgumentException("Cannot update answer for course in used.");

            // Map changes from the update model to the entity
            _mapper.Map(upAns, ans);

            // Save to the database
            try
            {
                _unitOfWork.HomeworkAnswerRepositories.Update(ans);
                // This ensures the session is updated first
                _unitOfWork.Save();

                return;
            }
            catch (Exception ex)
            {
                // Log exception (if logging is configured)
                throw new Exception("An error occurred while updating the answer.", ex);
            }
        }
    }
}
