using FluentValidation;
using InvoiceManagerApi.Models;

namespace InvoiceManagerApi.Logic.Invoices.Validators
{
    public class InvoiceRowValidator : AbstractValidator<InvoiceRow>
    {
        public InvoiceRowValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty();

            RuleFor(x => x.Count)
                .GreaterThanOrEqualTo(1);

            RuleFor(x => x.VatRate)
                .GreaterThanOrEqualTo(0)
                .LessThanOrEqualTo(100);

            RuleFor(x => x.UnitPriceNet)
                .GreaterThan(0);
        }
    }
}
