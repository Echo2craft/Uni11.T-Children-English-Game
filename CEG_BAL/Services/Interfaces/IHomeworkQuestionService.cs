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
    public interface IHomeworkQuestionService
    {
        Task Create(CreateNewQuestion newQues);
        Task CreateWithHomeworkId(CreateNewQuestion newQues, int homId);
        // void Update(HomeworkQuestionViewModel model);
        Task Update(int upQueId, UpdateQuestion upQue);
        Task UpdateWithHomeworkId(int upQueId, int upHomId);
        Task Delete(int delQueId, int homId);
        Task<List<HomeworkQuestionViewModel>> GetList();
        Task<List<HomeworkQuestionViewModel>> GetListByHomeworkId(int homId);
        Task<List<HomeworkQuestionViewModel>> GetExcludedListByHomeworkId(int homId);
        Task<List<HomeworkQuestionViewModel>> GetListByCourseId(int courseId);
        Task<List<HomeworkQuestionViewModel>> GetListBySessionId(int sessionId);
        Task<List<HomeworkQuestionViewModel>?> GetOrderedList();
        Task<HomeworkQuestionViewModel?> GetById(int id, int homId = 0);
    }
}
