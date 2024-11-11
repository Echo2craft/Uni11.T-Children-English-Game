using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_BAL.ViewModels
{
    public class StudentAnswerViewModel
    {
        public string? Answer { get; set; }

        public string? Type { get; set; }

        public GameViewModel Game { get; set; } = null!;

        public StudentHomeworkViewModel StudentHomework { get; set; } = null!;
    }
}
