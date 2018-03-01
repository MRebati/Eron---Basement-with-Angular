using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Eron.Core.AppEnums;
using Eron.Core.Entities.User;
using Eron.Core.Infrastructure;

namespace Eron.Core.Entities.Financial.Base
{
    public class Invoice : Entity<long>
    {
        public Invoice()
        {
            this.InvoiceItems = new List<InvoiceItem>();
        }

        [Required]
        public string InvoiceNumber { get; set; }

        public DateTime ExpireDateTime { get; set; }

        public bool Expired { get; set; }

        public bool Paid { get; set; }

        public InvoiceStatusType InvoiceStatus { get; set; }

        public InvoiceType Type { get; set; }

        public int Progress { get; set; }

        [Required]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public string ReferenceId { get; set; }

        public ICollection<InvoiceItem> InvoiceItems { get; set; }

        public ICollection<FinanceTransaction> FinanceTransactions { get; set; }

        public long Amount { get; set; }

        [NotMapped]
        public int TotalCount { get; set; }

        public void CheckProgress()
        {
            switch (InvoiceStatus)
            {
                case InvoiceStatusType.WaitingForPayment:
                    Progress = 1;
                    break;
                case InvoiceStatusType.Received:
                    Progress = 2;
                    break;
                case InvoiceStatusType.Processing:
                    Progress = 3;
                    break;
                case InvoiceStatusType.Posting:
                    Progress = 4;
                    break;
                case InvoiceStatusType.Posted:
                    Progress = 5;
                    break;
                case InvoiceStatusType.Delivered:
                    Progress = 6;
                    break;
                case InvoiceStatusType.Canceled:
                    Progress = 0;
                    break;
                case InvoiceStatusType.Expired:
                    Progress = 0;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
