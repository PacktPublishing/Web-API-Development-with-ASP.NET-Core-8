using FluentValidation;

namespace MyWebApiDemo.Models.Validators;

public class UserValidator : AbstractValidator<User>
{
    public UserValidator()
    {
        RuleFor(u => u.FirstName)
            .NotEmpty()
            .WithMessage("The LastName field is required.")
            .Length(3, 50)
            .WithMessage("The length of LastName must be between 3 and 50.");

        RuleFor(u => u.LastName)
            .NotEmpty()
            .WithMessage("The LastName field is required.")
            .Length(3, 50)
            .WithMessage("The length of LastName must be between 3 and 50.");

        RuleFor(u => u.Age)
            .InclusiveBetween(1, 120)
            .WithMessage("The value of Age must be between 1 and 120.");

        RuleFor(u => u.Email)
            .NotEmpty()
            .WithMessage("The Email field is required.")
            .EmailAddress()
            .WithMessage("The Email field is not a valid e-mail address.");

        RuleFor(u => u.Country)
            .NotEmpty()
            .WithMessage("The Country field is required.")
            .Length(2, 50)
            .WithMessage("The length of Country must be between 2 and 50.");

        RuleFor(u => u.PhoneNumber)
            .NotEmpty()
            .WithMessage("The PhoneNumber field is required.");

        // Create a custom rule to validate the Country and PhoneNumber. If the country is New Zealand, the phone number must start with 64.
        RuleFor(u => u)
            .Custom((user, context) =>
            {
                if (user.Country.ToLower() == "new zealand" && !user.PhoneNumber.StartsWith("64"))
                {
                    context.AddFailure("The phone number must start with 64 for New Zealand users.");
                }
            });
    }
}
