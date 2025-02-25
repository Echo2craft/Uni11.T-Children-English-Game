﻿using CEG_DAL.Infrastructure;
using CEG_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_DAL.Repositories.Interfaces
{
    public interface IHomeworkQuestionRepositories : IRepositoryBase<HomeworkQuestion>
    {
        Task<List<HomeworkQuestion>> GetList();
        Task<List<HomeworkQuestion?>?> GetOrderedQuestionList();
        Task<HomeworkQuestion?> GetByIdNoTracking(int id);
        Task<HomeworkQuestion?> GetByQuestion(string question);
        Task<int?> GetIdByQuestion(string question);
        Task<List<HomeworkQuestion>> GetListByHomeworkId(int homeworkId);
        Task<List<HomeworkQuestion>> GetExcludedListByHomeworkId(int homeworkId);
        Task<List<HomeworkQuestion>?> GetListBySessionId(int sessionId);
        Task<List<HomeworkQuestion>?> GetListByCourseId(int courseId);
        Task<int> GetCountByHomeworkId(int homId);
    }
}
