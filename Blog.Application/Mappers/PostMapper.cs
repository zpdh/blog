using Blog.Application.Services.Code;
using Blog.Domain.Communication.Requests.Post;
using Blog.Domain.Communication.Responses.Post;

namespace Blog.Application.Mappers;

public static class PostMapper {

    #region Create Recipe

    public static Domain.Entities.Post MapToPost(
        this CreatePostRequest request,
        string code,
        Domain.Entities.User user
    ) {
        return new Domain.Entities.Post {
            Code = code,
            PostOwner = user,
            Title = request.Title,
            TextContent = request.TextContent
        };
    }

    public static CreatePostResponse MapToResponse(
        this Domain.Entities.Post post
    ) {
        return new CreatePostResponse(post.Code);
    }

    #endregion

}