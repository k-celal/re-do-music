using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReDoMusic.Domain.Enums
{
    public enum Payment
    {
        CreditCard = 1,
        DebitCard = 2,
        BankTransfer = 3,
        Cash = 4,
        Other = 5
    }
}
