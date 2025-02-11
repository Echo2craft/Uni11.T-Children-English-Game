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
    public class CourseRepositories : RepositoryBase<Course>, ICourseRepositories
    {
        private readonly MyDBContext _dbContext;
        public CourseRepositories(MyDBContext dbContext) : base(dbContext) 
        {
            _dbContext = dbContext;
        }

        public async Task<Course?> GetByIdNoTracking(int id)
        {
            return await _dbContext.Courses
                .AsNoTrackingWithIdentityResolution()
                .SingleOrDefaultAsync(cou => cou.CourseId == id);
        }

        public async Task<Course?> GetByIdNoTracking(int id, bool includeSessions = false, bool includeClasses = false, bool includeHomeworks = false)
        {
            var query = _dbContext.Courses
                        .AsNoTrackingWithIdentityResolution()
                        .Select(c => new Course
                        {
                            CourseId = c.CourseId,
                            CourseName = c.CourseName,
                            CourseType = c.CourseType,
                            Description = c.Description,
                            Difficulty = c.Difficulty,
                            Category = c.Category,
                            Image = c.Image,
                            RequiredAge = c.RequiredAge,
                            TotalHours = c.TotalHours,
                            Status = c.Status,
                            // Include Sessions only if requested
                            Sessions = includeSessions ? c.Sessions.Select(s => new Session
                            {
                                SessionId = s.SessionId,
                                Title = s.Title,
                                Description = s.Description,
                                Hours = s.Hours,
                                SessionNumber = s.SessionNumber,
                                //Status = s.Status,
                                // Include Homeworks only if requested
                                Homeworks = includeHomeworks ? s.Homeworks.ToList() : new List<Homework>()
                            }).ToList() : new List<Session>(),
                            // Include Classes only if requested
                            Classes = includeClasses ? c.Classes.ToList() : new List<Class>()
                        });

            return await query.AsNoTrackingWithIdentityResolution().SingleOrDefaultAsync(cou => cou.CourseId == id);
        }

        public async Task<List<Course>> GetList()
        {
            // Define a dictionary for custom order mapping
            var statusOrder = new Dictionary<string, int>
            {
                { "Available", 1 },
                { "Postponed", 2 },
                { "EndofService", 3 },
                { "Cancelled", 4 },
                { "Draft", 5 }
            };

            var courses = await _dbContext.Courses
                .Select(c => new Course()
                {
                    CourseId = c.CourseId,
                    CourseName = c.CourseName,
                    CourseType = c.CourseType,
                    Description = c.Description,
                    Difficulty = c.Difficulty,
                    Category = c.Category,
                    Image = c.Image,
                    RequiredAge = c.RequiredAge,
                    TotalHours = c.TotalHours,
                    Status = c.Status,
                    Sessions = c.Sessions,
                    Classes = c.Classes
                })
                .ToListAsync();
            return courses
                .OrderBy(c => statusOrder.ContainsKey(c.Status) ? statusOrder[c.Status] : int.MaxValue)
                .ToList();
        }

        public async Task<List<Course>?> GetListByStatus(string status)
        {
            return await _dbContext.Courses
                .AsNoTrackingWithIdentityResolution()
                .Where(c => c.Status == status)
                .Select(c => new Course()
                {
                    CourseId = c.CourseId,
                    CourseName = c.CourseName,
                    CourseType = c.CourseType,
                    Description = c.Description,
                    Difficulty = c.Difficulty,
                    Category = c.Category,
                    Image = c.Image,
                    RequiredAge = c.RequiredAge,
                    TotalHours = c.TotalHours,
                    Status = c.Status,
                    Sessions = c.Sessions
                })
                .ToListAsync();
        }

        public async Task<List<string>> GetNameList()
        {
            return await _dbContext.Courses
                .Select(c => c.CourseName)
                .ToListAsync();
        }
        public async Task<List<string>> GetNameListByStatus(string status)
        {
            return await _dbContext.Courses
                .Where(c => c.Status == status)
                .Select(c => c.CourseName)
                .ToListAsync();
        }

        public async Task<Course?> GetByName(string name)
        {
            return await _dbContext.Courses.AsNoTrackingWithIdentityResolution().SingleOrDefaultAsync(cou => cou.CourseName == name);
        }

        public async Task<int> GetIdByName(string name)
        {
            return await _dbContext.Courses
                .Where(c => c.CourseName == name)
                .Select(c => c.CourseId)
                .FirstOrDefaultAsync();
        }

        public async Task<string?> GetStatusByHomeworkIdNoTracking(int homeworkId)
        {
            return await _dbContext.Homeworks
                .Where(h => h.HomeworkId == homeworkId)
                .Select(h => h.Session.Course.Status)
                .FirstOrDefaultAsync();
        }

        public async Task<string?> GetStatusByCourseIdNoTracking(int courseId)
        {
            return await _dbContext.Courses
                .Where(h => h.CourseId == courseId)
                .Select(h => h.Status)
                .FirstOrDefaultAsync();
        }

        public async Task<string?> GetStatusBySessionIdNoTracking(int sessionId)
        {
            return await _dbContext.Sessions
                .Where(h => h.SessionId == sessionId)
                .Select(h => h.Course.Status)
                .FirstOrDefaultAsync();
        }

        public async Task<string?> GetStatusByQuestionIdNoTracking(int questionId)
        {
            return await _dbContext.HomeworkQuestions
                .Where(h => h.HomeworkQuestionId == questionId)
                .Select(hq => hq.Homework != null
                      ? hq.Homework.Session.Course.Status
                      : "NotFound")
                .FirstOrDefaultAsync();
        }

        public async Task<int> GetTotalAmount()
        {
            return await _dbContext.Courses.CountAsync();
        }



        public async Task UpdateTotalHoursByIdThroughSessionsSum(int id)
        {
            int totalHours = await _dbContext.Sessions
                                .Where(ses => ses.CourseId == id)
                                .SumAsync(ses => ses.Hours != null ? ses.Hours.Value : 0);

            // Save changes
            try
            {
                /*// Reattach entity and mark it as modified
                _dbContext.Courses.Attach(selectedCourse);
                _dbContext.Entry(selectedCourse).Property(c => c.TotalHours).IsModified = true;
                await _dbContext.SaveChangesAsync();
                _dbContext.SaveChanges();*/
                await _dbContext.Courses
                    .Where(c => c.CourseId == id)
                    .ExecuteUpdateAsync(setters => setters.SetProperty(c => c.TotalHours, totalHours));
            }
            catch (DbUpdateConcurrencyException ex)
            {
                // Handle concurrency issues (e.g., row modified by another user)
                throw new InvalidOperationException("Update failed due to a concurrency conflict.", ex);
            }
            catch (Exception ex)
            {
                // Log and rethrow unexpected exceptions
                throw new Exception("An unexpected error occurred while updating course total hours.", ex);
            }
        }

        public async Task UpdateStatusAsync(int id, string status)
        {
            try
            {
                // Save changes
                await _dbContext.Courses
                    .Where(c => c.CourseId == id)
                    .ExecuteUpdateAsync(setters => setters.SetProperty(c => c.Status, status));
            }
            catch (DbUpdateConcurrencyException ex)
            {
                // Handle concurrency issues (e.g., row modified by another user)
                throw new InvalidOperationException("Update failed due to a concurrency conflict.", ex);
            }
            catch (Exception ex)
            {
                // Log and rethrow unexpected exceptions
                throw new Exception("An unexpected error occurred while updating course status.", ex);
            }
        }
    }
}
