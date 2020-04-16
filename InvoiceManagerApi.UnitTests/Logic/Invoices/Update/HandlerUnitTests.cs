using AutoFixture;
using FluentAssertions;
using InvoiceManagerApi.Logic.Invoices.Update;
using InvoiceManagerApi.Models;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace InvoiceManagerApi.UnitTests.Logic.Invoices.Update
{
    public class HandlerUnitTests : ApplicationDbContextUnitTests
    {
        private readonly Handler _handler;

        public HandlerUnitTests()
        {
            _handler = new Handler(DbContext);
        }

        [Fact]
        public async Task Should_update_invoice_if_invoice_exist_in_db()
        {
            // Arrange
            var invoices = Fixture.CreateMany<Invoice>().ToList();
            await DbContext.Invoices.AddRangeAsync(invoices);
            await DbContext.SaveChangesAsync();

            var invoiceToUpdate = invoices[invoices.Count - 1];

            // Act
            var updatedInvoice = await _handler.Handle(new Command(invoiceToUpdate), CancellationToken.None);

            // Assert
            updatedInvoice
                .Should()
                .BeEquivalentTo(invoiceToUpdate);

            var dbInvoice = await DbContext
                .Invoices
                .FirstOrDefaultAsync(c => c.Id == updatedInvoice.Id);

            dbInvoice
                .Should()
                .BeEquivalentTo(invoiceToUpdate);
        }

        [Fact]
        public async Task Should_not_update_invoice_if_invoice_not_exist_in_db()
        {
            // Arrange
            var invoices = Fixture.CreateMany<Invoice>().ToList();
            await DbContext.Invoices.AddRangeAsync(invoices);
            await DbContext.SaveChangesAsync();

            var invoiceToUpdate = Fixture.Create<Invoice>();

            // Act
            var updatedInvoice = await _handler.Handle(new Command(invoiceToUpdate), CancellationToken.None);

            // Assert
            updatedInvoice.ShouldBeNull();
        }
    }
}
