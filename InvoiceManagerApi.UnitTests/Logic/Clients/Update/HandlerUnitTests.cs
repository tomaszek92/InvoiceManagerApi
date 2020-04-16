using AutoFixture;
using FluentAssertions;
using InvoiceManagerApi.Logic.Clients.Update;
using InvoiceManagerApi.Models;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace InvoiceManagerApi.UnitTests.Logic.Clients.Update
{
    public class HandlerUnitTests : ApplicationDbContextUnitTests
    {
        private readonly Handler _handler;

        public HandlerUnitTests()
        {
            _handler = new Handler(DbContext);
        }

        [Fact]
        public async Task Should_update_client_if_client_exist_in_db()
        {
            // Arrange
            var clients = Fixture.CreateMany<Client>().ToList();
            await DbContext.Clients.AddRangeAsync(clients);
            await DbContext.SaveChangesAsync();

            var clientToUpdate = clients[clients.Count - 1];

            // Act
            var updatedClient = await _handler.Handle(new Command(clientToUpdate), CancellationToken.None);

            // Assert
            updatedClient
                .Should()
                .BeEquivalentTo(clientToUpdate);

            var dbClient = await DbContext
                .Clients
                .FirstOrDefaultAsync(c => c.Id == updatedClient.Id);

            dbClient
                .Should()
                .BeEquivalentTo(clientToUpdate);
        }

        [Fact]
        public async Task Should_not_update_client_if_client_not_exist_in_db()
        {
            // Arrange
            var clients = Fixture.CreateMany<Client>().ToList();
            await DbContext.Clients.AddRangeAsync(clients);
            await DbContext.SaveChangesAsync();

            var clientToUpdate = Fixture.Create<Client>();

            // Act
            var updatedClient = await _handler.Handle(new Command(clientToUpdate), CancellationToken.None);

            // Assert
            updatedClient.ShouldBeNull();
        }
    }
}
