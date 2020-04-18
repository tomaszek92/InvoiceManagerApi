using FluentValidation;
using InvoiceManagerApi.Logic.Clients.Validators;
using InvoiceManagerApi.Logic.Invoices.Validators;
using InvoiceManagerApi.Models;

namespace InvoiceManagerApi.Logic.Invoices.Update
{
    public class Validator : AbstractValidator<Command>
    {
        public Validator(IApplicationDbContext dbContext)
        {
            RuleFor(x => x.Invoice.Id)
                .NotEmpty();

            // Założenie, pewnie przydałoby się ustawiać to na podstawie konfiguracji, ale na potrzeby demo myślę, że to wystarczy.
            RuleFor(x => x.Invoice.Year)
                .GreaterThanOrEqualTo(2000);

            RuleFor(x => x.Invoice.Month)
                .IsInEnum();

            // Nie wiem czy nie trzeba sprawdzić czy w danym roku i miesiącu już nie istnieje faktura o tym numerze,
            // aby nie było dwóch faktur o tym samym numerze.
            RuleFor(x => x.Invoice.Number)
                .GreaterThanOrEqualTo(1);

            RuleFor(x => x.Invoice.ClientId)
                .SetValidator(new ClientIsDefinedValidator(dbContext));

            RuleFor(x => x.Invoice.Paytime)
                .GreaterThanOrEqualTo(1);

            RuleFor(x => x.Invoice.PaymentType)
                .IsInEnum();

            RuleFor(x => x.Invoice.Rows.Count)
                .GreaterThanOrEqualTo(1);

            RuleForEach(x => x.Invoice.Rows)
                .SetValidator(new InvoiceRowValidator());
        }
    }
}
