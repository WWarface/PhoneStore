using FluentValidation;
using PhoneStore.Models;

namespace PhoneStore.Validation
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(c => c.FirstName).NotEmpty().Length(4, 12);
            RuleFor(c => c.Age).GreaterThan(10).NotEmpty().WithMessage("Age is required field!!!");
            RuleFor(c => c.LastName).NotEmpty();
            RuleFor(c=>c.Email).NotEmpty().EmailAddress();
            RuleFor(c=>c.Phone).Length(4, 12).NotNull().NotEmpty();
        }
    }
}
