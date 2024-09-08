using Blog.API.Attributes;
using Blog.Application.Post;
using Blog.Domain.Communication.Requests.Post;
using Blog.Domain.Communication.Responses;
using Blog.Domain.Communication.Responses.Post;
using Microsoft.AspNetCore.Mvc;

namespace Blog.API.Controllers;

[AuthenticatedUser]
public class PostController : BlogController {
    [HttpPost]
    [Route("create")]
    [ProducesResponseType(typeof(CreatePostResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(
        [FromServices] ICreatePostUseCase useCase,
        [FromBody] CreatePostRequest request
    ) {
        var result = await useCase.ExecuteAsync(request);

        return Created(string.Empty, result);
    }

    [HttpPut]
    [Route("edit")]
    public async Task<IActionResult> Edit() {
        return NoContent();
    }

    [HttpDelete]
    [Route("code")]
    public async Task<IActionResult> Delete(
        [FromRoute] string code
    ) {
        return NoContent();
    }
}