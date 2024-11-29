﻿using CEG_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_BAL.ViewModels
{
    public class GameViewModel
    {
        public int? GameId { get; set; }

        public int? GameConfigId { get; set; }

        public string? DownloadLink { get; set; }

        public string Title { get; set; } = null!;

        public int? Point { get; set; }

        public string? Status { get; set; }

        public string? Type { get; set; }

        public GameConfigViewModel? GameConfig { get; set; }

        public List<GameLevelViewModel> GameLevels { get; set; } = new List<GameLevelViewModel>();
    }
}
