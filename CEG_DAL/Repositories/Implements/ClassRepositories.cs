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
    public class ClassRepositories : RepositoryBase<Class>, IClassRepositories
    {
        private readonly MyDBContext _dbContext;
        public ClassRepositories(MyDBContext dbContext) : base(dbContext) 
        {
            _dbContext = dbContext;
        }
        /// <summary>
        /// Get Class Info by Class id
        /// </summary>
        /// <param name="id">Class id</param>
        /// <param name="includeTeacher">Default: false, determine whether if the query should include teacher info</param>
        /// <param name="includeCourse">Default: false, determine whether if the query should include course info</param>
        /// <param name="includeSession">Default: false, determine whether if the query should include course's sessions info</param>
        /// <param name="filterSession">Default: false, determine whether if the query should include filter session infos to only contain unscheduled session</param>
        public async Task<Class?> GetByIdNoTracking(
            int id, 
            bool includeTeacher = false, 
            bool includeCourse = false, 
            bool includeSession = false, 
            bool filterSession = false,
            bool includeSchedule = false
            )
        {
            return await _dbContext.Classes
                .AsNoTrackingWithIdentityResolution()
                .Where(cla => cla.ClassId == id)
                .Select(c => new Class
                {
                    ClassId = c.ClassId,
                    ClassName = c.ClassName,
                    StartDate = c.StartDate,
                    EndDate = c.EndDate,
                    MinimumStudents = c.MinimumStudents,
                    MaximumStudents = c.MaximumStudents,
                    EnrollmentFee = c.EnrollmentFee,
                    TeacherId = c.TeacherId,
                    CourseId = c.CourseId,
                    Status = c.Status,
                    Teacher = includeTeacher ? new Teacher // Create a new Teacher object
                    {
                        TeacherId = c.Teacher.TeacherId,
                        AccountId = c.Teacher.AccountId,
                        Email = c.Teacher.Email,
                        Phone = c.Teacher.Phone,
                        Image = c.Teacher.Image,
                        Account = new Account
                        {
                            Fullname = c.Teacher.Account.Fullname,
                            Gender = c.Teacher.Account.Gender,
                        }
                        // Add other necessary properties here, but do NOT include Classes
                    } : null,
                    Course = includeCourse ? new Course // Create a new Course object
                    {
                        CourseId = c.Course.CourseId,
                        CourseName = c.Course.CourseName,
                        Sessions = includeSession
                        ? (filterSession
                            ? c.Course.Sessions.Where(ses => !ses.Schedules.Any(sch => sch.ClassId == c.ClassId)).Select(ses => new Session
                            {
                                SessionId = ses.SessionId,
                                SessionNumber = ses.SessionNumber,
                                Title = ses.Title,
                                Description = ses.Description,
                                Hours = ses.Hours,
                            }).ToList()
                            : c.Course.Sessions.Select(ses => new Session
                            {
                                SessionId = ses.SessionId,
                                SessionNumber = ses.SessionNumber,
                                Title = ses.Title,
                                Description = ses.Description,
                                Hours = ses.Hours,
                            }).ToList())
                        : null,
                        // Add other necessary properties here, but do NOT include Classes
                    } : null,
                    Schedules = includeSchedule ? c.Schedules.Select(sch => new Schedule()
                    {
                        ScheduleId = sch.ScheduleId,
                        ScheduleDate = sch.ScheduleDate,
                        StartTime = sch.StartTime,
                        EndTime = sch.EndTime,
                        Status = sch.Status,
                        Session = new Session()
                        {
                            SessionId = sch.SessionId,
                            CourseId = sch.Session.CourseId,
                            SessionNumber = sch.Session.SessionNumber,
                            Title = sch.Session.Title,
                            Description = sch.Session.Description,
                            Hours = sch.Session.Hours
                        }
                    }).OrderBy(sch => sch.ScheduleDate).ToList() : null,
                    Enrolls = c.Enrolls.Select(s => new Enroll()
                    {
                        EnrollId = s.EnrollId,
                        Student = new Student()
                        {
                            Account = new Account()
                            {
                                Fullname = s.Student.Account.Fullname
                            }
                        },
                        Status = s.Status
                    }).ToList(),
                })
                .SingleOrDefaultAsync();
        }

        public async Task<List<Class>> GetList()
        {
            return await _dbContext.Classes
                .AsNoTrackingWithIdentityResolution()
                .Select(c => new Class
                {
                    ClassId = c.ClassId,
                    ClassName = c.ClassName,
                    StartDate = c.StartDate,
                    EndDate = c.EndDate,
                    MinimumStudents = c.MinimumStudents,
                    MaximumStudents = c.MaximumStudents,
                    EnrollmentFee = c.EnrollmentFee,
                    TeacherId = c.TeacherId,
                    CourseId = c.CourseId,
                    Status = c.Status,
                    Teacher = new Teacher // Create a new Teacher object
                    {
                        TeacherId = c.Teacher.TeacherId,
                        Email = c.Teacher.Email,
                        Phone = c.Teacher.Phone,
                        Image = c.Teacher.Image,
                        Account = new Account
                        {
                            Fullname = c.Teacher.Account.Fullname,
                            Gender = c.Teacher.Account.Gender,
                        }
                        // Add other necessary properties here, but do NOT include Classes
                    },
                    Course = new Course // Create a new Course object
                    {
                        CourseId = c.Course.CourseId,
                        CourseName = c.Course.CourseName
                        // Add other necessary properties here, but do NOT include Classes
                    },
                    Schedules = c.Schedules,
                    Enrolls = c.Enrolls,
                })
                .ToListAsync();
        }

        public async Task<List<Class>> GetOptionListByStatusOpen(string filterClassByStudentName = "")
        {
            // Base query for classes with status "Open"
            var query = _dbContext.Classes
                .AsNoTrackingWithIdentityResolution()
                .Where(c => c.Status.Equals("Open"));

            // Apply the filter if a student name is provided
            if (!string.IsNullOrWhiteSpace(filterClassByStudentName))
            {
                query = query.Where(c => !c.Enrolls.Any(enr => enr.Student.Account.Fullname == filterClassByStudentName));
            }

            // Project only the required fields to the result
            return await query
                .Select(c => new Class
                {
                    ClassName = c.ClassName,
                    EnrollmentFee = c.EnrollmentFee
                })
                .ToListAsync();
        }

        public async Task<List<Class>> GetClassListParent()
        {
            return await _dbContext.Classes
                .Select(c => new Class
                {
                    ClassId = c.ClassId,
                    ClassName = c.ClassName,
                    StartDate = c.StartDate,
                    EndDate = c.EndDate,
                    MinimumStudents = c.MinimumStudents,
                    MaximumStudents = c.MaximumStudents,
                    EnrollmentFee = c.EnrollmentFee,
                    TeacherId = c.TeacherId,
                    CourseId = c.CourseId,
                    Status = c.Status,
                    Teacher = new Teacher // Create a new Teacher object
                    {
                        TeacherId = c.Teacher.TeacherId,
                        Email = c.Teacher.Email,
                        Phone = c.Teacher.Phone,
                        Image = c.Teacher.Image,
                        Account = new Account
                        {
                            Fullname = c.Teacher.Account.Fullname,
                            Gender = c.Teacher.Account.Gender,
                        }
                        // Add other necessary properties here, but do NOT include Classes
                    },
                    Course = new Course // Create a new Course object
                    {
                        CourseId = c.Course.CourseId,
                        CourseName = c.Course.CourseName
                        // Add other necessary properties here, but do NOT include Classes
                    },
                    Schedules = c.Schedules,
                    Enrolls = c.Enrolls,
                })
                .ToListAsync();
        }

        public async Task<List<Class>> GetListByTeacherId(int teacherId)
        {
            return await _dbContext.Classes
                .AsNoTrackingWithIdentityResolution()
                .Where(c => c.TeacherId == teacherId && c.Status != "Draft")
                .Select(c => new Class
                {
                    ClassId = c.ClassId,
                    ClassName = c.ClassName,
                    StartDate = c.StartDate,
                    EndDate = c.EndDate,
                    MinimumStudents = c.MinimumStudents,
                    MaximumStudents = c.MaximumStudents,
                    EnrollmentFee = c.EnrollmentFee,
                    TeacherId = c.TeacherId,
                    CourseId = c.CourseId,
                    Status = c.Status,
                    Teacher = new Teacher // Create a new Teacher object
                    {
                        TeacherId = c.Teacher.TeacherId,
                        Email = c.Teacher.Email,
                        Phone = c.Teacher.Phone,
                        Image = c.Teacher.Image,
                        Account = new Account
                        {
                            Fullname = c.Teacher.Account.Fullname,
                            Gender = c.Teacher.Account.Gender,
                        }
                        // Add other necessary properties here, but do NOT include Classes
                    },
                    Course = new Course // Create a new Course object
                    {
                        CourseId = c.Course.CourseId,
                        CourseName = c.Course.CourseName
                        // Add other necessary properties here, but do NOT include Classes
                    },
                    Schedules = c.Schedules,
                    Enrolls = c.Enrolls,
                })
                .ToListAsync();
        }

        public async Task<int> GetIdByClassId(int id)
        {
            var result = await (from c in _dbContext.Classes where c.ClassId == id select c).FirstOrDefaultAsync();
            if (result != null) return result.ClassId;
            return 0;
        }

        public async Task<Class?> GetByClassName(string className)
        {
            return await _dbContext.Classes
                .AsNoTrackingWithIdentityResolution()
                .SingleOrDefaultAsync(c => c.ClassName == className);
        }

        public async Task<int> GetTotalAmount()
        {
            return await _dbContext.Classes.CountAsync();
        }
    }
}
