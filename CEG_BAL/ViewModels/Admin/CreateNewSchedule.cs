using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_BAL.ViewModels.Admin
{
    public class CreateNewSchedule
    {
        public TimeOnly StartTime { get; set; }

        public TimeOnly EndTime { get; set; }
        public int? SessionId { get; set; }
    }
}
