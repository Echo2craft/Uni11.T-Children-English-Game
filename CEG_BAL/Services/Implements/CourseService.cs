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
        public void Create(CourseViewModel course, CreateNewCourse newCourse)
        {
            var cou = _mapper.Map<Course>(course);
            cou.Status = "Draft";
            cou.Image = "Image";
            if(newCourse != null)
            {
                cou.CourseName = newCourse.CourseName;
                cou.CourseType = newCourse.CourseType;
                cou.Description = newCourse.Description;
                cou.Image = newCourse.Image;
                cou.TotalHours = newCourse.TotalHours;
                cou.RequiredAge = newCourse.RequiredAge;
                cou.Difficulty = newCourse.Difficulty;
                cou.Category = newCourse.Category;
            }
            _unitOfWork.CourseRepositories.Create(cou);
            _unitOfWork.Save();
        }

        public async Task<CourseViewModel?> GetCourseById(int id)
        {
            await _unitOfWork.CourseRepositories.UpdateTotalHoursByIdThroughSessionsSum(id);
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

            // Reattach entity and mark it as modified
            _unitOfWork.CourseRepositories.Update(cou);

            // Save changes
            try
            {
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

        public void UpdateStatus(int courseId, string courseStatus)
        {
            var cou = _unitOfWork.CourseRepositories.GetByIdNoTracking(courseId, includeClasses: true).Result;
            if (cou == null) return;
            cou.Status = courseStatus;
            _unitOfWork.CourseRepositories.Update(cou);
            _unitOfWork.Save();
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
