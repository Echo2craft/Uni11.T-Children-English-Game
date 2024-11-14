using CEG_RazorWebApp.Libraries;
using CEG_RazorWebApp.Models.Class.Update;
using CEG_RazorWebApp.Models.Schedule.Create;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace CEG_RazorWebApp.Pages.Student.Class
{
    public class ClassInfoModel : PageModel
    {
        private readonly CEG_RAZOR_Library methcall = new();
        public int? ClassID;
        public void OnGet(
            [FromRoute][Required] int classId)
        {
            ClassID = classId;
        }
    }
}
