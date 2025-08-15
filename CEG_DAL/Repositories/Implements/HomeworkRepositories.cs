using CEG_DAL.Infrastructure;
using CEG_DAL.Models;
using CEG_DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_DAL.Repositories.Implements
{
    public class HomeworkRepositories : RepositoryBase<Homework>, IHomeworkRepositories
    {
        private readonly MyDBContext _dbContext;
        public HomeworkRepositories(MyDBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Homework?> GetByIdNoTracking(int id, bool includeSession = false, bool includeCourse = false, bool includeQuestions = false, bool includeAnswers = false)
        {
            return await _dbContext.Homeworks
                .AsNoTrackingWithIdentityResolution()
                .Where(h => h.HomeworkId == id)
                .Select(hom =>  new Homework()
                {
                    HomeworkId = hom.HomeworkId,
                    Title = hom.Title,
                    Description = hom.Description,
                    Hours = hom.Hours,
                    Type = hom.Type,
                    GameConfigId = hom.GameConfigId,
                    SessionId = hom.SessionId,
                    StartDate = hom.StartDate,
                    EndDate = hom.EndDate,
                    // Include Session only if requested
                    Session = new Session()
                    {
                        SessionId = includeSession ? hom.Session.SessionId : 0,
                        CourseId = includeSession ? hom.Session.CourseId : 0,
                        SessionNumber = includeSession ? hom.Session.SessionNumber : 0,
                        Title = includeSession ? hom.Session.Title : "",
                        Description = includeSession ? hom.Session.Description : "",
                        Hours = includeSession ? hom.Session.Hours : 0,

                        Course = includeCourse ? hom.Session.Course : new Course()
                    },
                    GameConfig = hom.GameConfig,
                    HomeworkQuestions = includeQuestions ? hom.HomeworkQuestions.Select(que => new HomeworkQuestion()
                    {
                        HomeworkQuestionId = que.HomeworkQuestionId,
                        Question = que.Question,
                        HomeworkId = que.HomeworkId,
                        HomeworkAnswers = includeAnswers ? que.HomeworkAnswers.Select(ans => new HomeworkAnswer()
                        {
                            HomeworkAnswerId = ans.HomeworkAnswerId,
                            Answer = ans.Answer,
                            Type = ans.Type,
                            HomeworkQuestionId = ans.HomeworkQuestionId
                        }).ToList() : new List<HomeworkAnswer>()
                    }).ToList() : new List<HomeworkQuestion>()
                })
                .SingleOrDefaultAsync();
        }

        public async Task<List<Homework>> GetHomeworksList()
        {
            return await _dbContext.Homeworks.ToListAsync();
        }

        public async Task<int> GetIdByTitle(string name)
        {
            var result = await (from s in _dbContext.Homeworks where s.Title == name select s).FirstOrDefaultAsync();
            if (result != null) return result.HomeworkId;
            return 0;
        }

        public async Task<List<Homework>?> GetListBySessionId(int sessionId)
        {
            return await _dbContext.Homeworks
                .AsNoTrackingWithIdentityResolution()
                .Where(home => home.SessionId == sessionId)
                .ToListAsync();
        }

        public async Task<List<Homework>> GetListBySessionIds(int[] sesId)
        {
            return await _dbContext.Homeworks
                .AsNoTrackingWithIdentityResolution()
                .Where(h => sesId.Contains(h.SessionId))
                .ToListAsync();
        }

        public async Task<List<int>> GetIdListByScheduleId(int schId)
        {
            return await _dbContext.Homeworks
                .AsNoTrackingWithIdentityResolution()
                .Where(home => home.Session.Schedules.Any(sch => sch.ScheduleId == schId))
                .Select(hom => hom.HomeworkId)
                .ToListAsync();
        }

        public async Task<Homework?> GetByTitle(string name)
        {
            return await _dbContext.Homeworks
                .Select(h => new Homework()
                {
                    HomeworkId = h.HomeworkId,
                    Title = h.Title,
                    Description = h.Description,
                    StartDate = h.StartDate,
                    EndDate = h.EndDate,
                    GameConfigId = h.GameConfigId,
                    Hours = h.Hours,
                    Type = h.Type,
                    HomeworkQuestions = h.HomeworkQuestions,
                })
                .AsNoTrackingWithIdentityResolution()
                .SingleOrDefaultAsync(home => home.Title == name);
        }
    }
}
