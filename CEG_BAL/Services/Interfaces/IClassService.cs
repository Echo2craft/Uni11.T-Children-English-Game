using CEG_BAL.ViewModels;
using CEG_BAL.ViewModels.Admin;
using CEG_BAL.ViewModels.Admin.Update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_BAL.Services.Interfaces
{
    public interface IClassService
    {
        void Create(ClassViewModel classModel, CreateNewClass newClass);
        void Update(ClassViewModel classModel);
        void Update(ClassViewModel classModel, UpdateClass classNewModel);
        void UpdateStatus(int classId, string classStatus);
        Task<List<ClassViewModel>> GetClassList();
        Task<List<ClassViewModel>> GetClassListAdmin();
        Task<List<ClassViewModel>> GetClassListByTeacherAccountId(int id);
        Task<ClassViewModel?> GetClassById(int id);
    }
}
