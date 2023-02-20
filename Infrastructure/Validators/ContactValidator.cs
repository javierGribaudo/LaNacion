using FluentValidation;
using LaNacionChallenge.Domain;

namespace LaNacionChallenge.Infrastructure.Validators
{
    public class ContactValidator : AbstractValidator<Contact>
    {
        public ContactValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Email).NotEmpty()
                .EmailAddress().WithMessage("The email is not valid.");
        }
    }
}
