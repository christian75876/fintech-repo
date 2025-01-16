using System.Transactions;
using FintechLibrary.DTOs;
using FintechWebAPI.Class;
using FintechWebAPI.Entity;

namespace FintechWebAPI.Class


{
    
    public class Cuenta
    {
        public int Id { get; set; }
        public string AccountNumber { get; set; }
        public double Balance { get; set; }
        public EnumAccountType AccountType { get; set; }

        public ICollection<FintechTransaction> OriginTransactions { get; set; } = new List<FintechTransaction>();
        public ICollection<FintechTransaction> DestinationTransactions { get; set; } = new List<FintechTransaction>();

    }

}
