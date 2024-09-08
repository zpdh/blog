using Blog.Domain.Communication.Requests;
using Blog.Domain.Communication.Requests.Post;
using Blog.Exceptions.ExceptionMessages;
using FluentValidation;

namespace Blog.Application.Post;

public class CreatePostValidator : AbstractValidator<CreatePostRequest> {
    public CreatePostValidator() {
        RuleFor(post => post.Title).NotEmpty().WithMessage(ErrorMessages.EmptyTitle);
        RuleFor(post => post.TextContent).NotEmpty().WithMessage(ErrorMessages.EmptyTextContent);
    }
}