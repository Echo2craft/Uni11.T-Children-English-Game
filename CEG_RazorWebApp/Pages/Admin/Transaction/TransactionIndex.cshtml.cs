using AutoMapper;
using CEG_RazorWebApp.Libraries;
using CEG_RazorWebApp.Models.Transaction.Create;
using CEG_RazorWebApp.Pages.Admin.Course;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace CEG_RazorWebApp.Pages.Admin.Transaction
{
    public class TransactionIndexModel : PageModel
    {
        public string? LayoutUrl { get; set; } = Constants.ADMIN_LAYOUT_URL;
        public CreateTransactionVM CreateTransactionInfo { get; set; } = new CreateTransactionVM();
        public void OnGet()
        {

        }
    }
}
