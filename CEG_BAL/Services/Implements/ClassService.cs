using AutoMapper;
using CEG_BAL.Configurations;
using CEG_BAL.Services.Interfaces;
using CEG_BAL.ViewModels;
using CEG_BAL.ViewModels.Admin;
using CEG_BAL.ViewModels.Admin.Get;
using CEG_BAL.ViewModels.Admin.Update;
using CEG_DAL.Infrastructure;
using CEG_DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics.CodeAnalysis;

namespace CEG_BAL.Services.Implements
{
    public class ClassService : IClassService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IJWTService _jwtService;
        private readonly IConfiguration _configuration;

        public ClassService(
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

        public async Task Create(CreateNewClass newCla)
        {
            if (newCla == null)
                throw new ArgumentNullException(nameof(newCla), "The new class info cannot be null.");

            var cla = new Class
            {
                Status = CEGConstants.CLASS_STATUS_DRAFT
            };
            _mapper.Map(newCla, cla);
            foreach(var schedule in cla.Schedules)
            {
                schedule.EndTime = schedule.StartTime.Value.AddHours((await _unitOfWork.SessionRepositories.GetByIdNoTracking(schedule.SessionId)).Hours.Value);
            }

            // Save to the database
            try
            {
                _unitOfWork.ClassRepositories.Create(cla);
                _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                // Log exception (if logging is configured)
                throw new Exception("An error occurred while creating the class info.", ex);
            }
        }
        /// <summary>
        /// Get Class Info by Class id. 
        /// int id, class id to be use to query. 
        /// bool includeTeacher = true, determine whether if the query should include teacher info. 
        /// bool includeCourse = true, determine whether if the query should include course info. 
        /// bool includeSession = false, determine whether if the query should include course's sessions info. 
        /// bool filterSession = false, determine whether if the query should include filter session infos to only contain unscheduled session. 
        /// </summary>
        /// <param name="id">Class id</param>
        /// <param name="includeTeacher">Default: false, determine whether if the query should include teacher info</param>
        /// <param name="includeCourse">Default: false, determine whether if the query should include course info</param>
        /// <param name="includeSession">Default: false, determine whether if the query should include course's sessions info</param>
        /// <param name="filterSession">Default: false, determine whether if the query should include filter session infos to only contain unscheduled session</param>
        /// <param name="includeSchedule">Default: false, determine whether if the query should include class's schedules info</param>
        /// <param name="includeAttendances">Default: false, determine whether if the query should include class's schedules attendances info</param>
        public async Task<ClassViewModel?> GetById(
            int id, 
            bool includeTeacher = true, 
            bool includeCourse = true,
            bool includeSession = false, 
            bool filterSession = false,
            bool includeSchedule = true,
            bool includeAttendances = true
        )
        {
            var clas = await _unitOfWork.ClassRepositories.GetByIdNoTracking(
                id, 
                includeTeacher: includeTeacher, 
                includeCourse: includeCourse, 
                includeSession: includeSession, 
                filterSession: filterSession,
                includeSchedule: includeSchedule,
                includeAttendances: includeAttendances
            );
            if (clas == null) return null;
            var viewCla = _mapper.Map<ClassViewModel>(clas);
            if(viewCla.Schedules != null && viewCla.Schedules.Count > 0)
            {
                for (int i = 0; i < viewCla.Schedules.Count; i++)
                {
                    viewCla.Schedules[i].ScheduleNumber = i + 1;
                }
            }
            return viewCla;
        }

        public async Task<ClassViewModel?> GetByIdParent(int id)
        {
            var clas = await _unitOfWork.ClassRepositories.GetByIdNoTracking(id, true, true, true, true);
            if (clas != null)
            {
                var c = _mapper.Map<ClassViewModel>(clas);
                return c;
            }
            return null;
        }

        public async Task<ClassViewModel?> GetByClassName(string className)
        {
            var clas = await _unitOfWork.ClassRepositories.GetByClassName(className);
            return clas != null ? _mapper.Map<ClassViewModel>(clas) : null;
        }

        public async Task<List<ClassViewModel>> GetClassList()
        {
            return _mapper.Map<List<ClassViewModel>>(await _unitOfWork.ClassRepositories.GetList());
        }

        public async Task<List<GetClassForTransaction>> GetOptionListByStatusOpen(string filterClassByStudentName = "")
        {
            return _mapper.Map<List<GetClassForTransaction>>(await _unitOfWork.ClassRepositories.GetOptionListByStatusOpen(filterClassByStudentName));
        }

        public async Task<List<ClassViewModel>> GetClassListParent()
        {
            return _mapper.Map<List<ClassViewModel>>(await _unitOfWork.ClassRepositories.GetClassListParent());
        }

        public async Task<List<ClassViewModel>> GetListByTeacherAccountId(int id)
        {
            var teacherId = await _unitOfWork.TeacherRepositories.GetIdByAccountId(id);
            if (teacherId == 0) return new List<ClassViewModel>();
            return _mapper.Map<List<ClassViewModel>>(await _unitOfWork.ClassRepositories.GetListByTeacherId(teacherId));
        }
        
        public async Task<List<ClassViewModel>> GetListByStudentAccountId(int id)
        {
            var studentId = await _unitOfWork.StudentRepositories.GetIdByAccountIdNoTracking(id);
            if (studentId == 0) return new List<ClassViewModel>();
            return _mapper.Map<List<ClassViewModel>>(await _unitOfWork.ClassRepositories.GetListByStudentId(studentId.Value));
            //return _mapper.Map<List<ClassViewModel>>(await _unitOfWork.ClassRepositories.GetListByStudentId(studentId));
        }
        public async Task Update(int claId, UpdateClass upCla)
        {
            if (upCla == null)
                throw new ArgumentNullException(nameof(upCla), "update class info cannot be null.");

            // Fetch the existing record
            var cla = await _unitOfWork.ClassRepositories.GetByIdNoTracking(claId)
                ?? throw new KeyNotFoundException("Class not found.");

            // Map changes from the update model to the entity
            _mapper.Map(upCla, cla);

            // Reattach entity and mark it as modified
            _unitOfWork.ClassRepositories.Update(cla);

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
                throw new Exception("An unexpected error occurred while updating the class.", ex);
            }
        }

