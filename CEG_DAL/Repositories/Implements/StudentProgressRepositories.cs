﻿using CEG_DAL.Infrastructure;
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
    public class StudentProgressRepositories : RepositoryBase<StudentProgress>, IStudentProgressRepositories
    {
        private readonly MyDBContext _dbContext;
        public StudentProgressRepositories(MyDBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<StudentProgress?> GetByIdNoTracking(int id)
        {
            return await _dbContext.StudentProgresses.AsNoTrackingWithIdentityResolution().SingleOrDefaultAsync(pro => pro.StudentProgressId == id);
        }

        public async Task<List<StudentProgress>> GetList()
        {
            return await _dbContext.StudentProgresses.AsNoTrackingWithIdentityResolution().ToListAsync();
        }

        public async Task<List<StudentProgress>> GetListByHomeworkId(int homId)
        {
            return await _dbContext.StudentProgresses
                .AsNoTrackingWithIdentityResolution()
                .Where(stuPro => stuPro.StudentHomeworks.Any(stuHom => stuHom.HomeworkId == homId))
                .ToListAsync();
        }

        public async Task<List<StudentProgress>> GetListByMultipleHomeworkId(int[] homIds)
        {
            return await _dbContext.StudentProgresses
                .AsNoTrackingWithIdentityResolution()
                .Where(stuPro => stuPro.StudentHomeworks.Any(stuHom => homIds.Contains(stuHom.HomeworkId)))
                .Select(stuPro => new StudentProgress()
                {
                    StudentId = stuPro.StudentId,
                    TotalPoint = stuPro.TotalPoint,
                    Playtime = stuPro.Playtime,
                    StudentHomeworks = stuPro.StudentHomeworks.Select(stuHom => new StudentHomework()
                    {
                        HomeworkId = stuHom.HomeworkId,
                        Point = stuHom.Point,
                        CorrectAnswers = stuHom.CorrectAnswers,
                        Playtime = stuHom.Playtime,
                        StudentAnswers = stuHom.StudentAnswers,
                        HomeworkResult = stuHom.HomeworkResult,
                        Status = stuHom.Status,
                    }).ToList()
                })
                .ToListAsync();
        }

        public async Task UpdateStudentProgressTotalPointsAsync()
        {
            var studentProgresses = await _dbContext.StudentProgresses
                .Include(sp => sp.StudentHomeworks)
                .ThenInclude(sh => sh.HomeworkResult)
                .ToListAsync();

            foreach (var progress in studentProgresses)
            {
                progress.TotalPoint = progress.StudentHomeworks
                    .Sum(sh => sh.HomeworkResult.TotalPoint ?? 0);
            }

            await _dbContext.SaveChangesAsync();
        }
    }
}
