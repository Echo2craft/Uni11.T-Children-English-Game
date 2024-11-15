﻿using CEG_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_BAL.ViewModels
{
    public class TransactionViewModel
    {
        public int TransactionId { get; set; }

        public int ParentId { get; set; }

        public string ParentFullname { get; set; } = null!;

        public string VnpayId { get; set; } = null!;

        public int TransactionAmount { get; set; }

        public DateTime TransactionDate { get; set; }

        public string TransactionStatus { get; set; } = null!;

        public string TransactionType { get; set; } = null!;

        public DateTime ConfirmDate { get; set; }

        public virtual ICollection<EnrollViewModel> Enrolls { get; set; } = new List<EnrollViewModel>();

        public virtual ParentViewModel Parent { get; set; } = null!;
    }
}
