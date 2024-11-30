using AutoMapper;
using CEG_BAL.Services.Interfaces;
using CEG_BAL.ViewModels;
using CEG_BAL.ViewModels.Admin.Create;
using CEG_BAL.ViewModels.Admin.Update;
using CEG_DAL.Infrastructure;
using CEG_DAL.Models;
using Microsoft.Extensions.Configuration;

namespace CEG_BAL.Services.Implements
{
    public class StudentAnswerService : IStudentAnswerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public StudentAnswerService(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _configuration = configuration;
        }

        public void Create(CreateNewStudentAnswer newStuAns)
        {
            var answ = new StudentAnswer();
            if (newStuAns != null)
            {
                answ.Answer = newStuAns.Answer;
                answ.Type = newStuAns.Type;
                answ.StudentHomeworkId = newStuAns.StudentHomeworkId.Value;
                answ.GameId = newStuAns.GameId.Value;
            }
            _unitOfWork.StudentAnswerRepositories.Create(answ);
            _unitOfWork.Save();
        }

        public async Task<StudentAnswerViewModel?> GetById(int id)
        {
            var answ = await _unitOfWork.StudentAnswerRepositories.GetByIdNoTracking(id);
            if (answ != null)
            {
                var answvm = _mapper.Map<StudentAnswerViewModel>(answ);
                return answvm;
            }
            return null;
        }

        public async Task<List<StudentAnswerViewModel>> GetList()
        {
            return _mapper.Map<List<StudentAnswerViewModel>>(await _unitOfWork.StudentAnswerRepositories.GetList());
        }

        public void Update(UpdateStudentAnswer upStuAns)
        {
            var answ = _unitOfWork.StudentAnswerRepositories.GetByIdNoTracking(upStuAns.StudentAnswerId.Value).Result;
            if (answ == null) throw new Exception("Student answer not found.");
            if (upStuAns == null) throw new Exception("New student answer is null.");
            answ.Answer = upStuAns.Answer;
            answ.Type = upStuAns.Type;
            answ.GameId = upStuAns.GameId.Value;
            answ.StudentHomeworkId = upStuAns.StudentHomeworkId.Value;
            _unitOfWork.StudentAnswerRepositories.Update(answ);
            _unitOfWork.Save();
        }
    }
}
