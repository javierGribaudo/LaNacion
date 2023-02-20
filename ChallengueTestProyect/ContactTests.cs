using FluentValidation.TestHelper;
using LaNacionChallenge.Domain;
using LaNacionChallenge.Infrastructure.Validators;

namespace ChallengueTestProyect
{
    [TestFixture]
    public class ContactTests
    {
        private ContactValidator _validator;
        private PhoneValidator _phoneValidator;

        [SetUp]
        public void Setup()
        {
            _validator = new ContactValidator();
            _phoneValidator = new PhoneValidator();
        }

        [Test]
        public void ShouldHaveErrorWhenNameIsNull()
        {
            var contact = new Contact { Name = null, Email = "test@test.com", PhoneNumbers = null };
            var result = _validator.TestValidate(contact);
            result.ShouldHaveValidationErrorFor(c => c.Name);
        }

        [Test]
        public void ShouldHaveErrorWhenEmailIsInvalid()
        {
            var contact = new Contact { Name = "John", Email = "invalid-email", PhoneNumbers = null };
            var result = _validator.TestValidate(contact);
            result.ShouldHaveValidationErrorFor(c => c.Email);
        }

        [Test]
        public void ShouldHaveErrorWhenPhoneNumberIsInvalid()
        {
            var phone = new PhoneNumber { Number = "invalid-phone" };
            var result = _phoneValidator.TestValidate(phone);
            result.ShouldHaveValidationErrorFor(c => c.Number);
        }

        [Test]
        public void ShouldNotHaveErrorsWhenContactIsValid()
        {
            var contact = new Contact { Name = "John", Email = "test@test.com", PhoneNumbers = new List<PhoneNumber> { new PhoneNumber { Number = "123456789" } } };
            var result = _validator.TestValidate(contact);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}