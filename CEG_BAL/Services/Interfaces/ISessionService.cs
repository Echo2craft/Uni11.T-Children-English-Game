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
    public interface ISessionService
    {
        Task Create(CreateNewSession newSes);
        Task Update(int upSesId, UpdateSession upSes);
        Task Delete(int delSesId);
        Task<List<SessionViewModel>> GetSessionList();
        Task<SessionViewModel?> GetSessionById(int id);
        Task<bool> IsSessionExistByTitle(string title);
        Task<List<SessionViewModel>> GetSessionListByCourseId(int courseId);
    }
}
