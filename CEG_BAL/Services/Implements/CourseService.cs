using AutoMapper;
using CEG_BAL.Services.Interfaces;
using CEG_BAL.ViewModels;
using CEG_DAL.Infrastructure;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CEG_DAL.Models;
using CEG_BAL.ViewModels.Admin;
using CEG_BAL.Configurations;
using Microsoft.EntityFrameworkCore;
using CEG_BAL.ViewModels.Admin.Update;
using Microsoft.IdentityModel.Tokens;

namespace CEG_BAL.Services.Implements
{
    public class CourseService : ICourseService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IJWTService _jwtService;
        private readonly IConfiguration _configuration;

        public CourseService(
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
        public async Task Create(CreateNewCourse newCou)
        {
            if (newCou == null)
                throw new ArgumentNullException(nameof(newCou), "The new course info cannot be null.");

            var cou = new Course
            {
                Image = "Placeholder Image",
                Status = CEGConstants.COURSE_STATUS_DRAFT
            };
            _mapper.Map(newCou, cou);

            // Save to the database
            try
            {
                _unitOfWork.CourseRepositories.Create(cou);
                _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                // Log exception (if logging is configured)
                throw new Exception("An error occurred while creating new course.", ex);
            }
        }

        public async Task<CourseViewModel?> GetByIdNoTracking(int id)
        {
            // await _unitOfWork.CourseRepositories.UpdateTotalHoursByIdThroughSessionsSum(id);
            var cou = await _unitOfWork.CourseRepositories.GetByIdNoTracking(id, true, true ,true);
            return cou != null ? _mapper.Map<CourseViewModel>(cou) : null;
        }

        public async Task<List<CourseViewModel>> GetCourseList()
        {
            return _mapper.Map<List<CourseViewModel>>(await _unitOfWork.CourseRepositories.GetList());
        }
        public async Task<List<CourseViewModel>?> GetListByStatus(string status)
        {
            return _mapper.Map<List<CourseViewModel>>(await _unitOfWork.CourseRepositories.GetListByStatus(status));
        }

        public async Task<List<string>> GetCourseNameList()
        {
            return await _unitOfWork.CourseRepositories.GetNameList();
        }

        public async Task<List<string>> GetCourseNameByStatusList(string status)
        {
            return await _unitOfWork.CourseRepositories.GetNameListByStatus(status);
        }

        public async Task Update(int couId, UpdateCourse upCou)
        {
            if (upCou == null)
                throw new ArgumentNullException(nameof(upCou), "update course info cannot be null.");

            // Fetch the existing record
            var cou = await _unitOfWork.CourseRepositories.GetByIdNoTracking(couId)
                ?? throw new KeyNotFoundException("Course not found.");

            // Map changes from the update model to the entity
            _mapper.Map(upCou, cou);

            try
            {
                // Reattach entity and mark it as modified
                _unitOfWork.CourseRepositories.Update(cou);
                // Save changes
                _unitOfWork.Save();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                // Handle concurrency issues (e.g., row modified by another user)
                throw new InvalidOperationException("Update failed due to a concurrency conflict.", ex);
            }
            catch (Exception ex)
            {
                // Log and rethrow unexpected exceptions
                throw new Exception("An unexpected error occurred while updating the course.", ex);
            }
        }

        public async Task UpdateStatus(int couId, string courseStatus)
        {
            if (courseStatus.IsNullOrEmpty())
                throw new ArgumentNullException(nameof(courseStatus), "update course status text cannot be null.");

            // Fetch the existing record
            _ = await _unitOfWork.CourseRepositories.GetByIdNoTracking(couId)
                ?? throw new KeyNotFoundException("Course not found.");

            try
            {
                // Save changes
                await _unitOfWork.CourseRepositories.UpdateStatusAsync(couId, courseStatus);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                // Handle concurrency issues (e.g., row modified by another user)
                throw new InvalidOperationException("Update failed due to a concurrency conflict.", ex);
            }
            catch (Exception ex)
            {
                // Log and rethrow unexpected exceptions
                throw new Exception("An unexpected error occurred while updating course status.", ex);
            }
        }

        public async Task<bool> IsExistByName(string name)
        {
            var cou = await _unitOfWork.CourseRepositories.GetByName(name);
            return cou != null;
        }

        public async Task<bool> IsAvailableByName(string name)
        {
            var cou = await _unitOfWork.CourseRepositories.GetByName(name);
            return cou != null && cou.Status.Equals(CEGConstants.COURSE_STATUS_AVAILABLE);
        }

        public async Task<bool> IsExistById(int id)
        {
            var cou = await _unitOfWork.CourseRepositories.GetByIdNoTracking(id);
            return cou != null;
        }

        public async Task<bool> IsAvailableById(int id)
        {
            var cou = await _unitOfWork.CourseRepositories.GetByIdNoTracking(id);
            return cou != null && cou.Status.Equals(CEGConstants.COURSE_STATUS_AVAILABLE);
        }

        public async Task<int> GetTotalAmount()
        {
            return await _unitOfWork.CourseRepositories.GetTotalAmount();
        }
    }
}
