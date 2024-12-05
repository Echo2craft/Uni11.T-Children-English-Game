﻿using CEG_DAL.Infrastructure;
using CEG_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_DAL.Repositories.Interfaces
{
    public interface IEnrollRepositories : IRepositoryBase<Enroll>
    {
        Task<List<Enroll>> GetList();
        Task<Enroll?> GetByIdNoTracking(int id);
        // Task<bool>? isExistedByStudentFullname(string studentFullname);
        Task<List<Enroll>> GetEnrollByParentId(int parentId);
    }
}
