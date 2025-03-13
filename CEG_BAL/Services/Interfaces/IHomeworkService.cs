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
    public interface IHomeworkService
    {
        Task Create(CreateNewHomework newHw);
        // void Update(HomeworkViewModel model);
        Task Update(int upHomId, UpdateHomework upHom);
        Task Delete(int delHomId);
        Task<List<HomeworkViewModel>> GetHomeworkList();
        Task<bool> IsHomeworkExistByTitle(string title);
        Task<HomeworkViewModel?> GetHomeworkById(int id);
    }
}
