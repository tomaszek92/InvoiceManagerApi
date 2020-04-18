using AutoFixture;
using FluentValidation.TestHelper;
using FluentValidation.Validators;
using InvoiceManagerApi.Logic.Invoices.Validators;
using InvoiceManagerApi.Models;
using Xunit;

namespace InvoiceManagerApi.UnitTests.Logic.Invoices.Validators
{
    public class InvoiceRowValidatorUnitTests
    {
        private readonly InvoiceRowValidator _validator = new InvoiceRowValidator();
        private readonly IFixture _fixture = UnitTestFixture.Get();

        [Fact]
        public void Should_not_have_error_if_name_is_not_empty()
        {
            // Arrange
            var invoiceRow = _fixture
                .Build<InvoiceRow>()
                .With(ir => ir.Name, _fixture.Create<string>())
                .Create();

            // Act & Assert
            _validator
                .TestValidate(invoiceRow)
                .ShouldNotHaveValidationErrorFor(x => x.Name);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void Should_have_error_if_name_is_empty(string name)
        {
            // Arrange
            var invoiceRow = _fixture
                .Build<InvoiceRow>()
                .With(ir => ir.Name, name)
                .Create();

            // Act & Assert
            _validator
                .TestValidate(invoiceRow)
                .ShouldHaveValidationErrorFor(x => x.Name)
                .WithErrorCode(nameof(NotEmptyValidator));
        }

        [Fact]
        public void Should_not_have_error_if_count_is_greater_or_equal_to_1()
        {
            // Arrange
            var invoiceRow = _fixture
                .Build<InvoiceRow>()
                .With(ir => ir.Count, 1)
                .Create();

            // Act & Assert
            _validator
                .TestValidate(invoiceRow)
                .ShouldNotHaveValidationErrorFor(x => x.Count);
        }

        [Fact]
        public void Should_have_error_if_count_is_less_than_0()
        {
            // Arrange
            var invoiceRow = _fixture
                .Build<InvoiceRow>()
                .With(ir => ir.Count, 0)
                .Create();

            // Act & Assert
            _validator
                .TestValidate(invoiceRow)
                .ShouldHaveValidationErrorFor(x => x.Count)
                .WithErrorCode(nameof(GreaterThanOrEqualValidator));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(50)]
        [InlineData(100)]
        public void Should_not_have_error_if_vat_rate_is_greater_or_equal_to_0_and_less_or_equal_to_100(int vatRate)
        {
            // Arrange
            var invoiceRow = _fixture
                .Build<InvoiceRow>()
                .With(ir => ir.VatRate, vatRate)
                .Create();

            // Act & Assert
            _validator
                .TestValidate(invoiceRow)
                .ShouldNotHaveValidationErrorFor(x => x.VatRate);
        }

        [Fact]
        public void Should_have_error_if_vat_rate_is_less_than_0()
        {
            // Arrange
            var invoiceRow = _fixture
                .Build<InvoiceRow>()
                .With(ir => ir.VatRate, -1)
                .Create();

            // Act & Assert
            _validator
                .TestValidate(invoiceRow)
                .ShouldHaveValidationErrorFor(x => x.VatRate)
                .WithErrorCode(nameof(GreaterThanOrEqualValidator));
        }

        [Fact]
        public void Should_have_error_if_vat_rate_is_greater_than_100()
        {
            // Arrange
            var invoiceRow = _fixture
                .Build<InvoiceRow>()
                .With(ir => ir.VatRate, 101)
                .Create();

            // Act & Assert
            _validator
                .TestValidate(invoiceRow)
                .ShouldHaveValidationErrorFor(x => x.VatRate)
                .WithErrorCode(nameof(LessThanOrEqualValidator));
        }

        [Fact]
        public void Should_not_have_error_if_unit_price_net_is_greater_than_0()
        {
            // Arrange
            var invoiceRow = _fixture
                .Build<InvoiceRow>()
                .With(ir => ir.UnitPriceNet, 1)
                .Create();

            // Act & Assert
            _validator
                .TestValidate(invoiceRow)
                .ShouldNotHaveValidationErrorFor(x => x.UnitPriceNet);
        }

        [Fact]
        public void Should_have_error_if_unit_price_net_is_less_than_or_equal_to_0()
        {
            // Arrange
            var invoiceRow = _fixture
                .Build<InvoiceRow>()
                .With(ir => ir.UnitPriceNet, 0)
                .Create();

            // Act & Assert
            _validator
                .TestValidate(invoiceRow)
                .ShouldHaveValidationErrorFor(x => x.UnitPriceNet)
                .WithErrorCode(nameof(GreaterThanValidator));
        }
    }
}
