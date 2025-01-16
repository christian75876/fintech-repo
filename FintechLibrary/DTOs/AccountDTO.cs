using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FintechLibrary.DTOs
{

    public enum EnumAccountType
    {
        ahorros, corriente
    }

    public class AccountDTO
    {
        public int Id { get; set; }
        public string AccountNumber { get; set; }
        public double Balance { get; set; }
        public EnumAccountType AccountType { get; set; }
    }
}
