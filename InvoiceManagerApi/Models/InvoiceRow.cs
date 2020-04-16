using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvoiceManagerApi.Models
{
    public class InvoiceRow
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        public int VatRate { get; set; }
        public decimal UnitPriceNet { get; set; }
        public decimal UnitPriceGross { get; set; }

        [ForeignKey(nameof(Invoice))]
        public int InvoiceId { get; set; }
        public Invoice Invoice { get; set; }
    }
}
