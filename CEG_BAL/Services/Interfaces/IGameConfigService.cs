using CEG_BAL.ViewModels;
using CEG_BAL.ViewModels.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_BAL.Services.Interfaces
{
    public interface IGameConfigService
    {
        void Create(CreateNewGameConfig newGameConfig);
        void Update(GameConfigViewModel model);
        Task<List<GameConfigViewModel>> GetGameConfigsList();
        Task<GameConfigViewModel?> GetGameConfigById(int id);
    }
}
