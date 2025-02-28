using AutoMapper;
using CEG_BAL.Configurations;
using CEG_BAL.Services.Interfaces;
using CEG_BAL.ViewModels;
using CEG_BAL.ViewModels.Admin;
using CEG_BAL.ViewModels.Admin.Update;
using CEG_DAL.Infrastructure;
using CEG_DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_BAL.Services.Implements
{
    public class SessionService : ISessionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IServiceProvider _serviceProvider;

        public SessionService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IConfiguration configuration,
            IServiceProvider serviceProvider)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _configuration = configuration;
            _serviceProvider = serviceProvider;
        }
        public async Task Create(CreateNewSession newSes)
        {
            if (newSes == null)
                throw new ArgumentNullException(nameof(newSes), "The new session info cannot be null.");

            var cou = await _unitOfWork.CourseRepositories.GetByIdNoTracking(newSes.CourseId) 
                ?? throw new ArgumentNullException(nameof(newSes), "The new session info contains invalid course id: course not found.");

            if (cou.Status == null)
                throw new ArgumentNullException("Failed to fetch course status from given session.");
            if (!cou.Status.Equals(CEGConstants.COURSE_STATUS_DRAFT))
                throw new ArgumentException("Cannot create new session for course in used.");

            var ses = _mapper.Map<Session>(newSes);

            // Save to the database
            try
            {
                _unitOfWork.SessionRepositories.Create(ses);
                // This ensures the session is created first
                _unitOfWork.Save();

                await _unitOfWork.CourseRepositories.UpdateTotalHoursByIdThroughSessionsSum(ses.CourseId);
                return;
            }
            catch (Exception ex)
            {
                // Log exception (if logging is configured)
                throw new Exception("An error occurred while creating the session.", ex);
            }
        }

        public async Task<List<SessionViewModel>> GetSessionList()
        {
            return _mapper.Map<List<SessionViewModel>>(await _unitOfWork.SessionRepositories.GetSessionList());
        }

        public async Task<SessionViewModel?> GetSessionById(int id)
        {
            var ses = await _unitOfWork.SessionRepositories.GetByIdNoTracking(id);
            if (ses != null)
            {
                var sesVM = _mapper.Map<SessionViewModel>(ses);
                sesVM.CourseStatus = await _unitOfWork.CourseRepositories.GetStatusBySessionIdNoTracking(id);
                return sesVM;
            }
            return null;
        }

        public async Task Update(int upSesId, UpdateSession upSes)
        {
            if (upSes == null)
                throw new ArgumentNullException(nameof(upSes), "New session info for updating cannot be null.");

            // Fetch the existing record
            var ses = await _unitOfWork.SessionRepositories.GetByIdNoTracking(upSesId)
                ?? throw new KeyNotFoundException("Session not found.");
            
            string? status = await _unitOfWork.CourseRepositories.GetStatusBySessionIdNoTracking(upSesId)
                ?? throw new ArgumentNullException("Failed to fetch course status from given session.");
            if (!status.Equals(CEGConstants.COURSE_STATUS_DRAFT))
                throw new ArgumentException("Cannot update session for course in used.");

            // Map changes from the update model to the entity
            _mapper.Map(upSes, ses);

            // Save to the database
            try
            {
                _unitOfWork.SessionRepositories.Update(ses);
                // This ensures the session is updated first
                _unitOfWork.Save();

                await _unitOfWork.CourseRepositories.UpdateTotalHoursByIdThroughSessionsSum(ses.CourseId);
                return;
            }
            catch (Exception ex)
            {
                // Log exception (if logging is configured)
                throw new Exception("An error occurred while updating the session.", ex);
            }
        }

        public async Task Delete(int delSesId)
        {
            // Fetch the existing record
            var ses = await _unitOfWork.SessionRepositories.GetByIdNoTracking(delSesId, includeCourse: true)
                ?? throw new KeyNotFoundException("Session not found.");
            if(ses.Course.Status == null) 
                throw new ArgumentNullException("Failed to fetch course status from given session.");
            if (!ses.Course.Status.Equals(CEGConstants.COURSE_STATUS_DRAFT)) 
                throw new ArgumentException("Cannot delete session in used.");

            // Save to the database
            try
            {
                _unitOfWork.SessionRepositories.Delete(ses);
                // This ensures the session is deleted first
                _unitOfWork.Save();

                await _unitOfWork.CourseRepositories.UpdateTotalHoursByIdThroughSessionsSum(ses.CourseId);
                return;
            }
            catch (Exception ex)
            {
                // Log exception (if logging is configured)
                throw new Exception("An error occurred while deleting the session.", ex);
            }
        }

        public async Task<bool> IsSessionExistByTitle(string title)
        {
            return (await _unitOfWork.SessionRepositories.GetByTitle(title)) != null;
        }

        public async Task<List<SessionViewModel>> GetSessionListByCourseId(int courseId)
        {
            return _mapper.Map<List<SessionViewModel>>(await _unitOfWork.SessionRepositories.GetSessionListByCourseId(courseId));
        }
    }
}
