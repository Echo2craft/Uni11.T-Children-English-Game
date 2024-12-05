using AutoMapper;
using CEG_BAL.Configurations;
using CEG_BAL.Services.Interfaces;
using CEG_BAL.ViewModels;
using CEG_BAL.ViewModels.Admin;
using CEG_BAL.ViewModels.Admin.Update;
using CEG_DAL.Infrastructure;
using CEG_DAL.Models;
using Microsoft.EntityFrameworkCore;
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

        public async Task Create(CreateNewSchedule newSch)
        {
            if (newSch == null)
                throw new ArgumentNullException(nameof(newSch), "The new schedule info cannot be null.");

            var sch = new Schedule
            {
                Status = Constants.SCHEDULE_STATUS_DRAFT
            };
            _mapper.Map(newSch, sch);
            sch.StartTime = newSch.ScheduleDate.HasValue ? TimeOnly.FromDateTime(newSch.ScheduleDate.Value) : default;
            sch.EndTime = sch.StartTime.Value.AddHours((await _unitOfWork.SessionRepositories.GetByIdNoTracking(newSch.SessionId)).Hours.Value);

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

        public async Task<List<ScheduleViewModel>> GetListByClassId(int claId)
        {
            var schLis = _mapper.Map<List<ScheduleViewModel>>(await _unitOfWork.ScheduleRepositories.GetListByClassId(claId));
            for (int i = 0; i < schLis.Count; i++)
            {
                schLis[i].ScheduleNumber = i + 1;
            }
            return schLis;
        }

        public async Task Update(int schId, UpdateSchedule upSch)
        {
            if (upSch == null)
                throw new ArgumentNullException(nameof(upSch), "New schedule cannot be null.");

            // Fetch the existing record
            var sch = await _unitOfWork.ScheduleRepositories.GetByIdNoTracking(schId)
                ?? throw new KeyNotFoundException("Schedule not found.");

            // Map changes from the update model to the entity
            _mapper.Map(upSch, sch);
            sch.StartTime = upSch.ScheduleDate.HasValue ? TimeOnly.FromDateTime(upSch.ScheduleDate.Value) : default;
            sch.EndTime = sch.StartTime.Value.AddHours((await _unitOfWork.SessionRepositories.GetByIdNoTracking(sch.SessionId)).Hours.Value);

            // Reattach entity and mark it as modified
            _unitOfWork.ScheduleRepositories.Update(sch);

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
                throw new Exception("An unexpected error occurred while updating the student progress.", ex);
            }
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
