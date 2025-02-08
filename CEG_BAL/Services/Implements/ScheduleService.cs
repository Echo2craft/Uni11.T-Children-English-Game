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
                Status = CEGConstants.SCHEDULE_STATUS_DRAFT
            };
            _mapper.Map(newSch, sch);

            var ses = await _unitOfWork.SessionRepositories.GetByIdNoTracking(newSch.SessionId);

            if (ses == null)
                throw new ArgumentNullException(nameof(newSch), "The new schedule info contains invalid session: session info is null.");
            if (ses.Hours == null)
                throw new ArgumentNullException(nameof(newSch), "The new schedule info contains invalid session: session hours is null.");
            if (sch.StartTime == null)
                throw new ArgumentNullException(nameof(newSch), "The new schedule info contains invalid data: schedule start time is null.");

            sch.EndTime = sch.StartTime.Value.AddHours(ses.Hours.Value);

            try
            {
                _unitOfWork.ScheduleRepositories.Create(sch);
                // Save to the database
                _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                // Log exception (if logging is configured)
                throw new Exception("An error occurred while creating the schedule.", ex);
            }
        }

        public async Task Delete(int delSchId)
        {
            // Fetch the existing record
            var sch = await _unitOfWork.ScheduleRepositories.GetByIdNoTracking(delSchId)
                ?? throw new KeyNotFoundException("Schedule not found.");
            if (sch.Class.Status == null)
                throw new ArgumentNullException("Failed to fetch class status from given scheduled session.");
            if (!sch.Class.Status.Equals(CEGConstants.COURSE_STATUS_DRAFT))
                throw new ArgumentException("Cannot delete scheduled session in used.");

            // Save to the database
            try
            {
                _unitOfWork.ScheduleRepositories.Delete(sch);
                // This ensures the session is deleted first
                _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                // Log exception (if logging is configured)
                throw new Exception("An error occurred while deleting scheduled session.", ex);
            }
        }

        public async Task<ScheduleViewModel?> GetById(int id)
        {
            var sche = await _unitOfWork.ScheduleRepositories.GetByIdNoTracking(id);
            return sche != null ? _mapper.Map<ScheduleViewModel>(sche) : null;
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

        public async Task Update(int upSchId, UpdateSchedule upSch)
        {
            if (upSch == null)
                throw new ArgumentNullException(nameof(upSch), "New schedule info for updating cannot be null.");

            // Fetch the existing record
            var sch = await _unitOfWork.ScheduleRepositories.GetByIdNoTracking(upSchId)
                ?? throw new KeyNotFoundException("Schedule not found.");

            // Map changes from the update model to the entity
            _mapper.Map(upSch, sch);
            if (sch.StartTime == null)
                throw new ArgumentNullException(nameof(upSch), "The new schedule info contains invalid data: schedule start time is null.");
            var ses = await _unitOfWork.SessionRepositories.GetByIdNoTracking(sch.SessionId);

            if (ses == null)
                throw new ArgumentNullException(nameof(upSch), "(Update) The new schedule info contains invalid session: session info is null.");
            if (ses.Hours == null)
                throw new ArgumentNullException(nameof(upSch), "(Update) The new schedule info contains invalid session: session hours is null.");
            if (sch.StartTime == null)
                throw new ArgumentNullException(nameof(upSch), "(Update) The new schedule info contains invalid data: schedule start time is null.");

            sch.EndTime = sch.StartTime.Value.AddHours(ses.Hours.Value);
            /*sch.EndTime = sch.StartTime.Value.AddHours((await _unitOfWork.SessionRepositories.GetByIdNoTracking(sch.SessionId)).Hours.Value);*/

            sch.Class = null;
            sch.Session = null;

            try
            {
                // Reattach entity and mark it as modified
                _unitOfWork.ScheduleRepositories.Update(sch);
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
                throw new Exception("An unexpected error occurred while updating the schedule.", ex);
            }
        }

        public async Task UpdateStatus(int schId, string upSchStatus)
        {
            // Fetch the existing record
            var sch = await _unitOfWork.ScheduleRepositories.GetByIdNoTracking(schId)
                ?? throw new KeyNotFoundException("Schedule not found.");

            sch.Class = null;
            sch.Session = null;

            sch.Status = upSchStatus;
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
                throw new Exception("An unexpected error occurred while updating the class.", ex);
            }
        }
    }
}
