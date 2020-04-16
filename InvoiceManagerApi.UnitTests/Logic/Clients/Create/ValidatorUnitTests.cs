using AutoFixture;
using FluentValidation.TestHelper;
using FluentValidation.Validators;
using InvoiceManagerApi.Logic.Clients.Create;
using InvoiceManagerApi.Logic.Validators;
using InvoiceManagerApi.Models;
using Xunit;

namespace InvoiceManagerApi.UnitTests.Logic.Clients.Create
{
    public class ValidatorUnitTests
    {
        private readonly Validator _validator = new Validator();
        private readonly IFixture _fixture = UnitTestFixture.Get();

        [Fact]
        public void Should_not_have_error_if_id_is_empty()
        {
            // Arrange
            var client = _fixture
                .Build<Client>()
                .With(client => client.Id, default(int))
                .Create();

            var command = new Command(client);

            // Act & Assert
            _validator
                .TestValidate(command)
                .ShouldNotHaveValidationErrorFor(x => x.Client.Id);
        }

        [Fact]
        public void Should_have_error_if_id_is_not_empty()
        {
            // Arrange
            var client = _fixture
                .Build<Client>()
                .With(client => client.Id, _fixture.Create<int>())
                .Create();

            var command = new Command(client);

            // Act & Assert
            _validator
                .TestValidate(command)
                .ShouldHaveValidationErrorFor(x => x.Client.Id)
                .WithErrorCode(nameof(EmptyValidator));
        }

        [Fact]
        public void Should_not_have_error_if_name_is_not_empty()
        {
            // Arrange
            var client = _fixture
                .Build<Client>()
                .With(client => client.Name, _fixture.Create<string>())
                .Create();

            var command = new Command(client);

            // Act & Assert
            _validator
                .TestValidate(command)
                .ShouldNotHaveValidationErrorFor(x => x.Client.Name);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void Should_have_error_if_name_is_empty(string name)
        {
            // Arrange
            var client = _fixture
                .Build<Client>()
                .With(client => client.Name, name)
                .Create();

            var command = new Command(client);

            // Act & Assert
            _validator
                .TestValidate(command)
                .ShouldHaveValidationErrorFor(x => x.Client.Name)
                .WithErrorCode(nameof(NotEmptyValidator));
        }

        [Fact]
        public void Should_not_have_error_if_nip_is_valid()
        {
            // Arrange
            var client = _fixture
                .Build<Client>()
                .With(client => client.Nip, "1072143790")
                .Create();

            var command = new Command(client);

            // Act & Assert
            _validator
                .TestValidate(command)
                .ShouldNotHaveValidationErrorFor(x => x.Client.Nip);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void Should_have_error_if_nip_is_empty(string nip)
        {
            // Arrange
            var client = _fixture
                .Build<Client>()
                .With(client => client.Nip, nip)
                .Create();

            var command = new Command(client);

            // Act & Assert
            _validator
                .TestValidate(command)
                .ShouldHaveValidationErrorFor(x => x.Client.Nip)
                .WithErrorCode(nameof(NipValidator));
        }

        [Fact]
        public void Should_not_have_error_if_address_is_not_empty()
        {
            // Arrange
            var client = _fixture
                .Build<Client>()
                .With(client => client.Address, _fixture.Create<string>())
                .Create();

            var command = new Command(client);

            // Act & Assert
            _validator
                .TestValidate(command)
                .ShouldNotHaveValidationErrorFor(x => x.Client.Address);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void Should_have_error_if_address_is_empty(string address)
        {
            // Arrange
            var client = _fixture
                .Build<Client>()
                .With(client => client.Address, address)
                .Create();

            var command = new Command(client);

            // Act & Assert
            _validator
                .TestValidate(command)
                .ShouldHaveValidationErrorFor(x => x.Client.Address)
                .WithErrorCode(nameof(NotEmptyValidator));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void Should_not_have_error_if_email_is_empty(string email)
        {
            // Arrange
            var client = _fixture
                .Build<Client>()
                .With(client => client.Email, email)
                .Create();

            var command = new Command(client);

            // Act & Assert
            _validator
                .TestValidate(command)
                .ShouldNotHaveValidationErrorFor(x => x.Client.Email);
        }

        [Fact]
        public void Should_not_have_error_if_email_is_valid()
        {
            // Arrange
            var client = _fixture
                .Build<Client>()
                .With(client => client.Email, "test@test.pl")
                .Create();

            var command = new Command(client);

            // Act & Assert
            _validator
                .TestValidate(command)
                .ShouldNotHaveValidationErrorFor(x => x.Client.Email);
        }

        [Fact]
        public void Should_have_error_if_email_is_not_valid()
        {
            // Arrange
            var client = _fixture
                .Build<Client>()
                .With(client => client.Email, _fixture.Create<string>())
                .Create();

            var command = new Command(client);

            // Act & Assert
            _validator
                .TestValidate(command)
                .ShouldHaveValidationErrorFor(x => x.Client.Email)
                .WithErrorCode(nameof(EmailValidator));
        }
    }
}
