﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace InvoiceManagerApi.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public Month Month { get; set; }
        public int Number { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; }
        public DateTime SellDate { get; set; }
        public DateTime IssueDate { get; set; }
        public int Paytime { get; set; }
        public PaymentType PaymentType { get; set; }
        public bool IsPayed { get; set; }
        public ICollection<InvoiceRow> Rows { get; set; } = new List<InvoiceRow>();
        public decimal PriceNet => Rows.Sum(r => r.UnitPriceNet * r.Count);
        public decimal PriceGross => Rows.Sum(r => r.UnitPriceGross * r.Count);
    }
}
