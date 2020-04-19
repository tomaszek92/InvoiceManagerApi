using FluentValidation;
using InvoiceManagerApi.Logic.Validators;
using System;

namespace InvoiceManagerApi.Logic.Clients.Create
{
    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(x => x.Client.Id)
                .Empty();

            RuleFor(x => x.Client.Name)
                .NotEmpty();

            // Przydałaby się walidacja, aby było unikalne, ale na potrzeby demo pomijam.
            RuleFor(x => x.Client.Nip)
                .SetValidator(new NipValidator());

            RuleFor(x => x.Client.Address)
                .NotEmpty();

            // Przydałaby się walidacja, aby były same cyfry oraz '+', ale na potrzeby demo pomijam.
            RuleFor(x => x.Client.Email)
                .EmailAddress()
                .When(x => !String.IsNullOrWhiteSpace(x.Client.Email));
        }
    }
}
