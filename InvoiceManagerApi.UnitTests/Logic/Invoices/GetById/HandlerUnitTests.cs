using AutoFixture;
using FluentAssertions;
using InvoiceManagerApi.Logic.Invoices.GetById;
using InvoiceManagerApi.Models;
using Shouldly;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace InvoiceManagerApi.UnitTests.Logic.Invoices.GetById
{
    public class HandlerUnitTests : ApplicationDbContextUnitTests
    {
        private readonly Handler _handler;

        public HandlerUnitTests()
        {
            _handler = new Handler(DbContext);
        }

        [Fact]
        public async Task Should_get_invoice_by_id_if_invoice_exist_in_db()
        {
            // Act
            var invoices = Fixture.CreateMany<Invoice>().ToList();
            await DbContext.Invoices.AddRangeAsync(invoices);
            await DbContext.SaveChangesAsync();

            var expectedInvoice = invoices[invoices.Count - 1];

            // Act
            var dbInvoice = await _handler.Handle(new Query(expectedInvoice.Id), CancellationToken.None);

            // Assert
            dbInvoice
                .Should()
                .BeEquivalentTo(expectedInvoice);
        }

        [Fact]
        public async Task Should_not_get_invoice_by_id_if_invoice_not_exist_in_db()
        {
            // Act
            var invoices = Fixture.CreateMany<Invoice>().ToList();
            await DbContext.Invoices.AddRangeAsync(invoices);
            await DbContext.SaveChangesAsync();

            var notExistingId = Fixture.Create<int>();

            // Act
            var dbInvoice = await _handler.Handle(new Query(notExistingId), CancellationToken.None);

            // Assert
            dbInvoice.ShouldBeNull();
        }
    }
}
