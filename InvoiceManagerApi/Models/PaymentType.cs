using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceManagerApi.Models
{
    public enum PaymentType
    {
        BankTransfer = 0,
        Cash = 1,
        CreditCard = 2
    }
}
