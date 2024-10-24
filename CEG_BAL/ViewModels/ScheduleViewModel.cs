using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_BAL.ViewModels
{
    public class ScheduleViewModel
    {
        public string? DayOfWeek { get; set; }

        public TimeOnly? StartTime { get; set; }

        public TimeOnly? EndTime { get; set; }

        public string? Status { get; set; }

        public ClassViewModel Class { get; set; } = null!;

        public SessionViewModel Session { get; set; } = null!;
    }
}
