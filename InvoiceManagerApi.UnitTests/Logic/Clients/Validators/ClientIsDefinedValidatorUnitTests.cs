using AutoFixture;
using InvoiceManagerApi.Logic.Clients.Validators;
using InvoiceManagerApi.Models;
using Shouldly;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace InvoiceManagerApi.UnitTests.Logic.Clients.Validators
{
    public class ClientIsDefinedValidatorUnitTests : ApplicationDbContextUnitTests
    {
        private readonly ClientIsDefinedValidator _clientIsDefinedValidator;

        public ClientIsDefinedValidatorUnitTests()
        {
            _clientIsDefinedValidator = new ClientIsDefinedValidator(DbContext);
        }

        [Fact]
        public async Task Should_be_valid()
        {
            // Arrange
            var clients = Fixture.CreateMany<Client>().ToList();
            await DbContext.Clients.AddRangeAsync(clients);
            await DbContext.SaveChangesAsync();

            // Act
            var isValid = await _clientIsDefinedValidator.IsValidAsync(clients.GetRandom().Id, CancellationToken.None);

            // Assert
            isValid.ShouldBeTrue();
        }

        [Fact]
        public async Task Should_not_be_valid()
        {
            // Arrange
            var clients = Fixture.CreateMany<Client>().ToList();
            await DbContext.Clients.AddRangeAsync(clients);
            await DbContext.SaveChangesAsync();

            // Act
            var isValid = await _clientIsDefinedValidator.IsValidAsync(Fixture.Create<int>(), CancellationToken.None);

            // Assert
            isValid.ShouldBeFalse();
        }
    }
}
