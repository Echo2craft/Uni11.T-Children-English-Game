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
    public class SessionRepositories : RepositoryBase<Session>,ISessionRepositories
    {
        private readonly MyDBContext _dbContext;
        public SessionRepositories(MyDBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Session?> GetByIdNoTracking(int id, bool includeCourse = false, bool includeHomeworks = false, bool includeQuestions = false)
        {
            return await _dbContext.Sessions
                        .AsNoTrackingWithIdentityResolution()
                        .Where(s => s.SessionId == id)
                        .Select(ses => new Session
                        {
                            SessionId = ses.SessionId,
                            Title = ses.Title,
                            Hours = ses.Hours,
                            Description = ses.Description,
                            SessionNumber = ses.SessionNumber,
                            CourseId = ses.CourseId,
                            // Include Course only if requested
                            Course = includeCourse ? ses.Course : new Course(),
                            // Include Sessions only if requested
                            Homeworks = includeHomeworks ? ses.Homeworks.Select(hom => new Homework
                            {
                                HomeworkId = hom.HomeworkId,
                                Title = hom.Title,
                                Description = hom.Description,
                                Type = hom.Type,
                                StartDate = hom.StartDate,
                                EndDate = hom.EndDate,
                                Hours = hom.Hours,
                                SessionId = hom.SessionId,
                                // Include Questions only if requested
                                HomeworkQuestions = includeQuestions ? hom.HomeworkQuestions : new List<HomeworkQuestion>()
                            }).ToList() : new List<Homework>()
                        })
                        .SingleOrDefaultAsync();
        }

        public async Task<List<Session>> GetSessionList()
        {
            return await _dbContext.Sessions.ToListAsync();
        }
        
        public async Task<Session?> GetByTitle(string name)
        {
            return await _dbContext.Sessions.Include(s => s.Homeworks).AsNoTrackingWithIdentityResolution().SingleOrDefaultAsync(sess => sess.Title == name);
        }

        public async Task<int> GetIdByTitle(string name)
        {
            var result = await (from s in _dbContext.Sessions where s.Title == name select s).FirstOrDefaultAsync();
            if (result != null) return result.SessionId;
            return 0;
        }

        public async Task<List<Session>> GetSessionListByCourseId(int courseId)
        {
            return await _dbContext.Sessions.AsNoTrackingWithIdentityResolution().Where(sess => sess.CourseId == courseId).ToListAsync();
        }

        public Task<bool> DetachSession(Session ses)
        {
            _dbContext.Sessions.Entry(ses).State = EntityState.Detached;
            _dbContext.Courses.Entry(ses.Course).State = EntityState.Detached;
            return Task.FromResult(
                _dbContext.Sessions.Entry(ses).State == EntityState.Detached
                );
        }
    }
}
