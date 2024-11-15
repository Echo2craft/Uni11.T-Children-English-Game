using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_BAL.ViewModels.Transaction
{
    public class TransactionRequest
    {
        public string? ParentFullname { get; set; }
        public int TransactionAmount { get; set; }
        public string TransactionType { get; set; } = null!;
    }
}
