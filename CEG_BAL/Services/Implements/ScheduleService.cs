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

        public void Create(ScheduleViewModel scheduleModel, CreateNewSchedule newSchedule)
        {
            var sche = _mapper.Map<Schedule>(scheduleModel);
            if (newSchedule != null)
            {
                sche.SessionId = newSchedule.SessionId;
                sche.ClassId = newSchedule.ClassId;
                sche.ScheduleDate = newSchedule.ScheduleDate;
                sche.StartTime = newSchedule.ScheduleDate.HasValue ? TimeOnly.FromDateTime(newSchedule.ScheduleDate.Value) : default;
                sche.EndTime = sche.StartTime.Value.AddHours(_unitOfWork.SessionRepositories.GetByIdNoTracking(newSchedule.SessionId).Result.Hours.Value);
                sche.Status = Constants.SCHEDULE_STATUS_DRAFT;
            }
            _unitOfWork.ScheduleRepositories.Create(sche);
            _unitOfWork.Save();
        }

        public Task<ScheduleViewModel?> GetById(int id)
        {
            throw new NotImplementedException();
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

        public Task<List<ScheduleViewModel>> GetListByClassId(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(ScheduleViewModel scheduleModel, UpdateSchedule newSchedule)
        {
            throw new NotImplementedException();
        }

        public void UpdateStatus(int id, string status)
        {
            throw new NotImplementedException();
        }
    }
}
