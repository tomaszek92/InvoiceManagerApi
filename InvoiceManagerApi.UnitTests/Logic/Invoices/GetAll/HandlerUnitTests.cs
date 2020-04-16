using AutoFixture;
using FluentAssertions;
using InvoiceManagerApi.Logic.Invoices.GetAll;
using InvoiceManagerApi.Models;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace InvoiceManagerApi.UnitTests.Logic.Invoices.GetAll
{
    public class HandlerUnitTests : ApplicationDbContextUnitTests
    {
        private readonly Handler _handler;

        public HandlerUnitTests()
        {
            _handler = new Handler(DbContext);
        }

        [Fact]
        public async Task Should_get_all_invoices()
        {
            // Act
            var expectedInvoices = Fixture.CreateMany<Invoice>().ToList();
            await DbContext.Invoices.AddRangeAsync(expectedInvoices);
            await DbContext.SaveChangesAsync();

            // Act
            var dbInvoices = await _handler.Handle(new Query(), CancellationToken.None);

            // Assert
            dbInvoices
                .Should()
                .BeEquivalentTo(expectedInvoices);
        }
    }
}
