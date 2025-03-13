using AutoMapper;
using CEG_BAL.Services.Interfaces;
using CEG_BAL.ViewModels;
using CEG_DAL.Infrastructure;
using Microsoft.Extensions.Configuration;
using CEG_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CEG_BAL.ViewModels.Admin;
using CEG_BAL.Configurations;
using CEG_BAL.ViewModels.Admin.Update;

namespace CEG_BAL.Services.Implements
{
    public class HomeworkService : IHomeworkService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IJWTService _jwtService;
        private readonly IConfiguration _configuration;

        public HomeworkService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IJWTService jwtServices,
            IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _jwtService = jwtServices;
            _configuration = configuration;
        }
        public async Task Create(CreateNewHomework newHom)
        {
            if (newHom == null)
                throw new ArgumentNullException(nameof(newHom), "The new homework info cannot be null.");

            var ses = await _unitOfWork.SessionRepositories.GetByIdNoTracking(newHom.SessionId, includeCourse: true)
                ?? throw new ArgumentNullException(nameof(newHom), "The new homework info contains invalid session id: session not found.");
            if (ses.Course.Status == null)
                throw new ArgumentNullException("Failed to fetch course status from given homework with given session ID.");
            if (!ses.Course.Status.Equals(CEGConstants.COURSE_STATUS_DRAFT))
                throw new ArgumentException("Cannot create new homework for course in used.");

            var hom = _mapper.Map<Homework>(newHom);

            // Save to the database
            try
            {
                _unitOfWork.HomeworkRepositories.Create(hom);
                // This ensures the session is created first
                _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                // Log exception (if logging is configured)
                throw new Exception("An error occurred while creating the homework.", ex);
            }
        }

        public async Task<List<HomeworkViewModel>> GetHomeworkList()
        {
            return _mapper.Map<List<HomeworkViewModel>>(await _unitOfWork.HomeworkRepositories.GetHomeworksList());
        }

        public async Task<HomeworkViewModel?> GetHomeworkById(int id)
        {
            var user = await _unitOfWork.HomeworkRepositories.GetByIdNoTracking(id);
            if(user != null)
            {
                var urs = _mapper.Map<HomeworkViewModel>(user);
                urs.CourseStatus = await _unitOfWork.CourseRepositories.GetStatusByHomeworkIdNoTracking(id);
                return urs;
            }
            return null;
        }

        /*public void Update(HomeworkViewModel model)
        {
            var home = _mapper.Map<Homework>(model);
            var homeDefault = _unitOfWork.HomeworkRepositories.GetByIdNoTracking(model.HomeworkId.Value).Result;
            //home.Status = homeDefault.Status;
            home.SessionId = homeDefault.SessionId;
            _unitOfWork.HomeworkRepositories.Update(home);
            _unitOfWork.Save();
        }*/

        public async Task Update(int upHomId, UpdateHomework upHom)
        {
            if (upHom == null)
                throw new ArgumentNullException(nameof(upHom), "New homework info for updating cannot be null.");

            // Fetch the existing record
            var hom = await _unitOfWork.HomeworkRepositories.GetByIdNoTracking(upHomId)
                ?? throw new KeyNotFoundException("Homework not found.");

            string? status = await _unitOfWork.CourseRepositories.GetStatusByHomeworkIdNoTracking(upHomId)
                ?? throw new ArgumentNullException("Failed to fetch course status from given homework.");
            if (!status.Equals(CEGConstants.COURSE_STATUS_DRAFT))
                throw new ArgumentException("Cannot update session for course in used.");

            // Map changes from the update model to the entity
            _mapper.Map(upHom, hom);
            hom.Session = null;
            // Save to the database
            try
            {
                _unitOfWork.HomeworkRepositories.Update(hom);
                // This ensures the session is updated first
                _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                // Log exception (if logging is configured)
                throw new Exception("An error occurred while updating the homework.", ex);
            }
        }

        public async Task<bool> IsHomeworkExistByTitle(string title)
        {
            return (await _unitOfWork.HomeworkRepositories.GetByTitle(title)) != null;
        }

        public async Task Delete(int delHomId)
        {
            // Fetch the existing record
            var hom = await _unitOfWork.HomeworkRepositories.GetByIdNoTracking(delHomId, includeCourse: true)
                ?? throw new KeyNotFoundException("Homework not found.");
            if (hom.Session.Course.Status == null)
                throw new ArgumentNullException("Failed to fetch course status from given homework.");
            if (!hom.Session.Course.Status.Equals(CEGConstants.COURSE_STATUS_DRAFT))
                throw new ArgumentException("Cannot delete homework in used.");
            // Save to the database
            try
            {
                hom.Session = null;
                _unitOfWork.HomeworkRepositories.Delete(hom);
                // This ensures the session is deleted first
                _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                // Log exception (if logging is configured)
                throw new Exception("An error occurred while deleting the homework.", ex);
            }
        }
    }
}
