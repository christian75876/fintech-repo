using System.ComponentModel.DataAnnotations;
using FintechLibrary.DTOs;

namespace FintechMvc.Models;

public class CuentaViewModel
{
        public int Id { get; set; }
        public string AccountNumber { get; set; }
        public double Balance { get; set; }
        public EnumAccountType AccountType { get; set; }
        public List<FintechTransactionViewModel> OriginTransactions { get; set; }
        public List<FintechTransactionViewModel> DestinationTransactions { get; set; }
}