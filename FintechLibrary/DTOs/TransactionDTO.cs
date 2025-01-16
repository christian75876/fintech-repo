using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FintechLibrary.DTOs
{

    public enum TransactionType
    {
        transfer_deposit,
        transfer_withdrawal
    }
    public class TransactionDTO
    {
        public int Id { get; set; }
        public int? OriginAccountId { get; set; }
        public int? DestinationAccountId { get; set; }
        public DateTime Date { get; set; }
        public double Amount { get; set; }
        public TransactionType TypeTransaction { get; set; }
    }
}
