using AutoFixture;
using FluentAssertions;
using InvoiceManagerApi.Logic.Invoices.Delete;
using InvoiceManagerApi.Models;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace InvoiceManagerApi.UnitTests.Logic.Invoices.Delete
{
    public class HandlerUnitTests : ApplicationDbContextUnitTests
    {
        private readonly Handler _handler;

        public HandlerUnitTests()
        {
            _handler = new Handler(DbContext);
        }

        [Fact]
        public async Task Should_delete_invoice_by_id_if_invoice_exist_in_db()
        {
            // Act
            var invoices = Fixture.CreateMany<Invoice>().ToList();
            await DbContext.Invoices.AddRangeAsync(invoices);
            await DbContext.SaveChangesAsync();

            var invoiceToDelete = invoices[invoices.Count - 1];

            // Act
            var deletedInvoice = await _handler.Handle(new Command(invoiceToDelete.Id), CancellationToken.None);

            // Assert
            deletedInvoice
                .Should()
                .BeEquivalentTo(invoiceToDelete);

            var doesExistInDb = await DbContext.Invoices.AnyAsync(invoice => invoice.Id == invoiceToDelete.Id);
            doesExistInDb.ShouldBeFalse();
        }

        [Fact]
        public async Task Should_not_delete_invoice_by_id_if_invoice_not_exist_in_db()
        {
            // Act
            var invoices = Fixture.CreateMany<Invoice>().ToList();
            await DbContext.Invoices.AddRangeAsync(invoices);
            await DbContext.SaveChangesAsync();

            var notExistingId = Fixture.Create<int>();

            // Act
            var deletedInvoice = await _handler.Handle(new Command(notExistingId), CancellationToken.None);

            // Assert
            deletedInvoice.ShouldBeNull();
        }
    }
}
