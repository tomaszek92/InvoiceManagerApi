using AutoFixture;
using FluentAssertions;
using InvoiceManagerApi.Logic.Clients.GetAll;
using InvoiceManagerApi.Models;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace InvoiceManagerApi.UnitTests.Logic.Clients.GetAll
{
    public class HandlerUnitTests : ApplicationDbContextUnitTests
    {
        private readonly Handler _handler;

        public HandlerUnitTests()
        {
            _handler = new Handler(DbContext);
        }

        [Fact]
        public async Task Should_get_all_clients()
        {
            // Act
            var expectedClients = Fixture.CreateMany<Client>().ToList();
            await DbContext.Clients.AddRangeAsync(expectedClients);
            await DbContext.SaveChangesAsync();

            // Act
            var dbClients = await _handler.Handle(new Query(), CancellationToken.None);

            // Assert
            dbClients
                .Should()
                .BeEquivalentTo(expectedClients);
        }
    }
}
