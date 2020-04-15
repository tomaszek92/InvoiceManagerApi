using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceManagerApi.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int Number { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; }
        public DateTime SellDate { get; set; }
        public DateTime IssueDate { get; set; }
        public int Paytime { get; set; }
        public PaymentType PaymentType { get; set; }
        public bool IsPayed { get; set; }
        public IEnumerable<InvoiceRow> Rows { get; set; }
        public decimal PriceNet => Rows.Sum(r => r.UnitPriceNet * r.Count);
        public decimal PriceGross => Rows.Sum(r => r.UnitPriceGross * r.Count);
    }
}
