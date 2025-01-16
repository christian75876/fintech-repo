using FintechLibrary.DTOs;
using FintechWebAPI.Class;

namespace FintechWebAPI.Entity
{

    public class FintechTransaction
    {
        public int Id { get; set; }
        public int? OriginAccountId { get; set; }
        public int? DestinationAccountId { get; set; }
        public DateTime Date { get; set; }

        public double Amount { get; set; }

        public TransactionType TypeTransaction { get; set; }


        public Cuenta? OriginAccount { get; set; }
        public Cuenta? DestinationAccount { get; set; }

    }
}
