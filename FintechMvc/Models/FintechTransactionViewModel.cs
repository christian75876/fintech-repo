using FintechLibrary.DTOs;

namespace FintechMvc.Models;

public class FintechTransactionViewModel
{
    public int Id { get; set; }
    public int? OriginAccountId { get; set; }
    public int? DestinationAccountId { get; set; }
    public DateTime Date { get; set; }

    public double Amount { get; set; }

    public TransactionType TypeTransaction { get; set; }


    public CuentaViewModel? OriginAccount { get; set; }
    public CuentaViewModel? DestinationAccount { get; set; }
}