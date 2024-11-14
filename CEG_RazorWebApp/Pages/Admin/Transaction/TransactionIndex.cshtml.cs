using AutoMapper;
using CEG_RazorWebApp.Libraries;
using CEG_RazorWebApp.Models.Transaction.Create;
using CEG_RazorWebApp.Models.VnPay;
using CEG_RazorWebApp.Pages.Admin.Course;
using CEG_RazorWebApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace CEG_RazorWebApp.Pages.Admin.Transaction
{
    public class TransactionIndexModel : PageModel
    {
        private IConfiguration _config;
        public string? LayoutUrl { get; set; } = Constants.ADMIN_LAYOUT_URL;
        public CreateTransactionVM CreateTransactionInfo { get; set; } = new CreateTransactionVM();

        public TransactionIndexModel(IConfiguration config)
        {
            _config = config;
        }

        public void OnGet()
        {

        }

        // Update the method name to follow Razor Page conventions
        public IActionResult OnPostGeneratePaymentUrl([FromBody] CreateTransactionVM createTransaction)
        {
            if (!ModelState.IsValid)
            {
                return new JsonResult(new { success = false, message = "Invalid data." });
            }

            // Initialize VNPay library and populate the request data
            var vnPay = new VnPayLibrary();
            vnPay.AddRequestData("vnp_TxnRef", Guid.NewGuid().ToString()); // Unique transaction ID
            vnPay.AddRequestData("vnp_OrderInfo", $"{createTransaction.ParentFullname},{createTransaction.TransactionAmount},{createTransaction.TransactionType}");
            vnPay.AddRequestData("vnp_Amount", (createTransaction.TransactionAmount * 100).ToString()); // Amount in smallest currency unit
            vnPay.AddRequestData("vnp_CreateDate", createTransaction.TransactionDate.ToString("yyyyMMddHHmmss"));
            vnPay.AddRequestData("vnp_CurrCode", "VND"); // Currency
            vnPay.AddRequestData("vnp_Locale", "vn"); // Locale

            // Create VNPay payment URL
            string baseUrl = _config.GetSection("Vnpay:BaseUrl").Value;
            string vnpHashSecret = _config.GetSection("Vnpay:HashSecret").Value;
            string paymentUrl = vnPay.CreateRequestUrl(baseUrl, vnpHashSecret);

            return new JsonResult(new { success = true, paymentUrl = paymentUrl });
        }
    }
}
