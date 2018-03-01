using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eron.Core.Infrastructure;

namespace Eron.Core.Entities.Financial.Base
{
    public class FinanceTransaction: Entity<Guid>
    {
        public FinanceTransaction(long invoiceId, string transactionNumber, string userId)
        {
            this.Id = Guid.NewGuid();
            this.InvoiceId = invoiceId;
            this.TransactionNumber = transactionNumber;
            this.UserId = userId;
            this.Successful = false;
        }

        public FinanceTransaction(long invoiceId, string transactionNumber, string userId, string referenceId)
        {
            InvoiceId = invoiceId;
            TransactionNumber = transactionNumber;
            UserId = userId;
            ReferenceId = referenceId;
        }

        public FinanceTransaction()
        {
            this.Id = Guid.NewGuid();
        }

        public string TransactionNumber { get; set; }

        public long InvoiceId { get; set; }

        public string UserId { get; set; }

        public string BankResponse { get; set; }

        public bool Successful { get; set; }

        public string ReferenceId { get; set; }

        public Invoice Invoice { get; set; }
    }
}
