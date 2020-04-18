using AutoFixture;
using FluentValidation.TestHelper;
using FluentValidation.Validators;
using InvoiceManagerApi.Logic.Clients.Validators;
using InvoiceManagerApi.Logic.Invoices.Create;
using InvoiceManagerApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace InvoiceManagerApi.UnitTests.Logic.Invoices.Create
{
    public class ValidatorUnitTests : ApplicationDbContextUnitTests
    {
        private readonly Validator _validator;

        public ValidatorUnitTests()
        {
            _validator = new Validator(DbContext);
        }

        [Fact]
        public void Should_not_have_error_if_id_is_empty()
        {
            // Arrange
            var invoice = Fixture
                .Build<Invoice>()
                .With(invoice => invoice.Id, default(int))
                .Create();

            var command = new Command(invoice);

            // Act & Assert
            _validator
                .TestValidate(command)
                .ShouldNotHaveValidationErrorFor(x => x.Invoice.Id);
        }

        [Fact]
        public void Should_have_error_if_id_is_not_empty()
        {
            // Arrange
            var invoice = Fixture
                .Build<Invoice>()
                .With(invoice => invoice.Id, Fixture.Create<int>())
                .Create();

            var command = new Command(invoice);

            // Act & Assert
            _validator
                .TestValidate(command)
                .ShouldHaveValidationErrorFor(x => x.Invoice.Id)
                .WithErrorCode(nameof(EmptyValidator));
        }

        [Fact]
        public void Should_not_have_error_if_year_is_greater_or_equal_to_2000()
        {
            // Arrange
            var invoice = Fixture
                .Build<Invoice>()
                .With(invoice => invoice.Year, 2000)
                .Create();

            var command = new Command(invoice);

            // Act & Assert
            _validator
                .TestValidate(command)
                .ShouldNotHaveValidationErrorFor(x => x.Invoice.Year);
        }

        [Fact]
        public void Should_have_error_if_year_is_less_than_2000()
        {
            // Arrange
            var invoice = Fixture
                .Build<Invoice>()
                .With(invoice => invoice.Year, 1999)
                .Create();

            var command = new Command(invoice);

            // Act & Assert
            _validator
                .TestValidate(command)
                .ShouldHaveValidationErrorFor(x => x.Invoice.Year)
                .WithErrorCode(nameof(GreaterThanOrEqualValidator));
        }

        [Fact]
        public void Should_not_have_error_if_month_is_in_enum()
        {
            // Arrange
            var invoice = Fixture
                .Build<Invoice>()
                .With(invoice => invoice.Month, Fixture.Create<Month>())
                .Create();

            var command = new Command(invoice);

            // Act & Assert
            _validator
                .TestValidate(command)
                .ShouldNotHaveValidationErrorFor(x => x.Invoice.Month);
        }

        [Fact]
        public void Should_have_error_if_month_is_not_in_enum()
        {
            // Arrange
            var invoice = Fixture
                .Build<Invoice>()
                .With(invoice => invoice.Month, EnumHelper.GetInvalidValue<Month>())
                .Create();

            var command = new Command(invoice);

            // Act & Assert
            _validator
                .TestValidate(command)
                .ShouldHaveValidationErrorFor(x => x.Invoice.Month)
                .WithErrorCode(nameof(EnumValidator));
        }

        [Fact]
        public void Should_not_have_error_if_number_is_greater_or_equal_to_1()
        {
            // Arrange
            var invoice = Fixture
                .Build<Invoice>()
                .With(invoice => invoice.Number, 1)
                .Create();

            var command = new Command(invoice);

            // Act & Assert
            _validator
                .TestValidate(command)
                .ShouldNotHaveValidationErrorFor(x => x.Invoice.Number);
        }

        [Fact]
        public void Should_have_error_if_number_is_less_than_1()
        {
            // Arrange
            var invoice = Fixture
                .Build<Invoice>()
                .With(invoice => invoice.Number, 0)
                .Create();

            var command = new Command(invoice);

            // Act & Assert
            _validator
                .TestValidate(command)
                .ShouldHaveValidationErrorFor(x => x.Invoice.Number)
                .WithErrorCode(nameof(GreaterThanOrEqualValidator));
        }

        [Fact]
        public async Task Should_not_have_error_if_client_id_is_defiend_in_db()
        {
            // Arrange
            var client = Fixture.Create<Client>();
            await DbContext.Clients.AddAsync(client);
            await DbContext.SaveChangesAsync();

            var invoice = Fixture
                .Build<Invoice>()
                .With(invoice => invoice.ClientId, client.Id)
                .Create();

            var command = new Command(invoice);

            // Act & Assert
            _validator
                .TestValidate(command)
                .ShouldNotHaveValidationErrorFor(x => x.Invoice.ClientId);
        }

        [Fact]
        public void Should_have_error_if_client_id_is_not_defined_in_db()
        {
            // Arrange
            var invoice = Fixture
                .Build<Invoice>()
                .With(invoice => invoice.ClientId, Fixture.Create<int>())
                .Create();

            var command = new Command(invoice);

            // Act & Assert
            _validator
                .TestValidate(command)
                .ShouldHaveValidationErrorFor(x => x.Invoice.ClientId)
                .WithErrorCode(nameof(ClientIsDefinedValidator));
        }

        [Fact]
        public void Should_not_have_error_if_paytime_is_greater_or_equal_to_1()
        {
            // Arrange
            var invoice = Fixture
                .Build<Invoice>()
                .With(invoice => invoice.Paytime, 1)
                .Create();

            var command = new Command(invoice);

            // Act & Assert
            _validator
                .TestValidate(command)
                .ShouldNotHaveValidationErrorFor(x => x.Invoice.Paytime);
        }

        [Fact]
        public void Should_have_error_if_paytime_is_less_than_1()
        {
            // Arrange
            var invoice = Fixture
                .Build<Invoice>()
                .With(invoice => invoice.Paytime, 0)
                .Create();

            var command = new Command(invoice);

            // Act & Assert
            _validator
                .TestValidate(command)
                .ShouldHaveValidationErrorFor(x => x.Invoice.Paytime)
                .WithErrorCode(nameof(GreaterThanOrEqualValidator));
        }

        [Fact]
        public void Should_not_have_error_if_payment_type_is_in_enum()
        {
            // Arrange
            var invoice = Fixture
                .Build<Invoice>()
                .With(invoice => invoice.PaymentType, Fixture.Create<PaymentType>())
                .Create();

            var command = new Command(invoice);

            // Act & Assert
            _validator
                .TestValidate(command)
                .ShouldNotHaveValidationErrorFor(x => x.Invoice.PaymentType);
        }

        [Fact]
        public void Should_have_error_if_payment_type_is_not_in_enum()
        {
            // Arrange
            var invoice = Fixture
                .Build<Invoice>()
                .With(invoice => invoice.PaymentType, EnumHelper.GetInvalidValue<PaymentType>())
                .Create();

            var command = new Command(invoice);

            // Act & Assert
            _validator
                .TestValidate(command)
                .ShouldHaveValidationErrorFor(x => x.Invoice.PaymentType)
                .WithErrorCode(nameof(EnumValidator));
        }

        [Fact]
        public void Should_not_have_error_if_rows_count_is_greater_or_equal_to_1()
        {
            // Arrange
            var invoice = Fixture
                .Build<Invoice>()
                .With(invoice => invoice.Rows, Fixture.CreateMany<InvoiceRow>().ToList())
                .Create();

            var command = new Command(invoice);

            // Act & Assert
            _validator
                .TestValidate(command)
                .ShouldNotHaveValidationErrorFor(x => x.Invoice.Rows.Count);
        }

        [Fact]
        public void Should_have_error_if_rows_count_is_less_than_1()
        {
            // Arrange
            var invoice = Fixture
                .Build<Invoice>()
                .With(invoice => invoice.Rows, new List<InvoiceRow>())
                .Create();

            var command = new Command(invoice);

            // Act & Assert
            _validator
                .TestValidate(command)
                .ShouldHaveValidationErrorFor(x => x.Invoice.Rows.Count)
                .WithErrorCode(nameof(GreaterThanOrEqualValidator));
        }
    }
}
