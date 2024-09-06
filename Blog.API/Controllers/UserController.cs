using Blog.Application.User.Register;
using Blog.Domain.Communication.Requests;
using Blog.Domain.Communication.Requests.User;
using Blog.Domain.Communication.Responses;
using Blog.Domain.Communication.Responses.User;
using Microsoft.AspNetCore.Mvc;

namespace Blog.API.Controllers;

public class UserController : BlogController {
    [HttpPost]
    [Route("register")]
    [ProducesResponseType(typeof(RegisterUserResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register(
        [FromServices] IRegisterUserUseCase useCase,
        [FromBody] RegisterUserRequest request
    ) {
        var result = await useCase.ExecuteAsync(request);

        return Created(string.Empty, result);
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login() {
        return Ok();
    }
}