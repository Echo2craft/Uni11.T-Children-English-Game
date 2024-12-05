using AutoMapper;
using CEG_BAL.Configurations;
using CEG_BAL.Services.Interfaces;
using CEG_BAL.ViewModels;
using CEG_BAL.ViewModels.Admin;
using CEG_BAL.ViewModels.Admin.Update;
using CEG_DAL.Infrastructure;
using CEG_DAL.Models;
using Microsoft.Extensions.Configuration;

namespace CEG_BAL.Services.Implements
{
    public class ScheduleService : IScheduleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public ScheduleService(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task Create(CreateNewSchedule newSchedule)
        {
            if (newSchedule == null)
                throw new ArgumentNullException(nameof(newSchedule), "The new schedule info cannot be null.");

            var sch = new Schedule
            {
                Status = Constants.SCHEDULE_STATUS_DRAFT
            };
            _mapper.Map(newSchedule, sch);
            sch.StartTime = newSchedule.ScheduleDate.HasValue ? TimeOnly.FromDateTime(newSchedule.ScheduleDate.Value) : default;
            sch.EndTime = sch.StartTime.Value.AddHours((await _unitOfWork.SessionRepositories.GetByIdNoTracking(newSchedule.SessionId)).Hours.Value);

            // Save to the database
            try
            {
                _unitOfWork.ScheduleRepositories.Create(sch);
                _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                // Log exception (if logging is configured)
                throw new Exception("An error occurred while creating the schedule.", ex);
            }
        }

        public async Task<ScheduleViewModel?> GetById(int id)
        {
            var sche = await _unitOfWork.ScheduleRepositories.GetByIdNoTracking(id);
            if (sche != null)
            {
                var sch = _mapper.Map<ScheduleViewModel>(sche);
                return sch;
            }
            return null;
        }

        public Task<ScheduleViewModel?> GetByIdAdmin(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<ScheduleViewModel>> GetList()
        {
            throw new NotImplementedException();
        }

        public Task<List<ScheduleViewModel>> GetListAdmin()
        {
            throw new NotImplementedException();
        }

        public async Task<List<ScheduleViewModel>> GetListByClassId(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(ScheduleViewModel scheduleModel, UpdateSchedule newSchedule)
        {
            var mainSchedule = _mapper.Map<Schedule>(scheduleModel);
            if (newSchedule != null)
            {
                // mainSchedule.TeacherId = _unitOfWork.TeacherRepositories.GetByFullname(classNewModel.TeacherName).Result.TeacherId;
                mainSchedule.ScheduleDate = newSchedule.ScheduleDate;
                mainSchedule.StartTime = newSchedule.ScheduleDate.HasValue ? TimeOnly.FromDateTime(newSchedule.ScheduleDate.Value) : default;
                mainSchedule.EndTime = mainSchedule.StartTime.Value.AddHours(_unitOfWork.SessionRepositories.GetByIdNoTracking(mainSchedule.SessionId).Result.Hours.Value);
            }
            mainSchedule.Class = null;
            mainSchedule.Session = null;
            _unitOfWork.ScheduleRepositories.Update(mainSchedule);
            _unitOfWork.Save();
        }

        public void UpdateStatus(int id, string status)
        {
            var sche = _unitOfWork.ScheduleRepositories.GetByIdNoTracking(id).Result;
            if (sche == null) return;
            sche.Status = status;
            _unitOfWork.ScheduleRepositories.Update(sche);
            _unitOfWork.Save();
        }
    }
}
