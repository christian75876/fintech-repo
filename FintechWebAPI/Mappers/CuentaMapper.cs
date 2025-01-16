using FintechLibrary.DTOs;
using FintechWebAPI.Class;

namespace FintechWebAPI.Mappers;

public class CuentaMapper
{
    public static AccountDTO CuentaDto( Cuenta account)
    {
        return new AccountDTO
        {
            Id = account.Id,
            AccountNumber = account.AccountNumber,
            Balance = account.Balance,
            AccountType = account.AccountType,
        };
    }

    public static Cuenta ToEntity(AccountDTO accountDTO)
    {
        return new Cuenta
        {
            Id = accountDTO.Id,
            AccountNumber = accountDTO.AccountNumber,
            Balance = accountDTO.Balance,
            AccountType = accountDTO.AccountType
        };
    }
    
}