using AutoMapper;
using CEG_BAL.Services.Interfaces;
using CEG_BAL.ViewModels;
using CEG_BAL.ViewModels.Admin.Create;
using CEG_BAL.ViewModels.Admin.Update;
using CEG_DAL.Infrastructure;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_BAL.Services.Implements
{
    public class StudentHomeworkService : IStudentHomeworkService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public StudentHomeworkService(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _configuration = configuration;
        }

        public void Create(CreateNewStudentHomework newStuHom)
        {
            throw new NotImplementedException();
        }

        public async Task<StudentHomeworkViewModel?> GetById(int id)
        {
            var answ = await _unitOfWork.StudentHomeworkRepositories.GetByIdNoTracking(id);
            if (answ != null)
            {
                var answvm = _mapper.Map<StudentHomeworkViewModel>(answ);
                return answvm;
            }
            return null;
        }

        public Task<List<StudentHomeworkViewModel>> GetList()
        {
            throw new NotImplementedException();
        }

        public void Update(UpdateStudentHomework upStuHom)
        {
            throw new NotImplementedException();
        }
    }
}
