using FluentValidation;
using InvoiceManagerApi.Logic.Validators;
using System;

namespace InvoiceManagerApi.Logic.Clients.Update
{
    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(x => x.Client.Id)
                .NotEmpty();

            RuleFor(x => x.Client.Name)
                .NotEmpty();

            RuleFor(x => x.Client.Nip)
                .SetValidator(new NipValidator());

            RuleFor(x => x.Client.Address)
                .NotEmpty();

            RuleFor(x => x.Client.Email)
                .EmailAddress()
                .When(x => !String.IsNullOrWhiteSpace(x.Client.Email));
        }
    }
}
