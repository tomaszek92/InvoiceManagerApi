using FluentValidation;
using FluentValidation.Validators;
using InvoiceManagerApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace InvoiceManagerApi.Logic.Clients.Validators
{
    public class ClientIsDefinedValidator : PropertyValidator
    {
        public const string ErrorMessage = "Client with given id is not defined";

        private readonly IApplicationDbContext _dbContext;

        public ClientIsDefinedValidator(IApplicationDbContext dDbContext) : base(ErrorMessage)
        {
            _dbContext = dDbContext;
        }

        public override bool ShouldValidateAsync(ValidationContext context) => true;

        protected override bool IsValid(PropertyValidatorContext context) => throw new System.NotImplementedException();

        protected override Task<bool> IsValidAsync(PropertyValidatorContext context, CancellationToken cancellationToken)
        {
            var clientId = (int)context.PropertyValue;

            return IsValidAsync(clientId, cancellationToken);
        }

        public void IsValidAsync<T>(T t)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> IsValidAsync(int clientId, CancellationToken cancellationToken)
        {
            return await _dbContext
                .Clients
                .AnyAsync(client => client.Id == clientId, cancellationToken);
        }
    }
}
