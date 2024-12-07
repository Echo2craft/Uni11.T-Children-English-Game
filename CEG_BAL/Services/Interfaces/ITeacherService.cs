using CEG_BAL.ViewModels;
using CEG_BAL.ViewModels.Account.Create;
using CEG_BAL.ViewModels.Admin.Get;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_BAL.Services.Interfaces
{
    public interface ITeacherService
    {
        Task<List<TeacherViewModel>> GetTeacherList();
        Task<List<GetTeacherNameOption>?> GetTeacherNameOptionList();
        Task<TeacherViewModel?> GetTeacherById(int id);
        Task<TeacherViewModel?> GetByAccountId(int id);
        Task<bool> IsExistByEmail(string email);
        Task<bool> IsExistByFullname(string fullname);
        Task<bool> IsExistById(int id);
        Task Create(CreateNewTeacher newTeach);
        void Update(TeacherViewModel teacher);
        Task UploadToBlobAsync(string teacherName, IFormFile certificate, string imageType);
    }
}
