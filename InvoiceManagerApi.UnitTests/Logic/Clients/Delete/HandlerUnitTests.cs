using AutoFixture;
using FluentAssertions;
using InvoiceManagerApi.Logic.Clients.Delete;
using InvoiceManagerApi.Models;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace InvoiceManagerApi.UnitTests.Logic.Clients.Delete
{
    public class HandlerUnitTests : ApplicationDbContextUnitTests
    {
        private readonly Handler _handler;

        public HandlerUnitTests()
        {
            _handler = new Handler(DbContext);
        }

        [Fact]
        public async Task Should_delete_client_by_id_if_client_exist_in_db()
        {
            // Act
            var clients = Fixture.CreateMany<Client>().ToList();
            await DbContext.Clients.AddRangeAsync(clients);
            await DbContext.SaveChangesAsync();

            var clientToDelete = clients[clients.Count - 1];

            // Act
            var deletedClient = await _handler.Handle(new Command(clientToDelete.Id), CancellationToken.None);

            // Assert
            deletedClient
                .Should()
                .BeEquivalentTo(clientToDelete);

            var doesExistInDb = await DbContext.Clients.AnyAsync(client => client.Id == clientToDelete.Id);
            doesExistInDb.ShouldBeFalse();
        }

        [Fact]
        public async Task Should_not_delete_client_by_id_if_client_not_exist_in_db()
        {
            // Act
            var clients = Fixture.CreateMany<Client>().ToList();
            await DbContext.Clients.AddRangeAsync(clients);
            await DbContext.SaveChangesAsync();

            var notExistingId = Fixture.Create<int>();

            // Act
            var deletedClient = await _handler.Handle(new Command(notExistingId), CancellationToken.None);

            // Assert
            deletedClient.ShouldBeNull();
        }
    }
}
