using FluentValidation;
using PhoneStore.Models;

namespace PhoneStore.Validation
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(c => c.FirstName).NotEmpty().Length(4, 12);
            RuleFor(c => c.Age).NotEmpty().WithMessage("Wrong number");
            RuleFor(c => c.LastName).Matches("[A-Za-zА-Яа-я]").NotEmpty();
            RuleFor(c => c.Email).EmailAddress();
            RuleFor(c=>c.Phone).Length(4, 12).NotNull().NotEmpty();
        }
    }
}
