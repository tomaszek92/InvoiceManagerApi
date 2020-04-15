using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceManagerApi.Models
{
    public class InvoiceRow
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        public int VatRate { get; set; }
        public decimal UnitPriceNet { get; set; }
        public decimal UnitPriceGross { get; set; }
    }
}
