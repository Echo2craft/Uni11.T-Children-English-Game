using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_BAL.ViewModels.Admin.Update
{
    public class UpdateAnswer
    {
        [Required]
        public string? Answer { get; set; }
        [Required]
        public string? Type { get; set; }
    }
}
