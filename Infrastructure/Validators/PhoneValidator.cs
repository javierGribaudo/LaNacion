using FluentValidation;
using LaNacionChallenge.Domain;

namespace LaNacionChallenge.Infrastructure.Validators
{
    public class PhoneValidator : AbstractValidator<PhoneNumber>
    {
        public PhoneValidator()
        {
            RuleFor(c => c.Number)
                .Matches(@"^\d{3}-\d{3}-\d{4}$")
                .WithMessage("The phone number is not valid.");

        }
    }
}
