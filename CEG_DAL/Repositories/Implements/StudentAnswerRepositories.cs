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
    public class StudentAnswerRepositories : RepositoryBase<StudentAnswer>, IStudentAnswerRepositories
    {
        private readonly MyDBContext _dbContext;

        public StudentAnswerRepositories(MyDBContext context) : base(context)
        {
            _dbContext = context;
        }

        public async Task<StudentAnswer?> GetByIdNoTracking(int id)
        {
            return await _dbContext.StudentAnswers
                .AsNoTrackingWithIdentityResolution()
                .SingleOrDefaultAsync(home => home.StudentAnswerId == id);
        }

        public async Task<List<StudentAnswer>> GetList()
        {
            return await _dbContext.StudentAnswers
                .AsNoTrackingWithIdentityResolution()
                .ToListAsync();
        }

        public async Task<List<StudentAnswer>> GetListByStudentId(int stuId)
        {
            return await _dbContext.StudentAnswers
                .Include(sa => sa.StudentHomework)
                .ThenInclude(sh => sh.StudentProgress)
                .Where(sa => sa.StudentHomework.StudentProgress.StudentId == stuId)
                .Select(stuAns => new StudentAnswer()
                {
                    GameId = stuAns.GameId,
                    Answer = stuAns.Answer,
                    Type = stuAns.Type,
                })
                .ToListAsync();
        }
    }
}