        public async Task UpdateStatus(int claId, string upClaStatus)
        {
            // Fetch the existing record
            var cla = await _unitOfWork.ClassRepositories.GetByIdNoTracking(claId, includeSchedule: true)
                ?? throw new KeyNotFoundException("Class not found.");
            // check is schedule date valid
            var errorList = await _unitOfWork.ScheduleRepositories.IsListScheduleDateHasValidSequenceByClassId(claId);
            if (errorList.Count > 0)
            {
                var errorMessage = "Class contain invalid schedule date: \n";
                foreach (var error in errorList) errorMessage += error;
                throw new ArgumentException(errorMessage);
            }
            if (upClaStatus == CEGConstants.CLASS_STATUS_OPEN)
            {
                foreach (var sche in cla.Schedules)
                {
                    sche.Status = CEGConstants.SCHEDULE_STATUS_UPCOMING;
                    _unitOfWork.ScheduleRepositories.Update(sche);
                }
            }

            if (upClaStatus == CEGConstants.CLASS_STATUS_ONGOING)
            {
                foreach (var sche in cla.Schedules)
                {
                    foreach(var enr in cla.Enrolls)
                    {
                        /*sche.Attendances.Add(new Attendance()
                        {
                            StudentId = enr.StudentId,
                            ScheduleId = sche.ScheduleId,
                            HasAttended = CEGConstants.ATTENDANCE_STATUS_ABSENT,
                        });*/

                        sche.Attendances.Add(new Attendance()
                        {
                            StudentId = enr.StudentId,
                            ScheduleId = sche.ScheduleId,
                            HasAttended = CEGConstants.ATTENDANCE_STATUS_ABSENT,
                        });
                        enr.Student = null;

                        // Add attendance without setting the Schedule navigation property
                        /*var attendance = new Attendance()
                        {
                            StudentId = enr.StudentId,
                            ScheduleId = sche.ScheduleId, // Set only the foreign key
                            HasAttended = CEGConstants.ATTENDANCE_STATUS_ABSENT
                        };*/
                        // Add attendance directly to the database or to the collection
                        //_unitOfWork.AttendanceRepositories.Create(attendance);
                    }
                    // _unitOfWork.ScheduleRepositories.Update(sche);
                }
            }

            cla.Status = upClaStatus;
            // Reattach entity and mark it as modified
            _unitOfWork.ClassRepositories.Update(cla);

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
                throw new Exception("An unexpected error occurred while updating the class.", ex);
            }
        }

        public async Task<bool> IsEditableById(int id)
        {
            var clas = await _unitOfWork.ClassRepositories.GetByIdNoTracking(id);
            return clas != null && (clas.Status.Equals(CEGConstants.CLASS_STATUS_DRAFT) || clas.Status.Equals(CEGConstants.CLASS_STATUS_POSTPONED));
        }

        public async Task<int> GetTotalAmount()
        {
            return await _unitOfWork.ClassRepositories.GetTotalAmount();
        }

        public async Task<int> GetTotalAmountByTeacherAccountId(int id)
        {
            var teacherId = await _unitOfWork.TeacherRepositories.GetIdByAccountId(id);
            if (teacherId == 0) return 0;
            return await _unitOfWork.ClassRepositories.GetTotalAmountByTeacherId(teacherId);
        }
    }
}
