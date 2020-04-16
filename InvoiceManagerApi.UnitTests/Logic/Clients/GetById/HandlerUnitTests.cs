using AutoFixture;
using FluentAssertions;
using InvoiceManagerApi.Logic.Clients.GetById;
using InvoiceManagerApi.Models;
using Shouldly;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace InvoiceManagerApi.UnitTests.Logic.Clients.GetById
{
    public class HandlerUnitTests : ApplicationDbContextUnitTests
    {
        private readonly Handler _handler;

        public HandlerUnitTests()
        {
            _handler = new Handler(DbContext);
        }

        [Fact]
        public async Task Should_get_client_by_id_if_client_exist_in_db()
        {
            // Act
            var clients = Fixture.CreateMany<Client>().ToList();
            await DbContext.Clients.AddRangeAsync(clients);
            await DbContext.SaveChangesAsync();

            var expectedClient = clients[clients.Count - 1];

            // Act
            var dbClient = await _handler.Handle(new Query(expectedClient.Id), CancellationToken.None);

            // Assert
            dbClient
                .Should()
                .BeEquivalentTo(expectedClient);
        }

        [Fact]
        public async Task Should_not_get_client_by_id_if_client_not_exist_in_db()
        {
            // Act
            var clients = Fixture.CreateMany<Client>().ToList();
            await DbContext.Clients.AddRangeAsync(clients);
            await DbContext.SaveChangesAsync();

            var notExistingId = Fixture.Create<int>();

            // Act
            var dbClient = await _handler.Handle(new Query(notExistingId), CancellationToken.None);

            // Assert
            dbClient.ShouldBeNull();
        }
    }
}
