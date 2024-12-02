using CEG_BAL.ViewModels.Admin.Create;
using CEG_BAL.ViewModels.Admin.Update;
using CEG_BAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_BAL.Services.Interfaces
{
    public interface IStudentHomeworkService
    {
        void Create(CreateNewStudentHomework newStuHom);
        void Update(UpdateStudentHomework upStuHom);
        Task<List<StudentHomeworkViewModel>> GetList();
        Task<StudentHomeworkViewModel?> GetById(int id);
    }
}
