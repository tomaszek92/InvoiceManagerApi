using InvoiceManagerApi.Logic.Validators;
using Shouldly;
using Xunit;

namespace InvoiceManagerApi.UnitTests.Logic.Validators
{
    public class NipValidatorUnitTests
    {
        private readonly NipValidator _validator = new NipValidator();

        [Theory]
        [InlineData("7622654927")]
        [InlineData("5311682493")]
        [InlineData("3892726480")]
        [InlineData("5249366383")]
        public void Should_be_valid(string nip)
        {
            // Act
            var isValid = _validator.IsValid(nip);

            // Assert
            isValid.ShouldBeTrue();
        }

        [Theory]
        [InlineData("5249116383")]
        [InlineData("11493116383")]
        [InlineData("531168249")]
        [InlineData("")]
        [InlineData(null)]
        public void Should_not_be_valid(string nip)
        {
            // Act
            var isValid = _validator.IsValid(nip);

            // Assert
            isValid.ShouldBeFalse();
        }
    }
}
