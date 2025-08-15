﻿using CEG_DAL.Infrastructure;
using CEG_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_DAL.Repositories.Interfaces
{
    public interface IScheduleRepositories : IRepositoryBase<Schedule>
    {
        Task<List<Schedule>?> GetListByClassId(int classId);
        Task CreateBySessionId(int sessionId);
        Task<Schedule?> GetByIdNoTracking(int id);
        Task<bool> IsListScheduleDateValidByClassId(int claId);
        Task<List<string>> IsListScheduleDateHasValidSequenceByClassId(int claId);
        Task UpdateStatusAsync(int id, string status);
    }
}
