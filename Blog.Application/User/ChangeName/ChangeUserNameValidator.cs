using Blog.Domain.Communication.Requests.User;
using Blog.Exceptions.ExceptionMessages;
using FluentValidation;

namespace Blog.Application.User.ChangeName;

public class ChangeUserNameValidator : AbstractValidator<ChangeUsernameUserRequest> {
    public ChangeUserNameValidator() {
        RuleFor(u => u.NewName).NotEmpty().WithMessage(ErrorMessages.EmptyUsername);
    }
}