using AutoFixture;
using FluentAssertions;
using InvoiceManagerApi.Logic.Clients.Create;
using InvoiceManagerApi.Models;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace InvoiceManagerApi.UnitTests.Logic.Clients.Create
{
    public class HandlerUnitTests : ApplicationDbContextUnitTests
    {
        private readonly Handler _handler;

        public HandlerUnitTests()
        {
            _handler = new Handler(DbContext);
        }

        [Fact]
        public async Task Should_create_client()
        {
            // Arrange
            var clientToAdd = Fixture.Create<Client>();

            // Act
            var addedClient = await _handler.Handle(new Command(clientToAdd), CancellationToken.None);

            // Assert
            addedClient.Id.ShouldNotBe(default);

            addedClient
                .Should()
                .BeEquivalentTo(clientToAdd);

            var dbClient = await DbContext
                .Clients
                .FirstOrDefaultAsync(c => c.Id == addedClient.Id);

            dbClient
                .Should()
                .BeEquivalentTo(addedClient);
        }
    }
}
