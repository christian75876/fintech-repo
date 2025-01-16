using System.ComponentModel.DataAnnotations;
using FintechLibrary.DTOs;

namespace FintechMvc.Models;

public class CuentaViewModel
{
        [Required]
        public string AccountNumber { get; set; }

        [Required]
        public double Balance { get; set; }

        [Required]
        public EnumAccountType AccountType { get; set; }
        public List<FintechTransactionViewModel> OriginTransactions { get; set; }
        public List<FintechTransactionViewModel> DestinationTransactions { get; set; }
}