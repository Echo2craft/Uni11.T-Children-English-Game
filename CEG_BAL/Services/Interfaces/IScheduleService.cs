﻿using CEG_BAL.ViewModels.Admin.Update;
using CEG_BAL.ViewModels.Admin;
using CEG_BAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_BAL.Services.Interfaces
{
    public interface IScheduleService
    {
        Task Create(CreateNewSchedule newSch);
        void Update(ScheduleViewModel scheduleModel, UpdateSchedule newSchedule);
        void UpdateStatus(int id, string status);
        Task<List<ScheduleViewModel>> GetList();
        Task<List<ScheduleViewModel>> GetListAdmin();
        Task<List<ScheduleViewModel>> GetListByClassId(int id);
        Task<ScheduleViewModel?> GetById(int id);
        Task<ScheduleViewModel?> GetByIdAdmin(int id);
    }
}
