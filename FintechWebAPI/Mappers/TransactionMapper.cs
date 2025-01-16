using FintechLibrary.DTOs;
using FintechWebAPI.Entity;
using Microsoft.EntityFrameworkCore;

namespace FintechWebAPI.Mappers;

public static class TransactionMapper
{
    public static TransactionDTO TransactionDTO(FintechTransaction transaction)
    {
        return new TransactionDTO
        {
            Id = transaction.Id,
            OriginAccountId = transaction.OriginAccountId,
            DestinationAccountId = transaction.DestinationAccountId,
            Amount = transaction.Amount,
            TypeTransaction = transaction.TypeTransaction,
        };
    }

    public static TransactionDTO FinTransactionEntity(FintechTransaction transactionDto)
    {
        return new TransactionDTO
        {
            Id = transactionDto.Id,
            OriginAccountId = transactionDto.OriginAccountId,
            DestinationAccountId = transactionDto.DestinationAccountId,
            Amount = transactionDto.Amount,
            TypeTransaction = transactionDto.TypeTransaction,
        };
    }
}