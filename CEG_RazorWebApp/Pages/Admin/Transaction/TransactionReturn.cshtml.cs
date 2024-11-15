using Azure.Storage.Blobs.Models;
using CEG_BAL.ViewModels;
using CEG_RazorWebApp.Libraries;
using CEG_RazorWebApp.Models.Transaction.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CEG_RazorWebApp.Pages.Admin.Transaction
{
    public class TransactionReturnModel : PageModel
    {
        public TransactionResponseVM TransactionResponse { get; set; } = new TransactionResponseVM();
        private readonly IConfiguration _config;
        private readonly VnPayLibrary _vnPayLibrary;
        private readonly HttpClient _httpClient;

        public TransactionReturnModel(IConfiguration config, HttpClient httpClient)
        {
            _config = config;
            _vnPayLibrary = new VnPayLibrary();
            _httpClient = httpClient;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            string hashSecret = _config["Vnpay:HashSecret"]; // Replace with actual VNPay hash secret
            TransactionResponse = _vnPayLibrary.GetFullResponseData(Request.Query, hashSecret);

            if (TransactionResponse.Success)
            {
                TransactionResponse.Message = "Transaction Successful!";
                // Call the API to create the transaction in the database
                var transactionData = new TransactionViewModel
                {
                    ParentFullname = TransactionResponse.OrderDescription,
                    TransactionAmount = (int)TransactionResponse.Value,
                    TransactionDate = DateTime.Now,
                    TransactionStatus = "Completed",
                    TransactionType = TransactionResponse.TransactionType,
                    ConfirmDate = DateTime.Now,
                    VnpayId = TransactionResponse.VnpayId
                };

                var apiResponse = await _httpClient.PostAsJsonAsync("https://localhost:7143/api/Transaction/Create", transactionData);

                if (!apiResponse.IsSuccessStatusCode)
                {
                    TransactionResponse.Message = "Transaction processed, but failed to save.";
                }
            }
            else
            {
                TransactionResponse.Message = "Transaction Failed.";
            }

            return Page();
        }
    }
}
