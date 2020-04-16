using AutoFixture;
using FluentAssertions;
using InvoiceManagerApi.Logic.Invoices.Create;
using InvoiceManagerApi.Models;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace InvoiceManagerApi.UnitTests.Logic.Invoices.Create
{
    public class HandlerUnitTests : ApplicationDbContextUnitTests
    {
        private readonly Handler _handler;

        public HandlerUnitTests()
        {
            _handler = new Handler(DbContext);
        }

        [Fact]
        public async Task Should_create_invoice()
        {
            // Arrange
            var invoiceToAdd = Fixture.Create<Invoice>();

            // Act
            var addedInvoice = await _handler.Handle(new Command(invoiceToAdd), CancellationToken.None);

            // Assert
            addedInvoice.Id.ShouldNotBe(default);

            addedInvoice
                .Should()
                .BeEquivalentTo(invoiceToAdd);

            var dbInvoice = await DbContext
                .Invoices
                .FirstOrDefaultAsync(i => i.Id == addedInvoice.Id);

            dbInvoice
                .Should()
                .BeEquivalentTo(addedInvoice);
        }
    }
}
