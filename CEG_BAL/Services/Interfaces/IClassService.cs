using CEG_BAL.ViewModels;
using CEG_BAL.ViewModels.Admin;
using CEG_BAL.ViewModels.Admin.Get;
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
        Task Create(CreateNewClass newCla);
        Task Update(int claId, UpdateClass upCla);
        Task UpdateStatus(int claId, string upClaStatus);
        Task<List<ClassViewModel>> GetClassList();
        Task<List<GetClassForTransaction>> GetOptionListByStatusOpen();
        Task<List<ClassViewModel>> GetClassListParent();
        Task<List<ClassViewModel>> GetListAdmin();
        Task<List<ClassViewModel>> GetListByTeacherAccountId(int id);
        /// <summary>
        /// Get Class Info by Class id. 
        /// int id, class id to be use to query. 
        /// bool includeTeacher = true, determine whether if the query should include teacher info. 
        /// bool includeCourse = true, determine whether if the query should include course info. 
        /// bool includeSession = false, determine whether if the query should include course's sessions info. 
        /// bool filterSession = false, determine whether if the query should include filter session infos to only contain unscheduled session. 
        /// </summary>
        /// <param name="id">Class id</param>
        /// <param name="includeTeacher">Default: false, determine whether if the query should include teacher info</param>
        /// <param name="includeCourse">Default: false, determine whether if the query should include course info</param>
        /// <param name="includeSession">Default: false, determine whether if the query should include course's sessions info</param>
        /// <param name="filterSession">Default: false, determine whether if the query should include filter session infos to only contain unscheduled session</param>
        Task<ClassViewModel?> GetById(
            int id, 
            bool includeTeacher = false, 
            bool includeCourse = false, 
            bool includeSession = false, 
            bool filterSession = false,
            bool includeSchedule = false
        );
        Task<ClassViewModel?> GetByIdParent(int id);
        Task<ClassViewModel?> GetByClassName(string className);
        Task<bool> IsEditableById(int id);
    }
}
