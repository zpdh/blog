using Blog.Domain.Extensions;
using Blog.Exceptions.ExceptionMessages;
using FluentValidation;

namespace Blog.Application.User.Register;

public class RegisterUserValidator : AbstractValidator<Domain.Entities.User> {
    public RegisterUserValidator() {
        RuleFor(user => user.Username).NotEmpty().WithMessage(ErrorMessages.EmptyUsername);

        RuleFor(user => user.Email).NotEmpty().WithMessage(ErrorMessages.EmptyEmail);
        When(user => user.Email.IsNotEmpty(), () =>
            RuleFor(user => user.Email).EmailAddress().WithMessage(ErrorMessages.InvalidEmail));

        RuleFor(user => user.Password).MinimumLength(6).WithMessage(ErrorMessages.InvalidPassword);

    }
}