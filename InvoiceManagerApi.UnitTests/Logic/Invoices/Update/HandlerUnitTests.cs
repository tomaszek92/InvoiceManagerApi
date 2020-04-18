using AutoFixture;
using FluentAssertions;
using InvoiceManagerApi.Logic.Invoices.Update;
using InvoiceManagerApi.Models;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using System.Collections.Generic;
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
        public async Task Should_update_invoice_row_if_id_is_not_empty()
        {
            // Arrange
            var invoiceRow = Fixture
                .Build<InvoiceRow>()
                .Without(ir => ir.Invoice)
                .Create();

            var invoice = Fixture
                .Build<Invoice>()
                .With(i => i.Rows, new List<InvoiceRow> { invoiceRow })
                .Create();

            invoiceRow.InvoiceId = invoice.Id;

            await DbContext.Invoices.AddAsync(invoice);
            await DbContext.SaveChangesAsync();
            DbContext.Entry(invoice).State = EntityState.Detached;

            var invoiceRowToUpdate = Fixture
                .Build<InvoiceRow>()
                .With(ir => ir.Id, invoiceRow.Id)
                .Create();

            var invoiceToUpdate = Fixture
                .Build<Invoice>()
                .With(i => i.Id, invoice.Id)
                .With(i => i.Rows, new List<InvoiceRow> { invoiceRowToUpdate })
                .Create();

            // Act
            var updatedInvoice = await _handler.Handle(new Command(invoiceToUpdate), CancellationToken.None);

            updatedInvoice
                .Rows
                .ShouldHaveSingleItem()
                .Should()
                .BeEquivalentTo(invoiceRowToUpdate);

            var dbInvoice = await DbContext
                .Invoices
                .FirstOrDefaultAsync(c => c.Id == updatedInvoice.Id);

            dbInvoice
                .Rows
                .ShouldHaveSingleItem()
                .Should()
                .BeEquivalentTo(invoiceRowToUpdate);
        }

        [Fact]
        public async Task Should_add_invoice_row_if_id_is_empty()
        {
            // Arrange
            var invoice = Fixture
                .Build<Invoice>()
                .Without(i => i.Rows)
                .Create();

            await DbContext.Invoices.AddAsync(invoice);
            await DbContext.SaveChangesAsync();
            DbContext.Entry(invoice).State = EntityState.Detached;

            var invoiceToUpdate = Fixture
                .Build<Invoice>()
                .With(i => i.Id, invoice.Id)
                .Create();

            invoiceToUpdate.Rows.ToList().ForEach(ir => ir.Id = default);

            // Act
            var updatedInvoice = await _handler.Handle(new Command(invoiceToUpdate), CancellationToken.None);

            updatedInvoice
                .Rows
                .Should()
                .BeEquivalentTo(invoiceToUpdate.Rows);

            var dbInvoice = await DbContext
                .Invoices
                .FirstOrDefaultAsync(c => c.Id == updatedInvoice.Id);

            dbInvoice
                .Rows
                .Should()
                .BeEquivalentTo(invoiceToUpdate.Rows);
        }

        [Fact]
        public async Task Should_delete_invoice_row_if_there_is_no_matching_row()
        {
            // Arrange
            var invoice = Fixture.Create<Invoice>();
            await DbContext.Invoices.AddAsync(invoice);
            await DbContext.SaveChangesAsync();
            DbContext.Entry(invoice).State = EntityState.Detached;

            var invoiceToUpdate = Fixture
                .Build<Invoice>()
                .With(i => i.Id, invoice.Id)
                .Without(i => i.Rows)
                .Create();

            // Act
            var updatedInvoice = await _handler.Handle(new Command(invoiceToUpdate), CancellationToken.None);

            updatedInvoice
                .Rows
                .ShouldBeEmpty();

            var dbInvoice = await DbContext
                .Invoices
                .FirstOrDefaultAsync(c => c.Id == updatedInvoice.Id);

            dbInvoice
                .Rows
                .ShouldBeEmpty();
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
