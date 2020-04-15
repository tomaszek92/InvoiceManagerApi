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
    public class HandlerUnitTests : IDisposable
    {
        private readonly Fixture _fixture = new Fixture();
        private readonly TestApplicationDbContextProvider _testApplicationDbContextProvider = new TestApplicationDbContextProvider();
        private readonly IApplicationDbContext _dbContext;
        private readonly Handler _handler;

        public HandlerUnitTests()
        {
            _dbContext = _testApplicationDbContextProvider.DbContextInstance;
            _handler = new Handler(_dbContext);
        }

        [Fact]
        public async Task should_get_all_clients()
        {
            // Act
            var expectedClients = _fixture.CreateMany<Client>().ToList();
            await _dbContext.Clients.AddRangeAsync(expectedClients);
            await _dbContext.SaveChangesAsync();

            // Act
            var dbClients = await _handler.Handle(new Query(), CancellationToken.None);

            // Assert
            dbClients
                .Should()
                .BeEquivalentTo(expectedClients);
        }

        public void Dispose()
        {
            _testApplicationDbContextProvider.Dispose();
        }
    }
}
