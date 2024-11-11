using CEG_RazorWebApp.Libraries;
using CEG_RazorWebApp.Models.Account.Get;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace CEG_RazorWebApp.Pages.Admin.Account
{
    public class ParentInfoModel : PageModel
    {
        private CEG_RAZOR_Library methcall = new();
        public string? LayoutUrl { get; set; } = Constants.ADMIN_LAYOUT_URL;
        public AccountInfoVM? AccountInfo { get; set; } = new AccountInfoVM();
        public int AccountId = 0;
        public void OnGet(
            [FromRoute][Required] int accountId)
        {
            methcall.InitTempData(this);
            AccountId = accountId;
        }
    }
}
