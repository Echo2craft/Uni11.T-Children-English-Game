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
    public class SessionService : ISessionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public SessionService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _configuration = configuration;
        }
        public async Task Create(CreateNewSession newSes)
        {
            if (newSes == null)
                throw new ArgumentNullException(nameof(newSes), "The new session info cannot be null.");

            var ses = new Session();
            _mapper.Map(newSes, ses);

            var cou = await _unitOfWork.CourseRepositories.GetByIdNoTracking(newSes.CourseId, includeSessions: true);
            if (cou == null) 
                throw new ArgumentNullException(nameof(cou), "The new session info contains invalid course id: course not found.");

            // ses.SessionNumber = cou.Sessions.Count + 1;
            // var sess = _mapper.Map<Session>(model);
            //sess.Status = "Draft";

            /*if (newSes != null)
            {
                sess.Title = newSes.Title;
                sess.Description = newSes.Description;
                sess.Hours = newSes.Hours;
                sess.SessionNumber = newSes.Number;
                sess.CourseId = newSes.CourseId.Value;
                sess.Course = null;
            }*/

            // Save to the database
            try
            {
                _unitOfWork.SessionRepositories.Create(ses);
                // _unitOfWork.Save();
                await _unitOfWork.CourseRepositories.UpdateTotalHoursByIdThroughSessionsSum(ses.CourseId);
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
            var user = await _unitOfWork.SessionRepositories.GetByIdNoTracking(id);
            if (user != null)
            {
                var urs = _mapper.Map<SessionViewModel>(user);
                urs.CourseStatus = await _unitOfWork.CourseRepositories.GetStatusBySessionIdNoTracking(id);
                return urs;
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
            
            // Map changes from the update model to the entity
            _mapper.Map(upSes, ses);

            _unitOfWork.SessionRepositories.Update(ses);

            // Save to the database
            try
            {
                // _unitOfWork.Save();
                // this function already contains Save method.
                await _unitOfWork.CourseRepositories.UpdateTotalHoursByIdThroughSessionsSum(ses.CourseId);
            }
            catch (Exception ex)
            {
                // Log exception (if logging is configured)
                throw new Exception("An error occurred while creating the session.", ex);
            }
        }

        public void Update(SessionViewModel model)
        {
            var sess = _unitOfWork.SessionRepositories.GetByIdNoTracking(model.SessionId.Value).Result;
            if(model != null)
            {
                sess.Title = model.Title;
                sess.Description = model.Description;
                sess.SessionNumber = model.SessionNumber;
                sess.Hours = model.Hours;
            }
            _unitOfWork.SessionRepositories.Update(sess);
            _unitOfWork.Save();
            _unitOfWork.CourseRepositories.UpdateTotalHoursByIdThroughSessionsSum(sess.CourseId);
        }

        public async Task Delete(int delSesId)
        {
            // Fetch the existing record
            var ses = await _unitOfWork.SessionRepositories.GetByIdNoTracking(delSesId)
                ?? throw new KeyNotFoundException("Session not found.");

            _unitOfWork.SessionRepositories.Delete(ses);

            // Save to the database
            try
            {
                _unitOfWork.Save();
                // _unitOfWork.Save();
                // this function already contains Save method.
                await _unitOfWork.CourseRepositories.UpdateTotalHoursByIdThroughSessionsSum(ses.CourseId);
            }
            catch (Exception ex)
            {
                // Log exception (if logging is configured)
                throw new Exception("An error occurred while creating the session.", ex);
            }
        }

        public async Task<bool> IsSessionExistByTitle(string title)
        {
            var ses = await _unitOfWork.SessionRepositories.GetByTitle(title);
            if (ses != null) return true;
            return false;
        }

        public async Task<List<SessionViewModel>> GetSessionListByCourseId(int courseId)
        {
            return _mapper.Map<List<SessionViewModel>>(await _unitOfWork.SessionRepositories.GetSessionListByCourseId(courseId));
        }
    }
}
