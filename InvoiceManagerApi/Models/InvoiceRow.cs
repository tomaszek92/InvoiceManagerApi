using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace InvoiceManagerApi.Models
{
    public class InvoiceRow
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        public int VatRate { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPriceNet { get; set; }
        public decimal UnitPriceGross => UnitPriceNet * VatRate / 100 + UnitPriceNet;
        
        [ForeignKey(nameof(Invoice))]
        public int InvoiceId { get; set; }
        [JsonIgnore]
        public Invoice Invoice { get; set; }
    }
}
